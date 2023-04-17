using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
	public class AppointmentModel
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string UserEmail { get; set; }

		[Required]
		public DateTime FromDate { get; set; }

		[Required]
		public DateTime ToDate { get; set; }

		[Required]
		public DateTime ReminderTime { get; set; }
	}
}
