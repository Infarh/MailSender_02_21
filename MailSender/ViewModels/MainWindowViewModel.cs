using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MailSender.Infrastructure;
using MailSender.Infrastructure.Services;
using MailSender.Infrastructure.Services.InMemory;
using MailSender.lib.Commands;
using MailSender.lib.Interfaces;
using MailSender.lib.ViewModels.Base;
using MailSender.Models;
using MailSender.Models.Base;

namespace MailSender.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Server> _Servers;
        private readonly IRepository<Sender> _Senders;
        private readonly IRepository<Recipient> _Recipients;
        private readonly IRepository<Message> _Messages;


        private readonly IMailService _MailService;

        private string _Title = "Рассыльщик";

        public string Title { get => _Title; set => Set(ref _Title, value); }

        private string _Status = "Готов!";

        public string Status { get => _Status; set => Set(ref _Status, value); }

        public ObservableCollection<Server> Servers { get; } = new ();
        public ObservableCollection<Recipient> Recipients { get; } = new();
        public ObservableCollection<Sender> Senders { get; } = new();
        public ObservableCollection<Message> Messages { get; } = new();

        #region Команды

        private ICommand _LoadServersCommand;

        public ICommand LoadServersCommand => _LoadServersCommand
            ??= new LambdaCommand(OnLoadServersCommandExecuted, CanLoadServersCommandExecute);

        private bool CanLoadServersCommandExecute(object p) => Servers.Count == 0;

        private void OnLoadServersCommandExecuted(object p)
        {
            LoadServers();
        }

        private ICommand _SendEmailCommand;

        public ICommand SendEmailCommand => _SendEmailCommand
            ??= new LambdaCommand(OnSendEmailCommandExecuted, CanSendEmailCommandExecute);

        private bool CanSendEmailCommandExecute(object p) => Servers.Count == 0;

        private void OnSendEmailCommandExecuted(object p)
        {
            _MailService.SendEmail("Ивнов", "Петров", "Тема", "Тело письма");
        }

        #endregion

        public MainWindowViewModel(
            IRepository<Server> Servers,
            IRepository<Sender> Senders,
            IRepository<Recipient> Recipients,
            IRepository<Message> Messages,
            IMailService MailService)
        {
            _Servers = Servers;
            _Senders = Senders;
            _Recipients = Recipients;
            _Messages = Messages;

            _MailService = MailService;
        }

        private static void Load<T>(ObservableCollection<T> collection, IRepository<T> rep) where T : Entity
        {
            collection.Clear();
            foreach (var item in rep.GetAll())
                collection.Add(item);
        }

        private void LoadServers()
        {
            Load(Servers, _Servers);
            Load(Recipients, _Recipients);
            Load(Senders, _Senders);
            Load(Messages, _Messages);
        }
    }
}
