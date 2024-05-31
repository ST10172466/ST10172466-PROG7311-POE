using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG7311_POE_Part_2.Classes;
using PROG7311_POE_Part_2.ViewModels;
using System.Diagnostics;

namespace PROG7311_POE_Part_2.Controllers
{
    public class DisplayFarmerProductController : Controller
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Instansiating the dbContext
        /// </summary>
        private readonly DatabaseContextClass _dbContext;

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public DisplayFarmerProductController(DatabaseContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        //-----------------------------------------------------------------------------------------------*/

        // Population Methods
        #region Population
        //-----------------------------------------------------------------------------------------------//
        public DisplayProductViewModel PopulateTable(DisplayProductViewModel viewModel)
        {
            try
            {
                var userIDClaim = User.FindFirst("UserID");

                if (userIDClaim != null && Guid.TryParse(userIDClaim.Value, out Guid currentUserID))
                {
                    // Populates User List with all Farmers
                    viewModel.UsernameList = (from user in _dbContext.Users
                                              where user.RoleID == 1
                                              select user.Username).ToList();

                    // Populates Category List with all Categories
                    viewModel.CategoryList = _dbContext.Category.Select(c => c.CategoryName).ToList();

                    var query = _dbContext.Product.Include(p => p.User).Include(p => p.Category).Where(p => p.User.UserID == currentUserID).AsQueryable();

                    viewModel.ProductList = query.Select(p => new ProductTableViewModel
                    {
                        Product = p,
                        User = p.User,
                        Category = p.Category
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return viewModel;
        }
        #endregion Population
        //-----------------------------------------------------------------------------------------------//
        [HttpGet]
        public IActionResult DisplayFarmerProductView()
        {
            var viewModel = PopulateTable(new DisplayProductViewModel());
            return View("DisplayFarmerProductView", viewModel);
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//