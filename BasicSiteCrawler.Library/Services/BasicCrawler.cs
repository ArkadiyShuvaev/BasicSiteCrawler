using System;
using System.Linq;
using BasicSiteCrawler.Abstractions;
using BasicSiteCrawler.Models;
using BasicSiteCrawler.Services;
using Microsoft.Extensions.Logging;

namespace BasicSiteCrawler.Library.Services
{
	public sealed class BasicCrawler
	{
		private readonly INetworkProvider _networkProvider;
		private readonly ILogger<BasicCrawler> _logger;
		private readonly IHtmlParser _htmlParser;
		private readonly IUrlStorage _storage;
		public event EventHandler<CrawlingUrlArgs> UrlCrawled = (sender, args) => { };
		
		public BasicCrawler(INetworkProvider networkProvider, ILogger<BasicCrawler> logger, IHtmlParser htmlParser,
			IUrlStorage storage)
		{
			if (networkProvider == null) throw new ArgumentNullException(nameof(networkProvider));
			if (logger == null) throw new ArgumentNullException(nameof(logger));
			if (htmlParser == null) throw new ArgumentNullException(nameof(htmlParser));
			if (storage == null) throw new ArgumentNullException(nameof(storage));
			
			_networkProvider = networkProvider;
			_logger = logger;
			_htmlParser = htmlParser;
			_storage = storage;
		}

		public void ResetSubscriptions()
		{
			UrlCrawled = null;
		}


		public void CrawlUrl(string startingUrl)
		{
			if (startingUrl == null) throw new ArgumentNullException(nameof(startingUrl));

			Uri uri;
			var result = Uri.TryCreate(startingUrl, UriKind.Absolute, out uri);

			if (!result)
			{
				var message = $"The starting url '{startingUrl}' is wrong";
				_logger.LogError(message);
				throw new Exception(message);
			}

			_storage.TryAdd(CrawlingUrlForCreationHelpers.CreateFromUri(uri));
			ProcessUncrawledUrls();
		}

		private void ProcessUncrawledUrls()
		{
			while (_storage.IsUncrawledUrlExist)
			{
				var uncrawledUrls = _storage.GetUncrawledUrls().ToList();

				var items = uncrawledUrls
					.Select(u =>
					{
						var url = u.ToString();
						Uri uri;
						var parseResult = Uri.TryCreate(url, UriKind.Absolute, out uri);

						if (parseResult)
						{
							return new {CrawlingUrl = u, Uri = uri};
						}

						_storage.MarkUrlAsCrawled(u.Id);
						_storage.MarkUrlAsIncorrected(u.Id);

						_logger.LogWarning($"The '{url}' url cannot be processed.");

						return null;
					})
					.Where(u => u != null)
					.ToList();

				var tasks = items.Select(item => new
				{
					CrawledUrl = item.CrawlingUrl,
					PageBodyTask = _networkProvider.GetPageBody(item.Uri)
				});

				foreach (var task in tasks)
				{
					try
					{
						_logger.LogTrace($"Processing '{task.CrawledUrl}' url...");
						var pageBody = task.PageBodyTask.Result;
						ProcessPageBody(pageBody, task.CrawledUrl);
					}
					catch (Exception ex)
					{
						_logger.LogError(ex.ToString());
					}
				}
			}
		}

		private void ProcessPageBody(string pageBody, CrawlingUrl crawlingUrl)
		{
			var relativeUrls = _htmlParser.GetRelativeUrls(pageBody);
			foreach (var relativeUrl in relativeUrls)
			{
				_storage.TryAdd(CrawlingUrlForCreationHelpers.CreateFromLocalPath(crawlingUrl, relativeUrl));
			}

			_storage.MarkUrlAsCrawled(crawlingUrl.Id);
			OnUrlCrawled(new CrawlingUrlArgs { CrawlingUrl = crawlingUrl });
		}

		private void OnUrlCrawled(CrawlingUrlArgs args)
		{
			UrlCrawled.Invoke(this, args);
		}
	}
}
