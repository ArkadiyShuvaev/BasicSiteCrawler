using System.Collections.Generic;
using BasicSiteCrawler.Models;

namespace BasicSiteCrawler.Abstractions
{
	public interface IUrlStorage
	{
		bool TryAdd(CrawlingUrlForCreation url);
		IEnumerable<CrawlingUrl> GetUncrawledUrls();
		bool IsUncrawledUrlExist { get; }
		void MarkUrlAsCrawled(int id);
		//void MarkUrlAsProcessed(int id);
		void MarkUrlAsIncorrected(int id);
	}
}