using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG7311_POE_Part_2.Models
{
	[Table("Users")]
	public class UserModel
	{
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Globally Unique Identifier(GUID) for the User, acts as a primary key
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid UserID { get; set; }

		/// <summary>
		/// Collection of Products
		/// </summary>
		public ICollection<ProductModel> Products { get; set; }

		/// <summary>
		/// Name of the user
		/// </summary>
		[Required(ErrorMessage = "Name is required.")]
		private string name = string.Empty;

		/// <summary>
		/// Surname of the user
		/// </summary>
		[Required(ErrorMessage = "Surname is required.")]
		private string surname = string.Empty;

		/// <summary>
		/// Username, for example, Joe_Russell
		/// </summary>
		[Required(ErrorMessage = "Username is required.")]
		private string username = string.Empty;

		/// <summary>
		/// Password, for example, Test1@3$
		/// </summary>
		[Required(ErrorMessage = "Password is required.")]
		private string userPassword = string.Empty;

		/// <summary>
		/// Salt, for example, 123456
		/// </summary>
		private string salt = string.Empty;

		/// <summary>
		/// Role ID for the user
		/// </summary>
		public int RoleID { get; set; }

		/// <summary>
		/// Role for the user
		/// </summary>
		[ForeignKey("RoleID")]
		public RoleModel Role { get; set; }

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor
		/// </summary>
		public UserModel()
		{

		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Name getter and setter
		/// </summary>
		public string Name { get => name; set => name = value; }

		/// <summary>
		/// Surname getter and setter
		/// </summary>
		public string Surname { get => surname; set => surname = value; }

		/// <summary>
		/// Username getter and setter
		/// </summary>
		public string Username { get => username; set => username = value; }

		/// <summary>
		/// Password getter and setter
		/// </summary>
		public string UserPassword { get => userPassword; set => userPassword = value; }

		/// <summary>
		/// Salt getter and setter
		/// </summary>
		public string Salt { get => salt; set => salt = value; }

		//-----------------------------------------------------------------------------------------------//
	}
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//