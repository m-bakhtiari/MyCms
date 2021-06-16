using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyCms.WebApp.Models;
using MyCms.WebApp.Services;
using MyCms.WebApp.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace MyCms.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Const.ApiSiteUrl) });
            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddFileReaderService();

            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
