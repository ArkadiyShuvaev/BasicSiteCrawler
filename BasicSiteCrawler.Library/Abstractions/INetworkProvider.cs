using System;
using System.Threading.Tasks;

namespace BasicSiteCrawler.Library.Abstractions
{
	public interface INetworkProvider
	{
		Task<string> GetPageBody(Uri uri);
	}
}