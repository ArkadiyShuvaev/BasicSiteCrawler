﻿using System.ComponentModel.DataAnnotations;
using BasicSiteCrawler.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BasicSiteCrawler.Web.Controllers
{
	[Route("api/crawl")]
	public class CrawlUrlController : Controller
	{
		private readonly IHubContext<CrawlUrlHub> _crawlUrlHub;
		private readonly CrawlMediator _crawlMediator;

		public CrawlUrlController(IHubContext<CrawlUrlHub> crawlUrlHub, CrawlMediator crawlMediator)
		{
			_crawlUrlHub = crawlUrlHub;
			_crawlMediator = crawlMediator;
		}
	    [HttpPost("StartCrawl")]
		public IActionResult StartCrawl([FromBody] StartingUrlDto startingUrl)
		{
			if (ModelState.IsValid)
			{
				//_crawlUrlHub.Clients.All.SendAsync("Send", startingUrl.StartingUrl);
				_crawlMediator.StartCrawl(startingUrl.StartingUrl);

				return Ok(true);
			}

			return BadRequest(ModelState);
		}


	}

	public class StartingUrlDto
	{
		[Required]
		public string StartingUrl { get; set; }
	}
}