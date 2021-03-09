using System;
using System.Collections.Generic;
using MailSender.lib.Entities;

namespace MailSender.lib.Interfaces
{
    public interface IMailScheduler
    {
        event EventHandler TaskCompleted;

        bool Enable { get; set; }

        void Start();
        void Stop();

        SchedulerTask Plan(DateTime Time, Server server, Sender sender, IEnumerable<Recipient> recipients, Message message);
    }
}
