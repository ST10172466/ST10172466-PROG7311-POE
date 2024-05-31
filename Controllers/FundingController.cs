using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.Controllers
{
    public class FundingController : Controller
    {
        public IActionResult FundingView()
        {
            var opportunities = new List<FundingModel>
        {
            new FundingModel { Name = "Green Farm Fund", Description = "Provided by Green Farm Co.", Amount = 10000 },
            new FundingModel { Name = "AgriBoost", Description = "Provided by AgriTech Solutions", Amount = 20000 },
            new FundingModel { Name = "EcoGrow Initiative", Description = "Provided by EcoGrow Enterprises", Amount = 15000 },
            new FundingModel { Name = "Farmers First", Description = "Provided by Farmers First Foundation", Amount = 25000 },
            new FundingModel { Name = "Sustainable Agriculture Fund", Description = "Provided by Sustainable Agriculture Association", Amount = 30000 },

        };

            return View("FundingView", opportunities);
        }
    }
}
