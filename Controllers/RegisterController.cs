﻿using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_Part_2.Classes;
using PROG7311_POE_Part_2.Models;
using PROG7311_POE_Part_2.ViewModels;
using System.Diagnostics;
using System.Security.Cryptography;

namespace PROG7311_POE_Part_2.Controllers
{
	public class RegisterController : Controller
	{
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Instance of the DatabaseContext class
		/// </summary>
		private readonly DatabaseContextClass _dbContext;

		/// <summary>
		/// Variable to store the hashed password
		/// </summary>
		private string hashedPassword = string.Empty;

		/// <summary>
		/// Variable to store the salt used to hash the password
		/// </summary>
		private string salt = string.Empty;

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dbContext"></param>
		public RegisterController(DatabaseContextClass dbContext)
		{
			_dbContext = dbContext;
		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to display the RegisterView
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult RegisterView()
		{
			var viewModel = new UserViewModel();
			return View("RegisterView", viewModel);
		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to add the user details
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> AddUserDetails(UserViewModel viewModel)
		{
			try
			{
				// Checks if the username already exists
				if (UserNameValidation(viewModel.User.Username))
				{
                    ViewBag.ErrorMessage = "Username already exists.";
					return View("RegisterView", viewModel);
				}

				// Calls method to hash the password
				CreatePasswordHash(viewModel.User.UserPassword, out this.salt, out this.hashedPassword);

				// Checks if Password and Password Confirmation match
				if (!VerifyPassword(viewModel.ConfirmPassword, this.salt, this.hashedPassword))
				{
                    ViewBag.ErrorMessage = "Passwords do not match.";
					return View("RegisterView", viewModel);
				}

				var role = 0;
				var newUser = new UserModel
				{
					Name = viewModel.User.Name,
					Surname = viewModel.User.Surname,
					RoleID = role,
					Username = viewModel.User.Username,
					UserPassword = this.hashedPassword,
					Salt = this.salt
				};

				_dbContext.Users.Add(newUser);
				_dbContext.SaveChanges();

				await _dbContext.SaveChangesAsync();

				return RedirectToAction("LoginView", "Login");
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error: {ex}");
			}

			return View("RegisterView", viewModel);
		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method that checks if the inputed username already exists in the database
		/// </summary>
		/// <param name="username"></param>
		/// <returns></returns>
		private bool UserNameValidation(string username)
		{
			return _dbContext.Users.Any(u => u.Username == username);
		}

		//-----------------------------------------------------------------------------------------------//

		// Hash Methods
		#region Hashing
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to hash the password
		/// </summary>
		/// <param name="password"></param>
		/// <param name="salt"></param>
		/// <param name="hash"></param>
		public void CreatePasswordHash(string password, out string salt, out string hash)
		{
			using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
			{
				byte[] saltBytes = new byte[32];
				rngCsp.GetBytes(saltBytes);
				salt = BitConverter.ToString(saltBytes).Replace("-", "");

				string saltedPassword = String.Concat(password, salt);
				using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(saltedPassword, saltBytes, 10000))
				{
					byte[] hashBytes = hasher.GetBytes(32); // SHA-256 produces a 32-byte hash
					hash = BitConverter.ToString(hashBytes).Replace("-", "");
				}
			}
		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to verify the hashed password
		/// </summary>
		/// <param name="enteredPassword"></param>
		/// <param name="storedSalt"></param>
		/// <param name="storedHash"></param>
		/// <returns></returns>
		public bool VerifyPassword(string enteredPassword, string storedSalt, string storedHash)
		{
			string saltedPassword = String.Concat(enteredPassword, storedSalt);
			using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(saltedPassword, StringToByteArray(storedSalt), 10000))
			{
				byte[] hashBytes = hasher.GetBytes(32); // SHA-256 produces a 32-byte hash
				string computedHash = BitConverter.ToString(hashBytes).Replace("-", "");
				return computedHash == storedHash;
			}
		}

		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Helper method to convert a hex string to a byte array
		/// </summary>
		/// <param name="hex"></param>
		/// <returns></returns>
		private byte[] StringToByteArray(string hex)
		{
			int length = hex.Length / 2;
			byte[] bytes = new byte[length];
			for (int i = 0; i < length; i++)
			{
				bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
			}
			return bytes;
		}
		#endregion Hashing
		//-----------------------------------------------------------------------------------------------//

		// Redirect Methods
		#region Redirect
		//-----------------------------------------------------------------------------------------------//
		/// <summary>
		/// Method to redirect user to the Login page
		/// </summary>
		/// <returns></returns>
		public IActionResult RedirectToLoginView()
		{
			return RedirectToAction("LoginView", "Login");
		}
		#endregion Redirect
		//-----------------------------------------------------------------------------------------------//
	}
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//