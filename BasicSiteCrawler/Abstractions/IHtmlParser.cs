using System.Collections.Generic;

namespace BasicSiteCrawler.Abstractions
{
	public interface IHtmlParser
	{
		List<string> GetRelativeUrls(string pageBody);
	}
}
