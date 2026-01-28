using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers {
	public class LabController : Controller {
		public IActionResult Info() {
			var model = new LabInfoViewModel {
				LabNumber = 1,
				Topic = "Вступ до ASP.NET Core",
				Purpose = "Ознайомитися з основними принципами роботи .NET, навчитися налаштовувати середовище розробки та встановлювати необхідні компоненти, " +
							"набути навичок створення рішень та проектів різних типів, набути навичок обробки запитів з вико-ристанням middleware.",
				StudentName = "Denis Andriiuk"
			};

			return View(model);
		}
	}
}
