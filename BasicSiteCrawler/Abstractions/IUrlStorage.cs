using System.Collections.Generic;
using BasicSiteCrawler.Models;

namespace BasicSiteCrawler.Abstractions
{
	public interface IUrlStorage
	{
		CrawlingUrl Add(string url);
		IEnumerable<CrawlingUrl> GetUncrawledUrls();
		bool AreUncrawledUrlsExist();
		bool IsCrawled(int id);
		void MarkUrlAsCrawled(int id);
		string GetUrlAndMarkAsSaved(string scheme, int id);
		IEnumerable<string> GetUrlsAndMarkAsSaved(string currentScheme);
	}
}