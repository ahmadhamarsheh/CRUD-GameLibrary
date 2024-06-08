namespace GamesLibrary.Services
{
    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
