using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_Part_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PROG7311_POE_Part_2.Controllers
{
    public class MarketplaceController : Controller
    {
        public IActionResult MarketplaceView()
        {
            var products = new List<MarketplaceModel>
        {
            new MarketplaceModel { Name = "Tractor", Category = "Agriculture", Description = "Description for Product 1", Price = 99949.99M, ImageUrl = "https://images.unsplash.com/photo-1564868480822-32f714a0e763?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new MarketplaceModel { Name = "Solar Panel", Category = "Green Energy", Description = "Description for Product 2", Price = 23050.50M, ImageUrl = "https://plus.unsplash.com/premium_photo-1679500295784-0f9ba6d2d1d2?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new MarketplaceModel { Name = "Center Pivot Irrigation System", Category = "Agriculture", Description = "Description for Product 3", Price = 133304.95M, ImageUrl = "https://images.unsplash.com/photo-1453161535619-303d362ea39f?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
        };

            return View("MarketplaceView", products);
        }
    }
}
