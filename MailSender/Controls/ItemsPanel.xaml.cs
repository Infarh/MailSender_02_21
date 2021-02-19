using System.Windows.Controls;

namespace MailSender.Controls
{
    public partial class ItemsPanel : UserControl
    {
        public string Title { get => Header.Text; set => Header.Text = value; }

        public ItemsPanel() => InitializeComponent();
    }
}
