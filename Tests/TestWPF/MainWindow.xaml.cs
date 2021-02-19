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

            using var message = new MailMessage(from, to)
            {
                Subject = "Заголовок", 
                Body = "Текст письма"
            };

            using var client = new SmtpClient("smtp.yandex.ru", 225)
            {
                Timeout = 400,
                EnableSsl = true,
                Credentials = new NetworkCredential
                {
                    UserName = LoginEdit.Text,
                    SecurePassword = PasswordEdit.SecurePassword
                }
            };

            try
            {
                client.Send(message);
                MessageBox.Show("Почта успешно отправлена!", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SmtpException error) when(error.Message == "The operation has timed out.")
            {
                MessageBox.Show("Ошибка адреса сервера", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SmtpException)
            {
                MessageBox.Show("Ошибка авторизации", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
