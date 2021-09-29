using System;
using System.Threading;

namespace LockKeyword
{
    internal class MyClass
    {
        private readonly object _locker = new();

        public void Method()
        {
            int hash = Thread.CurrentThread.GetHashCode();

            lock (_locker)
            {
                for (int counter = 0; counter < 10; counter++)
                {
                    Console.WriteLine($"Thread # {hash}, iteration: {counter}");
                    Console.Beep();
                    Thread.Sleep(500);
                }

                Console.WriteLine(new string('-', 20));
            }
        }
    }
}