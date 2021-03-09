using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Reports;

namespace TestConsole
{
    internal static class WordReport
    {
        public static void Create()
        {
            var report = new Report
            {
                StrValue1 = "Hello World!",
                StrValue2 = "Всем привет!!!!111",
                IntValue1 = 13,
                IntValue2 = 42
            };

            report.CreatePackage("report.docx");
        }
        
    }
}
