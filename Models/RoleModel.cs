using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG7311_POE_Part_2.Models
{
	[Table("Roles")]
	public class RoleModel
	{
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Int for the Role, acts as a primary key
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int RoleID { get; set; }

		/// <summary>
		/// Collection of Users
		/// </summary>
		public ICollection<UserModel> Users { get; set; }

        /// <summary>
        /// Role Name 
        /// </summary>
        private string roleName = string.Empty;

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RoleModel()
		{

		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Role Name getter and setter
		/// </summary>
		public string RoleName { get => roleName; set => roleName = value; }
		
		//-----------------------------------------------------------------------------------------------//
	}
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//