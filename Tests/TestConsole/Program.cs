using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using System.Reflection.Emit;

namespace TestConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            const string lib_path = "TestLib.dll";
            //var lib_file_info = new FileInfo(lib_path);
            //Assembly lib = Assembly.LoadFile(lib_file_info.FullName);
            Assembly lib = Assembly.LoadFile(Path.GetFullPath(lib_path));

            //Assembly entry_point = Assembly.GetEntryAssembly();

            //var reports_asm = typeof(MailSender.Reports.Report).Assembly;

            //lib.Location

            Type program_type = typeof(Program);

            var list_of_strings = new List<string>();
            Type list_of_strings_type = list_of_strings.GetType();

            //list_of_strings_type.GetProperties();
            //list_of_strings_type.GetMethods();
            //list_of_strings_type.GetConstructors();
            //list_of_strings_type.GetFields();

            //ConstructorInfo
            //MethodInfo    *
            //ParameterInfo
            //PropertyInfo
            //EventInfo
            //FieldInfo     *

            var printer_type = lib.GetType("TestLib.Printer");

            var private_methods = printer_type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            //foreach (var method in printer_type.GetMethods())
            //{
            //    var return_type = method.ReturnType;
            //    var parameters = method.GetParameters();

            //    Console.WriteLine("{0} {1}({2})",
            //        return_type.Name,
            //        method.Name,
            //        string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}")));
            //}

            object printer1 = Activator.CreateInstance(printer_type, ">>>");

            var private_printer_type = lib.GetType("TestLib.InternalPrinter");
            object printer2 = Activator.CreateInstance(private_printer_type);

            var printer_ctor = printer_type.GetConstructor(new[] { typeof(string) });
            var printer3 = printer_ctor.Invoke(new object[] { "<<<" });

            var print_method_info = printer_type.GetMethod("Print"/*, BindingFlags.Instance | BindingFlags.NonPublic*/);

            print_method_info.Invoke(printer1, new object[] { "Hello World!!!" });

            var prefix_field_info = printer_type.GetField("_Prefix", BindingFlags.Instance | BindingFlags.NonPublic);

            var field_value = (string)prefix_field_info.GetValue(printer1);
            prefix_field_info.SetValue(printer1, "!!!---!!!");

            //var print_delegate = (Action<string>) print_method_info.CreateDelegate(typeof(Action<string>), printer1);
            var print_delegate = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), printer1, print_method_info);

            for (var i = 0; i < 10; i++)
                print_delegate($"Message {i}");

            dynamic printer_dynamic = printer1;
            printer_dynamic.Print("123123123");

            var objects = new object[]
            {
                "String value",
                123,
                3.14,
                true,
                'q',
                //new List<string>()
            };

            ProcessValues(objects);

            Console.WriteLine("Завершено");
            Console.ReadLine();
        }

        private static void ProcessValues(IEnumerable<object> values)
        {
            //foreach (var value in values)
            //    switch (value)
            //    {
            //        case string val: ProcessValue(val); break;
            //        case int val: ProcessValue(val); break;
            //        case double val: ProcessValue(val); break;
            //        case bool val: ProcessValue(val); break;
            //    }

            foreach (dynamic value in values)
                ProcessValue(value);
        }

        private static void ProcessValue(string value) => Console.WriteLine("string: {0}", value);
        private static void ProcessValue(int value) => Console.WriteLine("int: {0}", value);
        private static void ProcessValue(double value) => Console.WriteLine("double: {0}", value);
        private static void ProcessValue(bool value) => Console.WriteLine("bool: {0}", value);
    }
}
