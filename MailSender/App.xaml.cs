using System;
using MailSender.Infrastructure;
using MailSender.Infrastructure.Services;
using MailSender.lib.Interfaces;
using MailSender.lib.Service;
using MailSender.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MailSender
{
    public partial class App
    {
        private static IHost __Hosting;

        public static IHost Hosting => __Hosting
            ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration(opt => opt.AddJsonFile("appsettings.json", false, true))
               .ConfigureServices(ConfigureServices)
            ;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<StatisticViewModel>();

            services.AddSingleton<ServersRepository>();
            services.AddSingleton<IStatistic, InMemoryStatisticService>();
            services.AddSingleton<IMailService, DebugMailService>();
        }
    }
}
