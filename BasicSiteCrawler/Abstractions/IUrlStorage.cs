using System.Collections.Generic;
using BasicSiteCrawler.Models;

namespace BasicSiteCrawler.Abstractions
{
	public interface IUrlStorage
	{
		CrawlingUrl Add(string url);
		IEnumerable<CrawlingUrl> GetUnprocessedUrls();
		bool IsProcessed(int id);
		void MarkAsProcessed(int id);
		string GetUrlAndMarkAsSaved(string scheme, int id);
		IEnumerable<string> GetUrlsAndMarkAsSaved(string currentScheme);
	}
}