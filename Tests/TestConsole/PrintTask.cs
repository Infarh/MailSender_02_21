using System;
using System.Threading;

namespace TestConsole
{
    class PrintTask
    {
        private readonly int _MessageId;
        private readonly string _Message;
        private readonly Thread _Thread;

        public PrintTask(int MessageId, string Message)
        {
            _MessageId = MessageId;
            _Message = Message;
            _Thread = new Thread(ThreadMethod);
        }

        private void ThreadMethod()
        {
            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine("MId:{0}  --- {1}", _MessageId, _Message);
                Thread.Sleep(10);
            }
        }

        public void Start()
        {
            _Thread.Start();
        }
    }
}
