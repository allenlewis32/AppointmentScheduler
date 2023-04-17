using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
	public class LoginModel
	{
		[Key]
		[Required]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
