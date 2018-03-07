namespace BasicSiteCrawler.Abstractions
{
	public interface ILogger
	{
		void WriteWarning(string warningMessage);
		void WriteError(string errorMessage);
		void WriteInfo(string infoMessage);
	}
}