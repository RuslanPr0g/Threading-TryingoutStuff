using System;
using System.Threading;

namespace ThreadSynchronization
{
    class Program
    {
        private static readonly object _locker = new();
        private static readonly ManualResetEvent _mre = new(false);
        private static readonly AutoResetEvent _are = new(true);
        private static readonly Mutex _m = new();
        private static readonly Semaphore _s = new(1, 1);

        static void Main(string[] args)
        {
            // Lock covered in another project

            Console.WriteLine("Monitor:");
            MonitorWork();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("Manual:");
            ManualResetEventWork();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("Auto:");
            AutoResetEventWork();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("Mutex:");
            MutexWork();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("Semaphore:");
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
            // In AutoResetEventWork it's like WaitOne() and Reset() (in ManualResetEventWork) executated as a one atomic operation

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
            // mutex is around an OS while lock (Monitor) are about AppDoamin

            for (int i = 0; i < 5; i++)
            {
                new Thread(Write).Start();
            }

            static void Write()
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting to write...");
                _m.WaitOne();
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
                Thread.Sleep(5000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing completed...");
                _m.ReleaseMutex();
            }
        }

        static void SemaphoreWork()
        {
            for (int i = 0; i < 5; i++)
            {
                new Thread(Write).Start();
            }

            static void Write()
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} waiting to write...");
                _s.WaitOne();
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing...");
                Thread.Sleep(5000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} writing completed...");
                _s.Release();
            }
        }
    }
}
