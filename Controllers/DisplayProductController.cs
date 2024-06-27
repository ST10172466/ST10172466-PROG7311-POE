using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG7311_POE_Part_2.Classes;
using PROG7311_POE_Part_2.Models;
using PROG7311_POE_Part_2.ViewModels;
using System.Diagnostics;

namespace PROG7311_POE_Part_2.Controllers
{
	public class DisplayProductController : Controller
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
		public DisplayProductController(DatabaseContextClass dbContext)
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
                    viewModel.UsernameList = _dbContext.Users
                                               .Where(user => user.RoleID == 1)
                                               .Select(user => user.Username)
                                               .ToList();

                    // Populates Category List with all Categories
                    viewModel.CategoryList = _dbContext.Category.Select(c => c.CategoryName).ToList();

                    // Retrieve products for the current user
                    var query = _dbContext.Product.Include(p => p.User).Include(p => p.Category).AsQueryable();

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
		public IActionResult DisplayProductView()
		{
			var viewModel = PopulateTable(new DisplayProductViewModel());
			return View("DisplayProductView", viewModel);
		}

        //-----------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Method to filter Products
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
		public async Task<IActionResult> FilterProducts(DisplayProductViewModel viewModel)
		{
			try
			{
                // Base query to include related User and Category entities
                var query = _dbContext.Product.Include(p => p.User).Include(p => p.Category).AsQueryable();

                // Filter by selected user
                if (!string.IsNullOrEmpty(viewModel.SelectedUser))
				{
					var selectedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == viewModel.SelectedUser);
					if (selectedUser != null)
					{
						query = query.Where(p => p.UserID == selectedUser.UserID);
					}
				}

                // Filter by selected category
                if (!string.IsNullOrEmpty(viewModel.SelectedCategory))
				{
					var selectedCategory = await _dbContext.Category.FirstOrDefaultAsync(c => c.CategoryName == viewModel.SelectedCategory);
					if (selectedCategory != null)
					{
						query = query.Where(p => p.CategoryID == selectedCategory.CategoryID);
					}
				}

                // Filter by price range
                if (viewModel.MinPrice <= viewModel.MaxPrice)
				{
					query = query.Where(p => p.Price >= viewModel.MinPrice && p.Price <= viewModel.MaxPrice);
				}

                // Filter by production date range
                if (viewModel.MinDate <= viewModel.MaxDate)
				{
					query = query.Where(p => p.ProductionDate >= viewModel.MinDate && p.ProductionDate <= viewModel.MaxDate);
				}

                // Filter by stock range
                if (viewModel.MinStock <= viewModel.MaxStock)
				{
					query = query.Where(p => p.Stock >= viewModel.MinStock && p.Stock <= viewModel.MaxStock);
				}

                // Retrieve filtered products
                viewModel.ProductList = await query.Select(p => new ProductTableViewModel
                {
                    Product = p,
                    User = p.User,
                    Category = p.Category
                }).ToListAsync();

                // Populate user and category lists for the view model
                viewModel.UsernameList = await (from user in _dbContext.Users
												where user.RoleID == 1
												select user.Username).ToListAsync();

				viewModel.CategoryList = await _dbContext.Category.Select(c => c.CategoryName).ToListAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}

            return PartialView("_ProductTable", viewModel.ProductList);
        }

		//-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//