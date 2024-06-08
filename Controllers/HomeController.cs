using System.Diagnostics;

namespace GamesLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesServices _gameService;

        public HomeController(IGamesServices gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            return View(games);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
