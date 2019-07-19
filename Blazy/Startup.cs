using Blazor.Extensions;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blazy
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<HubConnectionBuilder>();
            //services.AddSignalR();

            //services.AddSignalR().AddAzureSignalR("Endpoint=https://blazort.service.signalr.net;AccessKey=S6vcAyR1NQ7/29x3ltvEEiZxHKaX5YEpBNWi3FpZvv0=;Version=1.0;");
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
