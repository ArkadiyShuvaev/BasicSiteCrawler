using System.ComponentModel.DataAnnotations;
using BasicSiteCrawler.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BasicSiteCrawler.Web.Controllers
{
	[Route("api/crawl")]
	public class CrawlUrlController : Controller
	{
		private readonly IHubContext<CrawlUrlHub> _crawlUrlHub;
		private readonly CrawlMediator _crawlMediator;
		private readonly ILogger<CrawlUrlController> _logger;

		public CrawlUrlController(IHubContext<CrawlUrlHub> crawlUrlHub, CrawlMediator crawlMediator, ILogger<CrawlUrlController> logger)
		{
			_crawlUrlHub = crawlUrlHub;
			_crawlMediator = crawlMediator;
			_logger = logger;
		}
	    [HttpPost("StartCrawl")]
		public IActionResult StartCrawl([FromBody] StartingUrlDto startingUrl)
		{
			if (ModelState.IsValid)
			{
				_logger.LogDebug($"Starting crawling with {startingUrl.StartingUrl}...");
				_crawlMediator.StartCrawl(startingUrl.StartingUrl);

				return Ok(true);
			}

			return BadRequest(ModelState);
		}

		[HttpPost("StopCrawl")]
		public IActionResult StopCrawl()
		{
			_crawlMediator.StopCrawl();
			return Ok(true);
		}


	}

	public class StartingUrlDto
	{
		[Required]
		public string StartingUrl { get; set; }
	}
}
