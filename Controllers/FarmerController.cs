using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_Part_2.Classes;
using PROG7311_POE_Part_2.Models;
using PROG7311_POE_Part_2.ViewModels;
using System.Diagnostics;

namespace PROG7311_POE_Part_2.Controllers
{
	public class FarmerController : Controller
	{
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Instance of the DatabaseContext class
		/// </summary>
		private readonly DatabaseContextClass _dbContext;

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbContext"></param>
		public FarmerController(DatabaseContextClass dbContext)
		{
			_dbContext = dbContext;
		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to display the FarmerView
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult FarmerView()
		{
			var viewModel = RetrieveCategories();
			return View("FarmerView", viewModel);
		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to populate the Category ComboBox
		/// </summary>
		/// <returns></returns>
		private ProductViewModel RetrieveCategories()
		{
			try
			{
				List<string> categoryNamesList = _dbContext.Category.Select(c => c.CategoryName).ToList();

				return new ProductViewModel
				{
					CategoryList = categoryNamesList
				};
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
			return new ProductViewModel();
		}

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to add the user details
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProductDetails(ProductViewModel viewModel)
        {
            try
            {
                // Retrieve the User ID claim from the authentication ticket
                var userIdClaim = User.FindFirst("UserID");

                // Check if the User ID in the authentication ticket is null, if not converts it to a GUID
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    ViewBag.ErrorMessage = "Invalid user ID.";
                    return View("FarmerView", viewModel);
                }

                // Prevent incorrect Product Name input
                if (string.IsNullOrWhiteSpace(viewModel.Product.Name))
                {
                    ViewBag.ErrorMessage = "Invalid Product Name.";
                    return View("FarmerView", viewModel);
                }

                // Prevent incorrect Price input
                if (viewModel.Product.Price <= 0)
                {
                    ViewBag.ErrorMessage = "Invalid Product Price.";
                    return View("FarmerView", viewModel);
                }

                // Prevent incorrect Stock input
                if (viewModel.Product.Stock <= 0 || viewModel.Product.Stock > 1000)
                {
                    ViewBag.ErrorMessage = "Invalid Stock Level.";
                    return View("FarmerView", viewModel);
                }

                // Retrieve stored User ID and associated User
                var selectedUser = _dbContext.Users.FirstOrDefault(u => u.UserID == userId);

                if (selectedUser == null)
                {
                    ViewBag.ErrorMessage = "User not found.";
                    return View("FarmerView", viewModel);
                }

                var selectedCategory = _dbContext.Category.FirstOrDefault(c => c.CategoryName == viewModel.SelectedCategory);

                if (selectedCategory == null)
                {
                    ViewBag.ErrorMessage = "Category not found.";
                    return View("FarmerView", viewModel);
                }

                // Add new Product
                var newProduct = new ProductModel
                {
                    UserID = selectedUser.UserID,
                    CategoryID = selectedCategory.CategoryID,
                    Name = viewModel.Product.Name.Trim(),
                    Price = viewModel.Product.Price,
                    ProductionDate = viewModel.Product.ProductionDate,
                    Description = viewModel.Product.Description.Trim(),
                    Stock = viewModel.Product.Stock
                };

                _dbContext.Product.Add(newProduct);
                await _dbContext.SaveChangesAsync();

                viewModel.CategoryList = RetrieveCategories().CategoryList;
                ViewBag.SuccessMessage = "Product was added successfully.";
                return View("FarmerView", viewModel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex}");
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
            }

            viewModel.CategoryList = RetrieveCategories().CategoryList;
            return View("FarmerView", viewModel);
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
