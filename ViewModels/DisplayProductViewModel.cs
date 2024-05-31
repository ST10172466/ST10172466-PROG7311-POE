using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.ViewModels
{
    public class DisplayProductViewModel
    {
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// List of Usernames
		/// </summary>
		public List<string> UsernameList { get; set; }

        /// <summary>
        /// Selected User ID
        /// </summary>
        public string SelectedUser { get; set; }

        /// <summary>
        /// List of ProductModel objects
        /// </summary>
        public List<ProductTableViewModel> ProductList { get; set; }

        /// <summary>
        /// ProductModel object 
        /// </summary>
        public ProductModel Product { get; set; }

		/// <summary>
		/// List to store the Category Names
		/// </summary>
		public List<string> CategoryList { get; set; }

		/// <summary>
		/// Selected Category
		/// </summary>
		public string SelectedCategory { get; set; }

		/// <summary>
		/// Price Filter
		/// </summary>
		public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

		/// <summary>
		/// Date Filter
		/// </summary>
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }

		/// <summary>
		/// Stock Filter
		/// </summary>
		public int MinStock { get; set; }
		public int MaxStock { get; set; }

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Constructor
		/// </summary>
		public DisplayProductViewModel()
        {
            ProductList = new List<ProductTableViewModel>();
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//