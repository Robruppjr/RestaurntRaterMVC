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
            var restaurant = await _service.GetRestaurantById(id);
            if(restaurant == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await _service.GetRestaurantById(id);
            if( restaurant == null)
                return RedirectToAction(nameof(Index));
            var restaurantEdit = new RestaurantEdit()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location
            };
            return View(restaurantEdit);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = await _service.GetRestaurantById(id);
            if (restaurant == null)
                return RedirectToAction(nameof(Index));
            RestaurantDetail restaurantDetail =  new RestaurantDetail()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location
            };
            return View(restaurantDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, RestaurantDetail model)
        {
            await _service.DeleteRestaurant(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, RestaurantEdit model)
        {
            if(!ModelState.IsValid)
                return View(model);
                await _service.UpdateRestaurant(model);
                return RedirectToAction("Details", new { id = model.Id});

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