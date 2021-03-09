using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TestLib
{
    [Description("Открытый принтер")]
    public class Printer
    {
        private readonly string _Prefix;

        public Printer([Required] string Prefix)
        {
            _Prefix = Prefix;
        }

        public virtual void Print(string Message)
        {
            Console.WriteLine("{0}{1}", _Prefix, Message);
        }
    }

    [Description("Скрытый принтер")]
    internal class InternalPrinter : Printer
    {
        public int Value { get; private set; } = 42;

        public InternalPrinter() : base("Internal:") { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void Print(string Message)
        {
            Console.WriteLine("Private print with {0}", Value++);
            base.Print(Message);
        }
    }
}
