using Microsoft.AspNetCore.Mvc;

namespace BasicSiteCrawler.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost("StartCrawl")]
		public IActionResult StartCrawl([FromBody] StartingUrlDto startingUrl)
		{
			if (startingUrl.StartingUrl == "") return BadRequest();
			return Ok(true);
		}
	}

	public class StartingUrlDto
	{
		public string StartingUrl { get; set; }
	}
}