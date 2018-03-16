using System;
using System.Collections.Generic;
using System.Linq;
using BasicSiteCrawler.Library.Abstractions;
using HtmlAgilityPack;

namespace BasicSiteCrawler.Library.Services
{
	public class SimpleHtmlParser : IHtmlParser
	{

		public List<string> GetRelativeUrls(string pageAsHtml)
		{
			var result = new List<string>();

			var document = new HtmlDocument();
			
			document.LoadHtml(pageAsHtml);
			var nodes = document.DocumentNode.Descendants("a");

			foreach (var elem in nodes)
			{
				var hrefValue = SafeGetAttributeValue(elem, "href");
				
				result.Add(hrefValue);
			}

			return result.Where(u => u.StartsWith("/")).ToList();
		}

		private static string SafeGetAttributeValue(HtmlNode elem, string attrName)
		{
			try
			{
				var src = elem.ChildAttributes(attrName).First();
				return src.Value ?? String.Empty;
			}
			catch (InvalidOperationException)
			{
				return String.Empty;
			}
		}
	
	}
}
