using System;
using System.Threading;

namespace LockKeyword
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MyClass instance = new();

            for (int i = 0; i < 3; i++)
            {
                new Thread(instance.Method).Start();
            }

            Thread.Sleep(1000);

            Console.ReadKey();
        }
    }
}
