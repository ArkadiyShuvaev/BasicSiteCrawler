namespace BasicSiteCrawler.Models
{
	public class CrawlingUrl
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public bool IsCrawled { get; set; }
		public bool IsSaved { get; set; }
	}
}