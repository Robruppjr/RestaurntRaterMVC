using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Restaurant;

public class RestaurantService : IRestaurantService
    {
        private RestaurantDbContext _context;
        public RestaurantService(RestaurantDbContext context)
            {
                _context = context;
            }
        public async Task<List<RestaurantListItem>> GetAllRestaurants()
        {
            List<RestaurantListItem> restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score,
            }).ToListAsync();

            return restaurants;
        }
        public async Task<bool> CreateRestaurant(RestaurantCreate model)
        {
            Restaurant restaurant = new Restaurant()
            {
                Name = model.Name,
                Location = model.Location,
            };
            _context.Restaurants.Add(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }
        public Task<RestaurantDetail> GetRestaurantById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRestaurant(RestaurantEdit model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRestaurant(int id)
        {
            throw new NotImplementedException();
        }
    }
