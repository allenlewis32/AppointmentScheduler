using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AppointmentScheduler.Controllers
{
	public class AppointmentController : Controller
	{
		public IActionResult Index()
		{
			return RedirectToAction("List");
		}

		// GET: Appointment/List
		public IActionResult List()
		{
			if(HttpContext.Session.Get("email") == null)
			{
				return RedirectToAction("Index", "Home");
			}
			List<AppointmentModel> appointments = new();
			SqlCommand cmd = new("select * from appointments", Connection.conn);
			using (var reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					appointments.Add(new()
					{
						Id = reader.GetInt32(0),
						Name = reader.GetString(1),
						UserEmail = reader.GetString(2),
						FromDate = reader.GetDateTime(3),
						ToDate = reader.GetDateTime(4),
						ReminderTime = reader.GetDateTime(5),
					});
				}
			}
			return View(appointments);
		}

		// GET: Appointment/New
		public IActionResult New()
		{
            string? email = HttpContext.Session.GetString("email");
            if (email == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult New(AppointmentModel model)
		{
			string? email = HttpContext.Session.GetString("email");
			if (email == null)
			{
                return RedirectToAction("Index", "Home");
            }
            ModelState.Remove("Id");
			ModelState.Remove("UserEmail");
            if (ModelState.IsValid)
			{
				SqlCommand command = new("createAppointment", Connection.conn);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@name", model.Name);
				command.Parameters.AddWithValue("@userEmail", email);
				command.Parameters.AddWithValue("@fromDate", model.FromDate);
				command.Parameters.AddWithValue("@toDate", model.ToDate);
				command.Parameters.AddWithValue("@reminderTime", model.ReminderTime);
				command.ExecuteNonQuery();
				TempData["success"] = "Appointment added";
			}
			return View(model);
		}
	}
}
