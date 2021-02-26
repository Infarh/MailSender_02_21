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
            var ui_thread_id = Thread.CurrentThread.ManagedThreadId;

            var open_dialog = new OpenFileDialog
            {
                Filter = "Excel (*.xlsx)|*.xlsx|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                Title = "Выбор файла для чтения"
            };

            if(open_dialog.ShowDialog() != true) return;

            var file_name = open_dialog.FileName;

            if (!File.Exists(file_name)) return;

            var load_data_thread = new Thread(() => LoadData(file_name));
            load_data_thread.Start();
        }

        private void LoadData(string FileName)
        {
            var persons = GetPersons(FileName);
            var work_thread_id = Thread.CurrentThread.ManagedThreadId;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var ui_thread_id = Thread.CurrentThread.ManagedThreadId;
                return Data.ItemsSource = persons;
            });
            //Data.Dispatcher.Invoke(() => Data.ItemsSource = persons);
        }

        private static IEnumerable<Person> GetPersons(string FileName)
        {
            var persons = new List<Person>();
            var thread_id = Thread.CurrentThread.ManagedThreadId;

            foreach (var row in Excel.File(FileName)["senders"].Skip(1))
            {
                var values = row.Values.ToArray();
                persons.Add(new Person(
                    int.Parse(values[0]),
                    values[1],
                    values[2],
                    values[3],
                    values[4]
                ));
            }
            return persons;
        }
    }
}
