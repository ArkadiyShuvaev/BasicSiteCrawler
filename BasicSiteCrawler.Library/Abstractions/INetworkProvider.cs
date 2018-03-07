using System;
using System.Threading.Tasks;

namespace BasicSiteCrawler.Abstractions
{
	public interface INetworkProvider
	{
		Task<string> GetPageBody(Uri uri);
	}
}