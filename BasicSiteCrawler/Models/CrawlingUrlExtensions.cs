using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSiteCrawler.Models
{
	public static class CrawlingUrlExtensions
	{
		public static string ConvertToFullUrl(this CrawlingUrl crawlingUrl, string scheme)
		{
			return scheme + "://" + crawlingUrl.Url;
		}
	}
}
