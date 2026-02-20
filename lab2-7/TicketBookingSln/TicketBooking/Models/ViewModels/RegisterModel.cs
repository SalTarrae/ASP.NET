using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Models.ViewModels {
	public class RegisterModel {
		[Required(ErrorMessage = "Please enter your email")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter a password")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please confirm your password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
