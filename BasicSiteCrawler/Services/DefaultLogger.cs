using System;
using System.Diagnostics;
using BasicSiteCrawler.Abstractions;

namespace BasicSiteCrawler.Services
{
	public class DefaultLogger : ILogger
	{
		public void WriteWarning(string warningMessage)
		{
			WriteEntry(warningMessage, "Warning");
		}

		public void WriteError(string errorMessage)
		{
			WriteEntry(errorMessage, "Error");
		}

		public void WriteInfo(string infoMessage)
		{
			WriteEntry(infoMessage, "Information");
		}

		private static void WriteEntry(string message, string type)
		{
			Trace.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {type}: {message}");
			Trace.Flush();
		}
	}
}
