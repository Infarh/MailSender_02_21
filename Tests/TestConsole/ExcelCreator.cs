using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestConsole
{
    internal static class ExcelCreator
    {
        private record Person(string LastName, string Name, string Patronymic);

        private static IEnumerable<Person> GetPersons(string FileName)
        {
            using var reader = File.OpenText(FileName);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var items = line.Split(' ');
                if (items.Length < 3) continue;
                yield return new Person(items[0], items[1], items[2]);
            }
        }

        private static T Next<T>(this Random rnd, IList<T> items) => items[rnd.Next(items.Count)];

        public static void Create()
        {
            var last_names = new List<string>(100);
            var first_names = new List<string>(100);
            var patronymics = new List<string>(100);

            string[] servers =
            {
                "yandex.ru",
                "mail.ru",
                "rambler.ru",
                "gmail.com",
                "live.com",
                "geekbrains.ru",
            };

            const string names_file = "Names.txt";
            foreach (var (last, first, patronymic) in GetPersons(names_file))
            {
                last_names.Add(last);
                first_names.Add(first);
                patronymics.Add(patronymic);
            }

            const int count = 100_000;

            var rnd = new Random();
            const string data_file = "senders.csv";
            using var writer = new StreamWriter(data_file, false, Encoding.UTF8);
            writer.WriteLine("id;Фамилия;Имя;Отчество;Адрес");
            for (var i = 1; i < count; i++)
            {
                var last_name = rnd.Next(last_names);
                var first_name = rnd.Next(first_names);
                var patronymic = rnd.Next(patronymics);

                var str = $"{i};{last_name};{first_name};{patronymic};{last_name.Transliterate()}@{rnd.Next(servers)}";
                Console.WriteLine(str);
                writer.WriteLine(str);
            }
        }

        private static string Transliterate(this string str) => str
           .Aggregate(
                new StringBuilder(),
                (s, c) => s.Append(c.Transliterate()),
                s => s.ToString());

        private static string Transliterate(this char c) => c switch
        {
            'а' => "a",
            'б' => "b",
            'в' => "v",
            'г' => "g",
            'д' => "d",
            'е' => "e",
            'ё' => "y",
            'ж' => "j",
            'з' => "z",
            'и' => "i",
            'к' => "k",
            'л' => "l",
            'м' => "m",
            'н' => "n",
            'о' => "o",
            'п' => "p",
            'р' => "r",
            'с' => "s",
            'т' => "t",
            'у' => "u",
            'ф' => "f",
            'х' => "h",
            'ц' => "c",
            'ч' => "ch",
            'ш' => "sh",
            'щ' => "sch",
            'ъ' => "'",
            'ы' => "i",
            'ь' => "'",
            'э' => "a",
            'ю' => "yu",
            'я' => "ya",

            'А' => "A",
            'Б' => "B",
            'В' => "V",
            'Г' => "G",
            'Д' => "D",
            'Е' => "E",
            'Ё' => "Yo",
            'Ж' => "J",
            'З' => "Z",
            'И' => "I",
            'К' => "K",
            'Л' => "L",
            'М' => "M",
            'Н' => "N",
            'О' => "O",
            'П' => "P",
            'Р' => "R",
            'С' => "S",
            'Т' => "T",
            'У' => "U",
            'Ф' => "F",
            'Х' => "H",
            'Ц' => "C",
            'Ч' => "Ch",
            'Ш' => "Sh",
            'Щ' => "Sch",
            'Ъ' => "'",
            'Ы' => "I",
            'Ь' => "'",
            'Э' => "A",
            'Ю' => "Yu",
            'Я' => "Ya",
            _ => ""
        };
    }
}
