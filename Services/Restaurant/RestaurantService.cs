using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRaterMVC.Controllers.Data;

namespace RestaurantRaterMVC.Services.Restaurant
{
    public class RestaurantService
    {
        public RestaurantService()
        {
            
        }
            private RestaurantDbContext _context;
            public RestaurantService(RestaurantDbContext context)
            {
                _context = context;
            }
    }
}