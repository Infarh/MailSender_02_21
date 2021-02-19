using System;

namespace MailSender.lib.Interfaces
{
    public interface IStatistic
    {
        int SendedMailsCount { get; }
        event EventHandler SendedMailsCountChanged;

        int SendersCount { get; }
        int RecipientsCount { get; }
        TimeSpan UpTime { get; }
        void MailSended();
    }
}
