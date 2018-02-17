using System;
using BasicSiteCrawler.Models;

namespace BasicSiteCrawler.Services
{
	public static class CrawlingUrlForCreationHelpers
	{
		public static CrawlingUrlForCreation CreateFromLocalPath(CrawlingUrl uri, string localPath)
		{
			return new CrawlingUrlForCreation
			{
				LocalPath = localPath,
				Authority = uri.Authority,
				Scheme = uri.Scheme
			};
		}

		public static CrawlingUrlForCreation CreateFromUri(Uri uri)
		{
			return new CrawlingUrlForCreation
			{
				LocalPath = uri.LocalPath,
				Authority = uri.Authority,
				Scheme = uri.Scheme
			};
		}
	}
}