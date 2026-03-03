using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Models.ViewModels {
	public class LoginModel {
		[Required(ErrorMessage = "Please enter your email")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter your password")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		public string? ReturnUrl { get; set; }
	}
}
