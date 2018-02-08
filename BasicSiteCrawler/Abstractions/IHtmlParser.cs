using System.Collections.Generic;

namespace BasicSiteCrawler.Abstractions
{
	public interface IHtmlParser
	{
		List<string> GetUrls(string pageBody);
	}
}
