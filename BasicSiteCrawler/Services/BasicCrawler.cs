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
		//private string _currentScheme;
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
			
			ProcessRootUri(uri);
			ProcessUnfinishedUrls();
			
			
		}
		

		private void ProcessRootUri(Uri uri)
		{
			if (!ProcessUriAndReturnOperationResultAsBool(uri))
			{
				return;
			}

			var crawlingUrl = _storage.Add(CrawlingUrlForCreationHelpers.CreateFromUri(uri));
			if (crawlingUrl != null)
			{
				_storage.MarkUrlAsCrawled(crawlingUrl.Id);
				WriteToStreamAndMarkAsProcessed(crawlingUrl.Id);
			}
			
		}
		
		private void ProcessCrawlingUrl(CrawlingUrl crawlingUrl)
		{
			if (_storage.IsCrawled(crawlingUrl.Id))
			{
				return;
			}

			var url = crawlingUrl.ToString();
			Uri uri;
			var parseResult = Uri.TryCreate(url, UriKind.Absolute, out uri);

			if (!parseResult)
			{
				_logger.WriteWarning($"The '{url}' url cannot be processed.");
				return;
			}

			if (!ProcessUriAndReturnOperationResultAsBool(uri))
			{
				return;
			}

			_storage.MarkUrlAsCrawled(crawlingUrl.Id);

			WriteToStreamAndMarkAsProcessed(crawlingUrl.Id);
		}

		private bool ProcessUriAndReturnOperationResultAsBool(Uri uri)
		{
			List<string> localPaths;
			var isGetLinksSucceeded = TryGetLinksFromRemotePage(uri, out localPaths);

			if (!isGetLinksSucceeded)
			{
				_logger.WriteWarning($"The '{uri.ToString()}' url cannot be processed.");
				return false;
			}

			foreach (var localPath in localPaths)
			{
				_storage.Add(CrawlingUrlForCreationHelpers.CreateFromLocalPath(uri, localPath));
			}
			return true;
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
		

		private void WriteToStreamAndMarkAsProcessed(int id)
		{
			var outputUrl = _storage.GetById(id);
			_simpleOutputWriter.WriteLine(outputUrl.ToString());
			_storage.MarkUrlAsProcessed(id);
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