using System.Windows;
using MailSender.ViewModels;

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
    }
}
