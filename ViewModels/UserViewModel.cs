using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.ViewModels
{
	public class UserViewModel
	{
		public string ConfirmPassword { get; set; }

		public UserModel User { get; set; }

		/// <summary>
		/// Default Constructor
		/// </summary>
		public UserViewModel()
		{

		}
	}
}
