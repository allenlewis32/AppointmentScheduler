using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
	public class LoginModel
	{
		[Key]
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
