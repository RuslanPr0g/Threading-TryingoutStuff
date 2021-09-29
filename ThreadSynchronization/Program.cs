using System;
using System.Threading;

namespace ThreadSynchronization
{
    class Program
    {
        private static object _locker = new();

        static void Main(string[] args)
        {
            // Lock covered in another project

            MonitorWork();

            ManualResetEvent();

            AutoResetEvent();

            MutexWork();

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
                finally // it's the benefit over lock
                {
                    Monitor.Exit(_locker);
                }
            }
        }

        static void ManualResetEvent()
        {


            static void DoWork()
            {

            }
        }

        static void AutoResetEvent()
        {
            static void DoWork()
            {

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
