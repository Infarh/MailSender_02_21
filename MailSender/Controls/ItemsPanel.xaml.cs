using System.Windows;

namespace MailSender.Controls
{
    public partial class ItemsPanel
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(ItemsPanel),
                new PropertyMetadata("(Название)", OnTitleChanged));

        private static void OnTitleChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
        {

        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ItemsPanel() => InitializeComponent();
    }
}
