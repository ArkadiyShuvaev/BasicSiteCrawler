using System.Net.Http;
using System.Threading;
using BasicSiteCrawler.Library.Abstractions;
using BasicSiteCrawler.Library.Models;
using BasicSiteCrawler.Library.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BasicSiteCrawler.Web.Services
{
    public class CrawlMediator
    {
	    private readonly IHubContext<CrawlUrlHub> _crawlUrlHub;
	    private readonly ILogger<CrawlMediator> _logger;
	    private readonly ILoggerFactory _loggerFactory;
	    private BasicCrawler _crawlerService;
	    private CancellationTokenSource _cancellationSource;

	    public CrawlMediator(IHubContext<CrawlUrlHub> crawlUrlHub, ILogger<CrawlMediator> logger, ILoggerFactory loggerFactory)
	    {
		    _crawlUrlHub = crawlUrlHub;
		    _logger = logger;
		    _loggerFactory = loggerFactory;
		}

	    public async void StartCrawl(string startUrlForCrawl)
	    {
		    _cancellationSource = new CancellationTokenSource();

			using (var clientHandler = new HttpClientHandler())
		    {
			    var networkProvider = new NetworkProvider(clientHandler);
			    IHtmlParser htmlParser = new SimpleHtmlParser();
			    var temporaryUrlStorage = new UrlMemoryStorage();

			    _crawlerService = new BasicCrawler(networkProvider, _loggerFactory.CreateLogger<BasicCrawler>(), htmlParser,
				    temporaryUrlStorage);

			    _crawlerService.UrlCrawled += CrawlerServiceOnUrlCrawled;

			    await _crawlerService.CrawUrlAsync(startUrlForCrawl, _cancellationSource.Token);
		    }
		}

	    public void StopCrawl()
	    {
			_logger.LogTrace("Cancel async crawling...");
		    _cancellationSource.Cancel();
			_crawlerService.ResetSubscriptions();
			
	    }

	    private void CrawlerServiceOnUrlCrawled(object sender, CrawlingUrlArgs crawlingUrlArgs)
	    {
			_crawlUrlHub.Clients.All.SendAsync("Send", crawlingUrlArgs.CrawlingUrl.ToString());
		}
    }
}
