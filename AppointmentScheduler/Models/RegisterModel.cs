using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
	public class RegisterModel
	{
		[Required]
		public string Name { get; set; }

		[Key]
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set;}

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Confirm Password")]
		[Compare("Password", ErrorMessage = "Passwords don't match")]
		public string ConfirmPassword { get; set;}

		[Required]
		public string Phone { get; set;}
	}
}
