
namespace GamesLibrary.Controllers
{
    public class GamesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoriesService _categoriesService;   //to inject in constructor
        private readonly IDevicesServices _devicesServices;
        private readonly IGamesServices _gamesService;
        public GamesController(AppDbContext context, ICategoriesService categoriesService, IDevicesServices devicesServices, IGamesServices gamesService)  //don't forget defined DI in program using builder
        {
            _context = context;
            _categoriesService = categoriesService;
            _devicesServices = devicesServices;
            _gamesService = gamesService;
        }
        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);
            if(game is null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),

                Devices = _devicesServices.GetDevices()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //To check Token if valid or not (token created at end of form
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();

                model.Devices = _devicesServices.GetDevices();

                return View(model);
            }
            //Save game to database
            //save cover to server
            await _gamesService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetById(id);
            if(game is null)
            {
                return NotFound();
            }
            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesServices.GetDevices(),
                Description = game.Description,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                CategoryId = game.category,
                Name = game.Name,
                currentCover = game.Cover,
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //To check Token if valid or not (token created at end of form
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();

                model.Devices = _devicesServices.GetDevices();

                return View(model);
            }
            //Save game to database
            //save cover to server
            var game = await _gamesService.Update(model);
            if(game is null)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gamesService.Delete(id);
            
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
