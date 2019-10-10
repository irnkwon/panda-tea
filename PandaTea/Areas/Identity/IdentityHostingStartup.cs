using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PandaTea.Areas.Identity.IdentityHostingStartup))]
namespace PandaTea.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}