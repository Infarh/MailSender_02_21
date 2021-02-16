using System.Windows;

namespace MailSender
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
