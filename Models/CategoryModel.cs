using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG7311_POE_Part_2.Models
{
	[Table("Category")]
	public class CategoryModel
	{
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Int for the Category, acts as a primary key
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CategoryID { get; set; }

		/// <summary>
		/// Collection of Products
		/// </summary>
		public ICollection<ProductModel> Products { get; set; }

		/// <summary>
		/// Name of the category
		/// </summary>
		private string categoryName = string.Empty;

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor
		/// </summary>
		public CategoryModel()
		{

		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Category Name getter and setter
		/// </summary>
		public string CategoryName { get => categoryName; set => categoryName = value; }

		//-----------------------------------------------------------------------------------------------//
	}
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//