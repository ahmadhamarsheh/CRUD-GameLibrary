using GamesLibrary.Attributes;

namespace GamesLibrary.ViewModels
{
    public class EditGameFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }
        public string? currentCover {  get; set; }

        [AllowedExtensions(FileSettings.AllowExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
