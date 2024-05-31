using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using PROG7311_POE_Part_2.Classes;
using PROG7311_POE_Part_2.ViewModels;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.Controllers
{
    public class LoginController : Controller
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Local Database Context Class
        /// </summary>
        private readonly DatabaseContextClass _dbContext;

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public LoginController(DatabaseContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to display the LoginView
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LoginView()
        {
            var userModel = new UserViewModel();
            return View(userModel);
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to verify the user details
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> VerifyUserDetails(UserViewModel userModel)
        {
            // Try-catch to handle errors
            try
            {
                // Check if user exists
                var enteredUsername = _dbContext.Users.FirstOrDefault(u => u.Username == userModel.User.Username);

                // If user does not exist, return to LoginView
                if (enteredUsername == null)
                {
                    ViewBag.ErrorMessage = "Username does not exist.";
                    return View("LoginView", userModel);
                }
                else
                {
                    // If user exists, verify password
                    if (this.VerifyPassword(userModel.User.UserPassword, enteredUsername.Salt, enteredUsername.UserPassword))
                    {
                        // Assigns static variable to reduce number of calls to the database
                        UserHelperClass.CurrentUserID = enteredUsername.UserID;
                        UserHelperClass.CurrentUserRole = enteredUsername.RoleID;

                        var options = new CookieOptions();
                        options.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Append("RoleID", enteredUsername.RoleID.ToString(), options);

                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                        var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name,enteredUsername.Username),
                        new Claim(ClaimTypes.Role,"User"),
                        new Claim("UserID", enteredUsername.UserID.ToString())
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        switch (enteredUsername.RoleID)
                        {
                            case 0:
                                return RedirectToAction("EmployeeView", "Employee");
                            case 1:
                                return RedirectToAction("FarmerView", "Farmer");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Incorrect password. Please try again.";
                        return View("LoginView", userModel);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex}");
            }

            return RedirectToAction("LoginView", "Login");
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to switch to the RegisterView
        /// </summary>
        /// <returns></returns>
        public IActionResult SwitchToRegisterView()
        {
            return RedirectToAction("RegisterView", "Register");
        }

        //-----------------------------------------------------------------------------------------------//
        public async Task<IActionResult> EmployeeLogin()
        {
            string username = "Joe_Russell";
            string password = @"J03Russ3\\";

            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && VerifyPassword(password, user.Salt, user.UserPassword))
            {
                UserHelperClass.CurrentUserID = user.UserID;
                UserHelperClass.CurrentUserRole = user.RoleID;

                var options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("RoleID", user.RoleID.ToString(), options);

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("UserID", user.UserID.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("EmployeeView", "Employee");
            }

            return RedirectToAction("LoginView", "Login");
        }

        //-----------------------------------------------------------------------------------------------//
        public async Task<IActionResult> FarmerLogin()
        {
            string username = "Koos_GreenThumb";
            string password = "Farm@2024!Koos";

            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);

            if (user != null && VerifyPassword(password, user.Salt, user.UserPassword))
            {
                UserHelperClass.CurrentUserID = user.UserID;
                UserHelperClass.CurrentUserRole = user.RoleID;

                var options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("RoleID", user.RoleID.ToString(), options);

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("UserID", user.UserID.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("FarmerView", "Farmer");
            }

            return RedirectToAction("LoginView", "Login");
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to verify the hashed password
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="storedSalt"></param>
        /// <param name="storedHash"></param>
        /// <returns></returns>
        public bool VerifyPassword(string enteredPassword, string storedSalt, string storedHash)
        {
            string saltedPassword = String.Concat(enteredPassword, storedSalt);
            using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(saltedPassword, StringToByteArray(storedSalt), 10000))
            {
                byte[] hashBytes = hasher.GetBytes(32); // SHA-256 produces a 32-byte hash
                string computedHash = BitConverter.ToString(hashBytes).Replace("-", "");
                return computedHash == storedHash;
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Helper method to convert a hex string to a byte array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private byte[] StringToByteArray(string hex)
        {
            int length = hex.Length / 2;
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//
