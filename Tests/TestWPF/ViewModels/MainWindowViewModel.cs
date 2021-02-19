using System.Printing;
using MailSender.lib.ViewModels.Base;

namespace TestWPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Test111";

        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }
}
