using System.Windows;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MailSendError.xaml
    /// </summary>
    public partial class MailSendError : Window
    {
        public MailSendError()
        {
            InitializeComponent();
        }

        private void buttonCloseError_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void SetErrorText(string text)
        {
            labelErrorText.Content = text;
        }

    }
}
