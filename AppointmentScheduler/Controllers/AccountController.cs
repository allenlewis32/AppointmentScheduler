using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AppointmentScheduler.Controllers
{
	public class AccountController : Controller
	{
		// GET: Account/Register
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// POST: Account/Register
		public IActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// check if email is already registered
					SqlCommand cmd = new($"select count(*) from users where email='{model.Email}'", Connection.conn);
					int? res = (int?)cmd.ExecuteScalar();
					if (res != null && res > 0) // email already registered
					{
						ModelState.AddModelError("", "Email already registered");
					}
					else
					{
						cmd = new("createUser", Connection.conn);
						cmd.CommandType = System.Data.CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@email", model.Email);
						cmd.Parameters.AddWithValue("@password", model.Password);
						cmd.Parameters.AddWithValue("@phone", model.Phone);
						cmd.Parameters.AddWithValue("@name", model.Name);
						cmd.ExecuteNonQuery();
						HttpContext.Session.SetString("email", model.Email);
						HttpContext.Session.SetString("name", model.Name);
						return RedirectToAction("Index", "Appointment");
					}
				}
				catch
				{
					ModelState.AddModelError("", "Unable to register user");
				}
			}
			return View(model);
		}

		// GET: Account/Logout
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}

		// GET: Accounr/Login
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// POST: Account/Login
		public IActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					SqlCommand cmd = new("checkLoginAndGetName", Connection.conn);
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@email", model.Email);
					cmd.Parameters.AddWithValue("@password", model.Password);
					string? res = (string?)cmd.ExecuteScalar();
					if (res != null)
					{
						HttpContext.Session.SetString("email", model.Email);
						HttpContext.Session.SetString("name", res!);
						return RedirectToAction("Index", "Appointment");
					}
					else
					{
						ModelState.AddModelError("", "Invalid email/password");
					}
				}
				catch
				{
					ModelState.AddModelError("", "Unable to login");
				}
			}
			return View(model);
		}

		// GET: Account/ChangePassword
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// POST: Account/ChangePassword
		public IActionResult ChangePassword(ChangePasswordModel model)
		{
			string? email = HttpContext.Session.GetString("email");
			if(email == null)
			{
				return RedirectToAction("Login");
			}
			if (ModelState.IsValid)
			{
				if (model.OldPassword == model.NewPassword)
				{
					TempData["error"] = "New password cannot be the same as the old password";
				}
				else
				{
					try
					{
						SqlCommand command = new("updatePassword", Connection.conn);
						command.CommandType = System.Data.CommandType.StoredProcedure;
						command.Parameters.AddWithValue("@email", email);
						command.Parameters.AddWithValue("@password", model.NewPassword);
						command.ExecuteNonQuery();
						TempData["success"] = "Password changed";
						return RedirectToAction("Index", "Appointment");
					}
					catch
					{
						TempData["error"] = "Unable to change password";
					}
				}
			}
			return View(model);
		}
	}
}
