using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.Controllers
{
	public class EducationHubController : Controller
	{
        public IActionResult EducationHubView()
        {
            var articles = new List<EducationHubModel>
        {
            new EducationHubModel { Title = "Article 1", Summary = "This is a summary of the first article...", Link = "#" },
            new EducationHubModel { Title = "Article 2", Summary = "This is a summary of the second article...", Link = "#" },
            new EducationHubModel { Title = "Article 3", Summary = "This is a summary of the third article...", Link = "#" },
            new EducationHubModel { Title = "Article 4", Summary = "This is a summary of the second article...", Link = "#" },
            new EducationHubModel { Title = "Article 5", Summary = "This is a summary of the third article...", Link = "#" },
            new EducationHubModel { Title = "Article 6", Summary = "This is a summary of the second article...", Link = "#" },
            new EducationHubModel { Title = "Article 7", Summary = "This is a summary of the third article...", Link = "#" },
        };

            return View("EducationHubView", articles);
        }
    }
}
