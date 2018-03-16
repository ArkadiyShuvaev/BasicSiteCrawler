using System.Collections.Generic;

namespace BasicSiteCrawler.Library.Abstractions
{
	public interface IHtmlParser
	{
		List<string> GetRelativeUrls(string pageBody);
	}
}
