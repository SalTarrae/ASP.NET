using TicketBooking.Infrastructure;

namespace TicketBooking.Models {
	public class SessionCart : Cart {
		public static Cart GetCart(IServiceProvider services) {
			var session = services.GetRequiredService<IHttpContextAccessor>()
								  .HttpContext!.Session;

			var cart = session.GetJson<SessionCart>("Cart") ?? new SessionCart();
			return cart;
		}

		public void Save(ISession session) {
			session.SetJson("Cart", this);
		}
	}
}
