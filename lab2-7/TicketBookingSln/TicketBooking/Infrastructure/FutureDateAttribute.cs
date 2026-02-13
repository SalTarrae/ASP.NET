using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Infrastructure {
	public class FutureDateAttribute : ValidationAttribute {
		public override bool IsValid(object? value) {
			if (value is not DateTime dt)
				return true; // Required. Check if null/empty
			return dt > DateTime.Now;
		}
	}
}
