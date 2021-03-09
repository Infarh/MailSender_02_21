using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailSender.lib.Entities;
using MailSender.lib.Interfaces;

namespace MailSender.lib
{
    public class MailSchedulerService : IMailScheduler
    {
        public event EventHandler TaskCompleted;

        protected virtual void OnTaskCompleted(EventArgs e)
        {
            TaskCompleted?.Invoke(this, e);
        }

        private readonly IMailService _MailService;
        private readonly IRepository<SchedulerTask> _Tasks;
        private volatile Task _ProcessTask;

        private readonly object _SyncRoot = new object();
        private CancellationTokenSource _Cancellation;

        public bool Enable
        {
            get => _ProcessTask != null;
            set
            {
                if(value)
                    Start();
                else
                    Stop();
            }
        }

        public MailSchedulerService(IMailService MailService, IRepository<SchedulerTask> Tasks)
        {
            _MailService = MailService;
            _Tasks = Tasks;
        }

        public void Start()
        {
            if(_ProcessTask != null) return;
            lock (_SyncRoot)
            {
                if (_ProcessTask != null) return;
                _Cancellation = new CancellationTokenSource();
                _ProcessTask = ProcessTasksAsync(_Cancellation.Token);
            }
        }

        public void Stop()
        {
            if (_ProcessTask is null) return;
            lock (_SyncRoot)
            {
                if (_ProcessTask is null) return;
                _Cancellation.Cancel();
                _ProcessTask = null;
            }
        }

        public SchedulerTask Plan(DateTime Time, Server server, Sender sender, IEnumerable<Recipient> recipients, Message message)
        {
            var is_started = Enable;
            Enable = false;

            var new_task = new SchedulerTask
            {
                Time = Time,
                Server = server,
                Sender = sender,
                Recipients = recipients.ToArray(),
                Message = message
            };

            _Tasks.Add(new_task);

            Enable = is_started;
            return new_task;
        }

        private async Task ProcessTasksAsync(CancellationToken Cancel)
        {
            // пропущенные задачи
            var tasks = _Tasks.GetAll().Where(task => task.Time <= DateTime.Now);

            await Task.WhenAll(tasks.Select(t => ProcessTaskAsync(t, Cancel))).ConfigureAwait(false);

            var next_task = _Tasks.GetAll().FirstOrDefault(t => t.Time > DateTime.Now);
            if (next_task is null)
            {
                _ProcessTask = null;
                return;
            }

            await Task.Delay(next_task.Time - DateTime.Now, Cancel).ConfigureAwait(false);

            await ProcessTasksAsync(Cancel).ConfigureAwait(false);
        }

        private async Task ProcessTaskAsync(SchedulerTask task, CancellationToken Cancel)
        {
            var server = task.Server;
            var sender = _MailService.GetSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);

            await sender.SendParallelAsync(
                task.Sender.Address, 
                task.Recipients.Select(r => r.Address), 
                task.Message.Title, 
                task.Message.Text, 
                Cancel)
               .ConfigureAwait(false);

            OnTaskCompleted(EventArgs.Empty);
        }
    }
}