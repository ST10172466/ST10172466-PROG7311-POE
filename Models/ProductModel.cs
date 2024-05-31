using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG7311_POE_Part_2.Models
{
	[Table("Product")]
	public class ProductModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

		/// <summary>
		/// Globally Unique Identifier(GUID) for the Product, acts as a primary key
		/// </summary>
		public Guid ProductID { get; set; }

		/// <summary>
		/// Globally Unique Identifier(GUID) for the User, acts as a foreign key
		/// </summary>
		private Guid userID;

		/// <summary>
		/// Instance of the User class
		/// </summary>
		private UserModel user;

		/// <summary>
		/// Globally Unique Identifier(GUID) for the Category, acts as a foreign key
		/// </summary>
		private int categoryID;

		/// <summary>
		/// Instance of the User class
		/// </summary>
		private CategoryModel category;

		//-----------------------------------------------------------------------------------------------//

		//Details about a Product

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Name
		/// </summary>
		private string name = string.Empty;

		/// <summary>
		/// Price
		/// </summary>
		private decimal price = 0.0m;

		/// <summary>
		/// Date
		/// </summary>
		private DateTime productionDate = DateTime.MinValue;

		/// <summary>
		/// Description
		/// </summary>
		private string description = string.Empty;

		/// <summary>
		/// Stock
		/// </summary>
		private int stock = 0;

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ProductModel()
		{
		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// User ID getter and setter
		/// </summary>
		public Guid UserID { get => userID; set => userID = value; }

		/// <summary>
		/// User getter and setter
		/// </summary>
		public UserModel User { get => user; set => user = value; }

		/// <summary>
		/// Category ID getter and setter
		/// </summary>
		public int CategoryID { get => categoryID; set => categoryID = value; }

		/// <summary>
		/// Category getter and setter
		/// </summary>
		public CategoryModel Category { get => category; set => category = value; }

		/// <summary>
		/// Name getter and setter
		/// </summary>
		public string Name { get => name; set => name = value; }

		/// <summary>
		/// Price getter and setter
		/// </summary>
		public decimal Price { get => price; set => price = value; }

		/// <summary>
		/// Date getter and setter
		/// </summary>
		public DateTime ProductionDate { get => productionDate; set => productionDate = value; }

		/// <summary>
		/// Description getter and setter
		/// </summary>
		public string Description { get => description; set => description = value; }

		/// <summary>
		/// Stock getter and setter
		/// </summary>
		public int Stock { get => stock; set => stock = value; }

		//-----------------------------------------------------------------------------------------------//
	}
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//