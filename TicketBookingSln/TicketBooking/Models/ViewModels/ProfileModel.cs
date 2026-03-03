using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Models.ViewModels {
	public class ProfileModel {
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
	}
}
