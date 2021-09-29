using System;
using System.Threading;

namespace ThreadSynchronization
{
    class Program
    {
        private static readonly object _locker = new();
        private static readonly ManualResetEvent _mre = new(false);
        private static readonly AutoResetEvent _are = new(true);

        static void Main(string[] args)
        {
            // Lock covered in another project

            MonitorWork();
            Console.WriteLine(new string('-', 20));
            ManualResetEventWork();
            Console.WriteLine(new string('-', 20));
            AutoResetEventWork();
            Console.WriteLine(new string('-', 20));
            MutexWork();
            Console.WriteLine(new string('-', 20));
            SemaphoreWork();

            Console.ReadKey();
        }

        static void MonitorWork()
        {
            for (int i = 0; i < 5; i++)
            {
                new Thread(DoWork).Start();
            }

            static void DoWork()
            {
                try
                {
                    Monitor.Enter(_locker);
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starting...");
                    Thread.Sleep(1500);
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} completed...");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally // it's a benefit over the lock
                {
                    Monitor.Exit(_locker);
                }
            }
        }

        static void ManualResetEventWork()
        {
            new Thread(Write).Start();

            for (int i = 0; i < 5; i++)
            {
                new Thread(Read).Start();
            }

            static void Write()
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
                _mre.Reset();
                Thread.Sleep(5000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing completed...");
                _mre.Set();
            }

            static void Read()
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} reading...");
                _mre.WaitOne();
                Thread.Sleep(2000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} reading completed...");
            }
        }

        static void AutoResetEventWork()
        {
            for (int i = 0; i < 5; i++)
            {
                new Thread(Write).Start();
            }

            static void Write()
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting to write...");
                _are.WaitOne();
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
                Thread.Sleep(5000);
                _are.Set();
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing completed...");
            }
        }

        static void MutexWork()
        {
            static void DoWork()
            {

            }
        }

        static void SemaphoreWork()
        {
            static void DoWork()
            {

            }
        }
    }
}
