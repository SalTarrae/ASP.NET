using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Data.Infrastructure {
	public class FutureDateAttribute : ValidationAttribute {
		public override bool IsValid(object? value) {
			if (value is not DateTime dt)
				return true; // Required. Check if null/empty
			return dt > DateTime.Now;
		}
	}
}
