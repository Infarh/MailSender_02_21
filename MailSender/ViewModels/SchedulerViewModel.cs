using MailSender.lib.Interfaces;

namespace MailSender.ViewModels
{
    internal class SchedulerViewModel
    {
        private readonly IMailScheduler _Scheduler;

        public SchedulerViewModel(IMailScheduler Scheduler) => _Scheduler = Scheduler;


    }
}
