using Microsoft.AspNetCore.Mvc;


namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			// If you omit the view name here, it will use the name of this method (Index). In this case, you'd just write "return View();" or "return new ViewResult();"
			return new ViewResult { ViewName = "Index" };
		}
	}
}
