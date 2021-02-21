using System.Linq;
using System.Windows;
using MailSender.Data;
using MailSender.Models;
using MailSender.ViewModels;
using MailSender.Windows;

namespace MailSender
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            //DataContext = new MainWindowViewModel();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void ButtonAddServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ServerEditWindow.Create(
                out var name,
                out var address,
                out var port,
                out var ssl,
                out var login,
                out var password))
                return;
            var server = new Server
            {
                Name = name,
                Address = address,
                Port = port,
                UseSSL = ssl,
                Login = login,
                Password = password,
            };
            TestData.Servers.Add(server);
            ComboBoxServers.Items.Refresh();
            ComboBoxServers.SelectedItem = server;
        }
        private void ButtonEditServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxServers.SelectedItem is Server server))
                return;
            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var ssl = server.UseSSL;
            var login = server.Login;
            var password = server.Password;
            if (!ServerEditWindow.ShowDialog("Редактирование почтового сервера",
                ref name,
                ref address,
                ref port,
                ref ssl,
                ref login,
                ref password))
                return;
            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSSL = ssl;
            server.Login = login;
            server.Password = password;
            ComboBoxServers.Items.Refresh();
            ComboBoxServers.SelectedItem = server;
        }

        private void ButtonDeleteServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ComboBoxServers.SelectedItem is Server server))
                return;
            TestData.Servers.Remove(server);
            ComboBoxServers.Items.Refresh();
            ComboBoxServers.SelectedItem = server;
        }
    }
}
