using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace TicketBooking.Data.Seed {
	public class IdentitySeedData {
		private const string AdminRole = "Admin";
		private const string UserRole = "User";

		public static async Task EnsurePopulatedAsync(IApplicationBuilder app) {
			using var scope = app.ApplicationServices.CreateScope();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

			if (!await roleManager.RoleExistsAsync(AdminRole))
				await roleManager.CreateAsync(new IdentityRole(AdminRole));

			if (!await roleManager.RoleExistsAsync(UserRole))
				await roleManager.CreateAsync(new IdentityRole(UserRole));

			var adminEmail = "admin@ticketbooking.local";
			var admin = await userManager.FindByEmailAsync(adminEmail);
			if (admin == null) {
				admin = new IdentityUser { UserName = adminEmail, Email = adminEmail };
				var res = await userManager.CreateAsync(admin, "Admin1234");
				if (res.Succeeded)
					await userManager.AddToRoleAsync(admin, AdminRole);
			}

			var userEmail = "user@ticketbooking.local";
			var user = await userManager.FindByEmailAsync(userEmail);
			if (user == null) {
				user = new IdentityUser { UserName = userEmail, Email = userEmail };
				var res = await userManager.CreateAsync(user, "User1234");
				if (res.Succeeded)
					await userManager.AddToRoleAsync(user, UserRole);
			}
		}
	}
}
