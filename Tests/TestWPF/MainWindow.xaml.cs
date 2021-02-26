using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Win32;
using TestWPF.Servcie;

namespace TestWPF
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        public record Person(int Id, string LastName, string Name, string Patronymic, string Address);

        private void OpenFileMenu_Click(object Sender, RoutedEventArgs E)
        {
            var open_dialog = new OpenFileDialog
            {
                Filter = "Excel (*.xlsx)|*.xlsx|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Выбор файла для чтения"
            };

            if(open_dialog.ShowDialog() != true) return;

            var file_name = open_dialog.FileName;

            if (!File.Exists(file_name)) return;

            var persons = new List<Person>();
            foreach (var row in Excel.File(file_name)["senders"].Skip(1))
            {
                var values = row.Values.ToArray();
                persons.Add(new Person(
                    int.Parse(values[0]),
                    values[1],
                    values[2],
                    values[3],
                    values[4]
                    ));
                Thread.Sleep(1);
            }

            Data.ItemsSource = persons;
        }
    }
}
