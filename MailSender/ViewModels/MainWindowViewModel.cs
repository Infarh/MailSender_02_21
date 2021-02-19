using System.Collections.Generic;
using System.Collections.ObjectModel;
using MailSender.Infrastructure;
using MailSender.lib.ViewModels.Base;
using MailSender.Models;

namespace MailSender.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly ServersRepository _Servers;

        private string _Title = "Рассыльщик";

        public string Title { get => _Title; set => Set(ref _Title, value); }

        private string _Status = "Готов!";

        public string Status { get => _Status; set => Set(ref _Status, value); }

        public ObservableCollection<Server> Servers { get; } = new ();

        public MainWindowViewModel(ServersRepository Servers)
        {
            _Servers = Servers;
        }

        private void LoadServers()
        {
            foreach (var server in _Servers.GetAll())
                Servers.Add(server);
        }
    }
}
