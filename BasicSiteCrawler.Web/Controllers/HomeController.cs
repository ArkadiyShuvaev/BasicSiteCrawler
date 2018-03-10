using Microsoft.AspNetCore.Mvc;

namespace BasicSiteCrawler.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
