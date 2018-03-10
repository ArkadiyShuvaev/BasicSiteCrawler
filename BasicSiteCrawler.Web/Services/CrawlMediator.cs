using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BasicSiteCrawler.Abstractions;
using BasicSiteCrawler.Models;
using BasicSiteCrawler.Services;
using Microsoft.AspNetCore.SignalR;

namespace BasicSiteCrawler.Web.Services
{
    public class CrawlMediator
    {
	    private readonly IHubContext<CrawlUrlHub> _crawlUrlHub;

	    public CrawlMediator(IHubContext<CrawlUrlHub> crawlUrlHub)
	    {
		    _crawlUrlHub = crawlUrlHub;
			
	    }

	    public void StartCrawl(string startUrlForCrawl)
	    {
		    var logger = new DefaultLogger();
			using (var clientHandler = new HttpClientHandler())
		    {
			    var networkProvider = new NetworkProvider(clientHandler);
			    IHtmlParser htmlParser = new SimpleHtmlParser();
			    var temporaryUrlStorage = new UrlMemoryStorage();

			    var crawlerService = new BasicCrawler(networkProvider, logger, htmlParser,
				    temporaryUrlStorage);

			    crawlerService.UrlCrawled += CrawlerServiceOnUrlCrawled;

			    crawlerService.CrawlUrl(startUrlForCrawl);
				
		    }
		}

	    private void CrawlerServiceOnUrlCrawled(object sender, CrawlingUrlArgs crawlingUrlArgs)
	    {
			_crawlUrlHub.Clients.All.SendAsync("Send", crawlingUrlArgs.CrawlingUrl.ToString());
		}
    }
}
