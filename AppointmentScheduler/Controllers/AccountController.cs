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
					return RedirectToAction("Index", "Home");
				}
			}
			catch
			{
				ModelState.AddModelError("", "Unable to register user");
			}
			return View(model);
		}

		// GET: Account/Logout
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}
