using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace PROG7311_POE_Part_2.Controllers
{
    public class LogOutController : Controller
    {
        /// <summary>
        /// Action that when the user clicks the logout button it removes their authentication 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginView", "Login");
        }
    }
}
