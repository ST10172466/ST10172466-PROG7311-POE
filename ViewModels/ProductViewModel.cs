using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.ViewModels
{
	public class ProductViewModel
	{
		/// <summary>
		/// List to store the Category Names
		/// </summary>
		public List<string> CategoryList { get; set; }

		/// <summary>
		/// Selected Category
		/// </summary>
		public string SelectedCategory { get; set; }

		public ProductModel Product { get; set; }

		public ProductViewModel()
		{

		}
	}
}
