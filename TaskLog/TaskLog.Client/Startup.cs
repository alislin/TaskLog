using Blazor.IndexedDB.Framework;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskLog.Client.Data;
using TaskLog.Client.Services;
using Thunder.Blazor.Services;

namespace TaskLog.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddComponentServiceScoped()
            .AddBlazoredLocalStorage()
            .AddScoped<IIndexedDbFactory, IndexedDbFactory>()
            .AddScoped<IStorage, IndexedDbStorage>()
            .AddScoped<LocalService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
