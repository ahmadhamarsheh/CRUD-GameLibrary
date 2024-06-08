namespace GamesLibrary.Services
{
    public interface IGamesServices
    {
        IEnumerable<Games> GetAll();
        Games? GetById(int id);
        Task Create(CreateGameFormViewModel model);
        Task<Games?> Update(EditGameFormViewModel model);
        bool Delete(int id);
    }
}
