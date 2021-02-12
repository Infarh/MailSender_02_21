using System;
using System.Net;
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
            Rotator.Angle += 15;

            var from = new MailAddress("shmachilin@yandex.ru", "Павел");
            var to = new MailAddress("shmachilin@gmail.com", "Павел");

            var message = new MailMessage(from, to);
            message.Subject = "Заголовок";
            message.Body = "Текст письма";

            var client = new SmtpClient("smtp.yandex.ru", 25);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential
            {
                UserName = LoginEdit.Text,
                SecurePassword = PasswordEdit.SecurePassword
                //Password = PasswordEdit.Password
            };

            try
            {
                client.Send(message);

                MessageBox.Show("Почта успешно отправлена!", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SmtpException)
            {
                MessageBox.Show("Ошибка авторизации", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Ошибка адреса сервера", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
