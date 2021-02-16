using System;
using System.Net.Mail;
using System.Windows;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            
            var from = new MailAddress(AppSettings.from, AppSettings.fromName);
            var to = new MailAddress(AppSettings.to, AppSettings.toName);

            var message = new MailMessage(from, to);
            message.Subject = SubjectEdit.Text;
            message.Body = BodyEdit.Text;

            var messageSender = new EmailSenderServiceClass(LoginEdit.Text, PasswordEdit.SecurePassword);
            var error = new MailSendError();
            try
            {
                messageSender.SendMessage(message);
                MessageBox.Show("Почта успешно отправлена!", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SmtpException)
            {
                this.IsEnabled = false;
                error.SetErrorText("Ошибка авторизации");
                error.Show();
                //MessageBox.Show("Ошибка авторизации", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (TimeoutException)
            {
                error.SetErrorText("Ошибка адреса сервера");
                error.Show();
                //MessageBox.Show("Ошибка адреса сервера", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
