using System;
using BasicSiteCrawler.Models;

namespace BasicSiteCrawler.Abstractions
{
	public interface IBasicCrawler
	{
		event EventHandler<CrawlingUrlArgs> UrlCrawled;
	}
}