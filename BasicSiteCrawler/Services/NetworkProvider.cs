using System;
using System.Net.Http;
using System.Threading.Tasks;
using BasicSiteCrawler.Abstractions;

namespace BasicSiteCrawler.Services
{
	public class NetworkProvider : INetworkProvider
	{
		private readonly HttpMessageHandler _httpMessageHandler;
		private const bool DisposeHttpMessageHandler = false; //Must be disposed by calling code

		public NetworkProvider(HttpMessageHandler httpMessageHandler)
		{
			if (httpMessageHandler == null) throw new ArgumentNullException(nameof(httpMessageHandler));
			_httpMessageHandler = httpMessageHandler;
		}
		public async Task<string> GetPageBody(Uri uri)
		{
			if (uri == null) throw new ArgumentNullException(nameof(uri));

			using (var httpClient = new HttpClient(_httpMessageHandler, DisposeHttpMessageHandler))
			{
				return await httpClient.GetStringAsync(uri);
			}
		}
	}
}
