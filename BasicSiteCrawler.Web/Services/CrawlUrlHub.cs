using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BasicSiteCrawler.Web.Services
{
    public class CrawlUrlHub : Hub
    {
	    public async Task Send(string message)
	    {
		    await Clients.All.SendAsync("Send", message);
	    }
    }
}
