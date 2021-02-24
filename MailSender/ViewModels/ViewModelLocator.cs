using Microsoft.Extensions.DependencyInjection;

namespace MailSender.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel
        {
            get
            {
                var model = App.Services.GetRequiredService<MainWindowViewModel>();
                return model;
            }
        }

        public StatisticViewModel Statistic => App.Services.GetRequiredService<StatisticViewModel>();
    }
}
