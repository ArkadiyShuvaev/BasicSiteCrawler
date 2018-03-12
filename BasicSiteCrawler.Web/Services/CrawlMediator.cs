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
	    private BasicCrawler _crawlerService;

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

			    _crawlerService = new BasicCrawler(networkProvider, logger, htmlParser,
				    temporaryUrlStorage);

			    _crawlerService.UrlCrawled += CrawlerServiceOnUrlCrawled;

			    _crawlerService.CrawlUrl(startUrlForCrawl);
				
		    }
		}

	    public void StopCrawl()
	    {
			_crawlerService.ResetSubscriptions();
	    }

	    private void CrawlerServiceOnUrlCrawled(object sender, CrawlingUrlArgs crawlingUrlArgs)
	    {
			_crawlUrlHub.Clients.All.SendAsync("Send", crawlingUrlArgs.CrawlingUrl.ToString());
		}
    }
}
