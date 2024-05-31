namespace PROG7311_POE_Part_2.Classes
{
	public class UserHelperClass
	{
		/// <summary>
		/// Variable to store the UserID of the user that logged in so that it can be used across View Models
		/// </summary>
		private static Guid _currentUserID;

		/// <summary>
		/// Variable to store the Role of the user that logged in so that it can be used across View Models
		/// </summary>
		private static int _currentUserRole;

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Default Constructor
		/// </summary>
		static UserHelperClass()
		{

		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Variable to store the UserID of the user that logged in so that it can be used across View Models
		/// </summary>
		public static Guid CurrentUserID { get => _currentUserID; set => _currentUserID = value; }

		/// <summary>
		/// Variable to store the Role of the user that logged in so that it can be used across View Models
		/// </summary>
		public static int CurrentUserRole { get => _currentUserRole; set => _currentUserRole = value; }


		//-----------------------------------------------------------------------------------------------//
	}
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//