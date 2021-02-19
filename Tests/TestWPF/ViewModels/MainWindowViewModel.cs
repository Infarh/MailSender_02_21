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
                //if(_Title == value) return;
                if(Equals(_Title, value)) return;
                
                _Title = value;
                OnPropertyChanged();
            }
        }
    }
}
