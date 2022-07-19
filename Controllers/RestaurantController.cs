using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Restaurant;
namespace RestaurantRaterMVC.Controllers
{
    public class RestaurantController : Controller 
    {
        private IRestaurantService _service;
        private RestaurantDbContext _context;
        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index() {
            List<RestaurantListItem> restaurants = await _service.GetAllRestaurants();
            return View(restaurants);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [ActionName("Details")]
        public async Task<IActionResult> Restaurant(int id)
        {
            Restaurant restaurant = await _context.Restaurants
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (restaurant == null)
            return RedirectToAction(nameof(Index));
            RestaurantDetail restaurantDetail = new RestaurantDetail()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                Score = restaurant.Score
            };

            return View(restaurantDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreate model)
        {
            if(!ModelState.IsValid)
                return View(model);
                await _service.CreateRestaurant(model);
                return RedirectToAction(nameof(Index));
        }
    }
}