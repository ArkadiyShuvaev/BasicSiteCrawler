using System;
using BasicSiteCrawler.Models;

namespace BasicSiteCrawler.Services
{
	public static class CrawlingUrlForCreationHelpers
	{
		public static CrawlingUrlForCreation CreateFromLocalPath(Uri uri, string localPath)
		{
			return new CrawlingUrlForCreation
			{
				Url = (uri.Authority + localPath).TrimEnd('/')
			};
		}

		public static CrawlingUrlForCreation CreateFromUri(Uri uri)
		{
			return new CrawlingUrlForCreation
			{
				Url = (uri.Authority + uri.LocalPath).TrimEnd('/')
			};
		}
	}
}