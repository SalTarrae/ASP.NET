using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace TicketBooking.Client.Services {
	public class AuthService {
		private readonly HttpClient _http;
		private readonly ILocalStorageService _localStorage;

		public AuthService(HttpClient http, ILocalStorageService localStorage) {
			_http = http;
			_localStorage = localStorage;
		}

		public class LoginRequest {
			public string Email { get; set; } = "";
			public string Password { get; set; } = "";
		}

		public class LoginResponse {
			public string Token { get; set; } = "";
			public DateTime Expires { get; set; }
		}
		public class RegisterRequest {
			public string Email { get; set; } = "";
			public string Password { get; set; } = "";
			public string ConfirmPassword { get; set; } = "";
		}

		public async Task<bool> LoginAsync(string email, string password) {
			var resp = await _http.PostAsJsonAsync("api/auth/login", new LoginRequest {
				Email = email,
				Password = password
			});

			if (!resp.IsSuccessStatusCode)
				return false;

			var data = await resp.Content.ReadFromJsonAsync<LoginResponse>();
			if (data == null || string.IsNullOrWhiteSpace(data.Token))
				return false;

			await _localStorage.SetItemAsync("authToken", data.Token);
			await _localStorage.SetItemAsync("authTokenExpires", data.Expires);

			return true;
		}

		public async Task LogoutAsync() {
			await _localStorage.RemoveItemAsync("authToken");
			await _localStorage.RemoveItemAsync("authTokenExpires");
		}

		public async Task<(bool Ok, string? Error)> RegisterAsync(string email, string password, string confirmPassword) {
			var resp = await _http.PostAsJsonAsync("api/auth/register", new RegisterRequest {
				Email = email,
				Password = password,
				ConfirmPassword = confirmPassword
			});

			if (resp.IsSuccessStatusCode)
				return (true, null);

			var msg = await resp.Content.ReadAsStringAsync();
			if (string.IsNullOrWhiteSpace(msg))
				msg = resp.StatusCode.ToString();

			return (false, msg);
		}

		public async Task<bool> IsAuthenticatedAsync() {
			var token = await _localStorage.GetItemAsync<string>("authToken");
			if (string.IsNullOrWhiteSpace(token))
				return false;

			var expires = await _localStorage.GetItemAsync<DateTime?>("authTokenExpires");
			if (expires is null)
				return true;

			return expires.Value.ToUniversalTime() > DateTime.UtcNow.AddSeconds(30);
		}
	}
}
