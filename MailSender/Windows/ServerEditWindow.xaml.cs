using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailSender.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServerEditWindow.xaml
    /// </summary>
    public partial class ServerEditWindow : Window
    {
        public ServerEditWindow()
        {
            InitializeComponent();
        }



        /// <summary> Закрытие окна </summary>
        private void WindowButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }
        /// <summary> Перед вводом текста в окно для ввода цифры </summary>
        private void TextBoxServerPort_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox) || string.IsNullOrEmpty(textBox.Text)) return;
            e.Handled = !int.TryParse(textBox.Text, out _);
        }
    }
}
