using CalendarApplication.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using CalendarApplication.DAL.Repositorys;
using Microsoft.Extensions.Hosting;
using CalendarApplication.ViewModels;

namespace CalendarApplication
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                   .ConfigureServices((context, services) =>
                   {
                       ConfigureServices(context.Configuration, services);
                   })
                   .Build();
        }

        private void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=CalendarDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<CalendarContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<ICalendarRepository, CalendarRepository>();
            services.AddSingleton<CalendarViewModel>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<CalendarViewModel>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}