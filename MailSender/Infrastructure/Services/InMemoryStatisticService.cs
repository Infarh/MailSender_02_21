using System;
using System.Diagnostics;
using MailSender.Data;
using MailSender.lib.Interfaces;

namespace MailSender.Infrastructure.Services
{
    internal class InMemoryStatisticService : IStatistic
    {
        private int _SendedMailsCount;

        public int SendedMailsCount => _SendedMailsCount;
        public event EventHandler SendedMailsCountChanged;


        public void MailSended()
        {
            _SendedMailsCount++;
            SendedMailsCountChanged?.Invoke(this, EventArgs.Empty);
        }

        public int SendersCount => TestData.Senders.Count;
        public int RecipientsCount => TestData.Recipients.Count;

        private readonly Stopwatch _StopwatchTimer = Stopwatch.StartNew();
        public TimeSpan UpTime => _StopwatchTimer.Elapsed;
    }
}
