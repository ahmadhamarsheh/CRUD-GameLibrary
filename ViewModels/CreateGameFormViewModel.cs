
using GamesLibrary.Attributes;

namespace GamesLibrary.ViewModels
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        //[Extension]  //better in API not in MVC
        [AllowedExtensions(FileSettings.AllowExtensions) , MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
