namespace BasicSiteCrawler.Models
{
	public class CrawlingUrl
	{
		public int Id { get; set; }
		public string LocalPath { get; set; }
		public string Authority { get; set; }
		public string Scheme { get; set; }
		public bool IsCrawled { get; set; }
		public bool IsProcessed { get; set; }

		public override string ToString()
		{
			return $"{Scheme}://{Authority}{LocalPath}";
		}
	}
}