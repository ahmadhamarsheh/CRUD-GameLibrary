

namespace GamesLibrary.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;
        public GamesServices(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";  //save path of image in variable to use it when need it
        }

        public IEnumerable<Games> GetAll()
        {
            return _context.Games
                .Include(x => x.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }
        public Games? GetById(int id)
        {
            return _context.Games
                .Include(x => x.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);
            //Now Cover saved in DB

            //Now Save Game in DB
            Games game = new()
            {
                Name = model.Name,
                Description = model.Description,
                category = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Games?> Update(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(x => x.Devices)
                .SingleOrDefault(g => g.Id == model.Id);
            if(game is null)
            {
                return null;
            }

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.category = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId=d }).ToList();

            if(hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _context.SaveChanges();
            if(effectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagePath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(id);
            if(game is null)
            {
                return isDeleted;
            }
            _context.Remove(game);

            var effectedRows = _context.SaveChanges();
            if(effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}"; //Guid.NewGuid for uniqe value
            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;

        }

        
    }
}
