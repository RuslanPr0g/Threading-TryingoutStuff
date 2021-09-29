using System;
using System.Threading;

namespace ThreadingTryingoutStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sync execution of functions:");
            Function1();
            Function2();

            Console.WriteLine("\nParallel execution of functions:");
            Thread obj1 = new(Function1);
            Thread obj2 = new(Function2);

            obj1.Start();
            obj2.Start();
        }

        static void Function1()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Function 1: " + i);
                Thread.Sleep(1000);
            }
        }

        static void Function2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Function 2: " + i);
                Thread.Sleep(1000);
            }
        }
    }
}
