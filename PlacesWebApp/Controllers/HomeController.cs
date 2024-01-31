using Microsoft.AspNetCore.Mvc;
using PlacesWebApp.Data;
using PlacesWebApp.Models;
using System.Diagnostics;

namespace PlacesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlacesDbContext placesDbContext;

        public HomeController(ILogger<HomeController> logger, PlacesDbContext placesDbContext)
        {
            _logger = logger;
            this.placesDbContext = placesDbContext;

        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;

            var places = placesDbContext.PlacesTable
                .OrderBy(p => p.PlaceId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalPlaces = placesDbContext.PlacesTable.Count();

            var totalPages = (int)Math.Ceiling((double)totalPlaces / pageSize);

            var currentListItems = new Tuple<List<PlaceItem>, int, int>(places, totalPages, page);

            return View(currentListItems);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
