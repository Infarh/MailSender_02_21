using System;
using System.Windows;
using MailSender.Data;
using MailSender.Infrastructure;
using MailSender.Infrastructure.Services;
using MailSender.Infrastructure.Services.InDatabase;
using MailSender.Infrastructure.Services.InMemory;
using MailSender.lib;
using MailSender.lib.Entities;
using MailSender.lib.Interfaces;
using MailSender.lib.Service;
using MailSender.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// ReSharper disable AsyncConverter.AsyncWait

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
            services.AddDbContext<MailSenderDb>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("SqlServer")));
            services.AddTransient<DbInitializer>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<StatisticViewModel>();
            services.AddScoped<SchedulerViewModel>();

            //services.AddSingleton<IRepository<Server>, ServersRepository>();
            //services.AddSingleton<IRepository<Sender>, SendersRepository>();
            //services.AddSingleton<IRepository<Recipient>, RecipientsRepository>();
            //services.AddSingleton<IRepository<Message>, MessagesRepository>();

            //services.AddScoped<IRepository<Server>, DbRepository<Server>>();
            //services.AddScoped<IRepository<Sender>, DbRepository<Sender>>();
            //services.AddScoped<IRepository<Recipient>, DbRepository<Recipient>>();
            //services.AddScoped<IRepository<Message>, DbRepository<Message>>();
            //services.AddScoped<IRepository<SchedulerTask>, DbRepository<SchedulerTask>>();
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));

            services.AddScoped<IMailScheduler, MailSchedulerService>();

            services.AddSingleton<IStatistic, InMemoryStatisticService>();

#if DEBUG
            services.AddSingleton<IMailService, DebugMailService>();
#else
            services.AddSingleton<IMailService, SmtpMailService>();
#endif
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var scope = Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                initializer.InitializeAsync().Wait();
            }

            base.OnStartup(e);
        }
    }
}
