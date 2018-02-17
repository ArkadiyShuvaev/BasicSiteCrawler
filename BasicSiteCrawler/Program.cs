using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using BasicSiteCrawler.Abstractions;
using BasicSiteCrawler.Services;

namespace BasicSiteCrawler
{
	class Program
	{
		private const string ResultLog = "result.log";

		static void Main(string[] args)
		{
			var logger = new DefaultLogger();

#if DEBUG
			//args = new[] { "http://www.bbc.com/" };
			args = new[] { "http://www.vk.com/" };
#endif

			try
			{
				if (!IsParamValid(args))
				{
					const string errMsg = "Please define a start link.";
					Console.WriteLine(errMsg);
					logger.WriteError(errMsg);
					return;
				}


				MainImpl(logger, args[0]);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				logger.WriteError(e.ToString());
				throw;
			}
		}

		private static void MainImpl(ILogger logger, string startLink)
		{
			if (File.Exists(ResultLog))
			{
				File.Delete(ResultLog);
			}


			using (var clientHandler = new HttpClientHandler())
			using (var streamWriter = new StreamWriter(ResultLog))
			{
				var networkProvider = new NetworkProvider(clientHandler);
				IHtmlParser htmlParser = new SimpleHtmlParser();
				var temporaryUrlStorage = new UrlMemoryStorage();

				var crawlerService = new BasicCrawler(networkProvider, logger, htmlParser, 
					temporaryUrlStorage, new SimpleOutputWriter(streamWriter));

				crawlerService.CrawlAndSaveToStream(startLink);
				Console.ReadKey();
			}
		}

		private static bool IsParamValid(string[] args)
		{
			if (args == null || args.Length < 1)
			{
				return false;
			}
			return true;
		}

	}
}
