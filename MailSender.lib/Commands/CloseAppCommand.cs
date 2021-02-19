using System.Windows;
using MailSender.lib.Commands.Base;

namespace MailSender.lib.Commands
{
    public class CloseAppCommand : Command
    {
        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
