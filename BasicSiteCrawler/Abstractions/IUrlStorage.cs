using System.Collections.Generic;
using BasicSiteCrawler.Models;

namespace BasicSiteCrawler.Abstractions
{
	public interface IUrlStorage
	{
		CrawlingUrl Add(CrawlingUrlForCreation url);
		CrawlingUrl GetById(int id);
		IEnumerable<CrawlingUrl> GetUncrawledUrls();
		IEnumerable<CrawlingUrl> GetCrawledUrls();
		bool IsUncrawledQueueEmpty { get; }
		bool IsCrawled(int id);
		void MarkUrlAsCrawled(int id);
		void MarkUrlAsProcessed(int id);
	}
}