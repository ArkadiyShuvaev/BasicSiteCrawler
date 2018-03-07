using System;
using System.IO;
using BasicSiteCrawler.Abstractions;

namespace BasicSiteCrawler.Services
{
	public class SimpleOutputWriter : IOutputWriter
	{
		private readonly StreamWriter _streamWriter;

		public SimpleOutputWriter(StreamWriter streamWriter)
		{
			_streamWriter = streamWriter;
		}

		public void WriteLine(string result)
		{
			if (result == null) throw new ArgumentNullException(nameof(result));
			
			_streamWriter.WriteLine(result);
			_streamWriter.Flush();

			Console.WriteLine(result);
		}
	}
}