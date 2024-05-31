using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_Part_2.Models;
using System.Runtime.Intrinsics.X86;

namespace PROG7311_POE_Part_2.Controllers
{
	public class ForumController : Controller
	{
        public IActionResult ForumView()
        {
            // Fetch the list of discussions. This is just a placeholder, replace with your actual data fetching logic.
            var discussions = new List<ForumModel>
        {
            new ForumModel
            {
                Title = "Discussion 1",
                Content = "This is the content of the first discussion...",
                Link = "#",
                User1 = "User 1",
                Message1 = "This is a message from User 1.",
                User2 = "User 2",
                Message2 = "This is a message from User 2.",
            },
        };

            return View("ForumView", discussions);
        }
    }
}
