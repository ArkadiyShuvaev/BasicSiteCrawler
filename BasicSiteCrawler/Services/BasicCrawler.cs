using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicSiteCrawler.Abstractions;
using BasicSiteCrawler.Models;

namespace BasicSiteCrawler.Services
{
	public class BasicCrawler
	{
		private readonly INetworkProvider _networkProvider;
		private readonly ILogger _logger;
		private readonly IHtmlParser _htmlParser;
		private readonly IUrlStorage _storage;
		private string _currentScheme;
		private readonly IOutputWriter _simpleOutputWriter;

		public BasicCrawler(INetworkProvider networkProvider, ILogger logger, IHtmlParser htmlParser, 
			IUrlStorage storage, IOutputWriter streamOutputWriter)
		{
			if (networkProvider == null) throw new ArgumentNullException(nameof(networkProvider));
			if (logger == null) throw new ArgumentNullException(nameof(logger));
			if (htmlParser == null) throw new ArgumentNullException(nameof(htmlParser));
			if (storage == null) throw new ArgumentNullException(nameof(storage));
			if (streamOutputWriter == null) throw new ArgumentNullException(nameof(streamOutputWriter));

			_networkProvider = networkProvider;
			_logger = logger;
			_htmlParser = htmlParser;
			_storage = storage;
			_simpleOutputWriter = streamOutputWriter;
		}


		public void CrawlAndSaveToStream(string startingUrl)
		{
			if (startingUrl == null) throw new ArgumentNullException(nameof(startingUrl));

			Uri uri;
			var result = Uri.TryCreate(startingUrl, UriKind.Absolute, out uri);

			if (!result)
			{
				var message = $"The starting url '{startingUrl}' is wrong";
				_logger.WriteError(message);
				throw new Exception(message);
			}
			_currentScheme = uri.Scheme;

			ProcessRootUri(uri);
			ProcessUnfinishedUrls();
			

			WriteCrawlResultsToStream();
		}

		private void WriteCrawlResultsToStream()
		{
			var result = _storage.GetUrlsAndMarkAsSaved(_currentScheme);

			foreach (var item in result)
			{
				_simpleOutputWriter.WriteLine(item);
			}
		}
		

		private void ProcessRootUri(Uri uri)
		{
			List<string> childrenLinks;
			var result = TryGetLinksFromRemotePage(uri, out childrenLinks);

			if (!result)
			{
				return;
			}

			foreach (var localLink in childrenLinks)
			{
				AddLinkToStorage(uri, localLink);
			}

			AddLinkToStorage(uri, uri.LocalPath);
		}
		
		private void ProcessUnfinishedUrls()
		{
			while (_storage.AreUncrawledUrlsExist())
			{
				var unprocessedUrls = _storage.GetUncrawledUrls().ToList();
				if (unprocessedUrls.Count == 0)
				{
					return;
				}

				var tasks = new List<Task>(unprocessedUrls.Count);

				foreach (var unprocessedUrl in unprocessedUrls)
				{
					tasks.Add(Task.Run(() => { ProcessCrawlingUrl(unprocessedUrl); }));
				}

				Task.WaitAll(tasks.ToArray());

			}
			
		}

		private void ProcessCrawlingUrl(CrawlingUrl crawlingUrl)
		{
			if (_storage.IsCrawled(crawlingUrl.Id))
			{
				return;
			}

			var url = crawlingUrl.ConvertToFullUrl(_currentScheme);
			Uri uri;
			var result = Uri.TryCreate(url, UriKind.Absolute, out uri);

			if (!result)
			{
				_logger.WriteWarning($"The '{url}' url cannot be processed.");
			}
			
			List<string> childrenLinks;
			var getLinksResult = TryGetLinksFromRemotePage(uri, out childrenLinks);

			if (getLinksResult)
			{
				foreach (var localLink in childrenLinks)
				{
					AddLinkToStorage(uri, localLink);
				}
			}

			_storage.MarkUrlAsCrawled(crawlingUrl.Id);

			var outputUrl = _storage.GetUrlAndMarkAsSaved(_currentScheme, crawlingUrl.Id);
			_simpleOutputWriter.WriteLine(outputUrl);
		}
		
		private void AddLinkToStorage(Uri uri, string localLink)
		{
			_storage.Add(uri.Authority + localLink);
		}

		private bool TryGetLinksFromRemotePage(Uri uri, out List<string> links)
		{
			var result = false;
			links = new List<string>();

			try
			{
				var pageBody = _networkProvider.GetPageBody(uri);
				var bodyUrls = _htmlParser.GetUrls(pageBody.Result);

				links = bodyUrls.Where(u => u.StartsWith("/")).ToList();
				result = true;
			}
			catch (Exception e)
			{
				_logger.WriteError($"The '{uri.AbsoluteUri}' cannot be processed due to the reason: {e}");
			}

			
			return result;
		}
	}
}