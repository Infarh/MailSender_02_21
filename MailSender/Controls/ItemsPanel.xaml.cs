using System.Windows;
using System.Windows.Controls;

namespace MailSender.Controls
{
    public partial class ItemsPanel : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(ItemsPanel),
                new PropertyMetadata(default(string)));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ItemsPanel() => InitializeComponent();
    }
}
