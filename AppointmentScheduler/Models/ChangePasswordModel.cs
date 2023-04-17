using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
	public class ChangePasswordModel
	{
		[Key]
		[Required]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("New Password")]
		public string NewPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Confirm Password")]
		[Compare("NewPassword", ErrorMessage = "Passwords don't match")]
		public string ConfirmPassword { get; set; }
	}
}
