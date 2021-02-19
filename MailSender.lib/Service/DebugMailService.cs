using System.Diagnostics;
using MailSender.lib.Interfaces;

namespace MailSender.lib.Service
{
    public class DebugMailService : IMailService
    {
        private readonly IStatistic _Statistic;

        public DebugMailService(IStatistic Statistic) => _Statistic = Statistic;

        public void SendEmail(string From, string To, string Title, string Body)
        {
            Debug.WriteLine($"Отправка письма от {From} к {To}: {Title} - {Body}");
            _Statistic.MailSended();
        }
    }
}
