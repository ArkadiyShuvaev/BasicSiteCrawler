using Microsoft.AspNetCore.Mvc;

namespace BasicSiteCrawler.Web.Controllers
{
	public class HomeController : Controller
	{
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}
	}
}