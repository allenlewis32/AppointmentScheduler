using System.ComponentModel;
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
		[DisplayName("From")]
		public DateTime FromDate { get; set; }

		[Required]
		[DisplayName("To")]
		public DateTime ToDate { get; set; }

		[Required]
		[DisplayName("Reminder")]
		public DateTime ReminderTime { get; set; }
	}
}
