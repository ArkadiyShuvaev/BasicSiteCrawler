using System.Collections.Generic;
using BasicSiteCrawler.Library.Models;

namespace BasicSiteCrawler.Library.Abstractions
{
	public interface IUrlStorage
	{
		bool TryAdd(CrawlingUrlForCreation url);
		IEnumerable<CrawlingUrl> GetUrlsForCrawl();
		void MarkUrlAsCrawled(int id);
		void MarkUrlAsIncorrected(int id);
	}
}