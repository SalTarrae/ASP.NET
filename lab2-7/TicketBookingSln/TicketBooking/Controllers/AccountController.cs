using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Models.ViewModels;

namespace TicketBooking.Controllers {
	public class AccountController : Controller {

		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr) {
			_userManager = userMgr;
			_signInManager = signInMgr;
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Register() {
			return View(new RegisterModel());
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel model) {
			if (!ModelState.IsValid)
				return View(model);

			var user = new IdentityUser {
				UserName = model.Email,
				Email = model.Email
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded) {
				await _signInManager.SignInAsync(user, isPersistent: false);
				return RedirectToAction("Index", "Home");
			}

			foreach (var err in result.Errors)
				ModelState.AddModelError("", err.Description);

			return View(model);
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login(string? returnUrl = null) {
			return View(new LoginModel { ReturnUrl = returnUrl });
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model) {
			if (!ModelState.IsValid)
				return View(model);

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user != null) {
				await _signInManager.SignOutAsync();
				var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
				if (result.Succeeded)
					return Redirect(model.ReturnUrl ?? "/");
			}

			ModelState.AddModelError("", "Invalid email or password");
			return View(model);
		}

		[Authorize]
		public async Task<IActionResult> Logout(string returnUrl = "/") {
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Profile() {
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return RedirectToAction(nameof(Login));

			return View(new ProfileModel { Email = user.Email ?? "" });
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Profile(ProfileModel model) {
			if (!ModelState.IsValid)
				return View(model);

			var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return RedirectToAction(nameof(Login));

			user.Email = model.Email;
			user.UserName = model.Email;

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded) {
				ViewBag.Success = "Profile updated";
				return View(model);
			}

			foreach (var err in result.Errors)
				ModelState.AddModelError("", err.Description);

			return View(model);
		}

		[AllowAnonymous]
		public IActionResult AccessDenied() => View();
	}
}
