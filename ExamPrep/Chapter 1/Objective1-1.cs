using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_1
{
    public class ThreadTesting
    {
        [ThreadStatic]
        private static int staticField = 0;
        private static int nonStaticField = 0;

        public static ThreadLocal<int> _field =
            new ThreadLocal<int>(() =>
            {
                return Thread.CurrentThread.ManagedThreadId;
            });

        private static void ThreadMethod(object o)
        {
            int start = (int)o;
            int max = start + 1;

            for (int i = 0; i < max; i++)
            {
                Console.WriteLine("Thread started at {0}, currently at {1}", start, i);
                Thread.Sleep(10);
            }
        }

        public static void TestThreading()
        {
            Thread t1 = new Thread(new ParameterizedThreadStart(ThreadMethod));
            Thread t2 = new Thread(new ParameterizedThreadStart(ThreadMethod));

            t1.IsBackground = true;

            t1.Start(5);
            t2.Start(8);

            t2.Join();
        }

        public static void CancelThread()
        {
            bool cancel = false;

            Thread t1 = new Thread(new ThreadStart(() =>
            {
                while (!cancel)
                {
                    Console.WriteLine("Still Running 1");
                    Thread.Sleep(10);
                }

                Console.WriteLine("Canceled");
            }));

            t1.Start();

            Thread.Sleep(40);

            cancel = true;

            t1.Join();
        }

        public static void ThreadStaticTest()
        {
            int staticFieldt1 = 0;
            int staticFieldt2 = 0;

            Thread t1 = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    staticField++;
                    nonStaticField++;
                }

                staticFieldt1 = staticField;
            }));

            Thread t2 = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    staticField++;
                    nonStaticField++;
                }

                staticFieldt2 = staticField;
            }));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine("Static Field t1: {0}", staticFieldt1);
            Console.WriteLine("Static Field t2: {0}", staticFieldt2);
            Console.WriteLine("Non Static Field {0}", nonStaticField);            
        }

        public static void ThreadLocalTest()
        {
            Thread t1 = new Thread(new ThreadStart(() =>
            {
                Console.WriteLine("Thread Id: {0}", _field.Value);
            }));

            Console.WriteLine("Main Thread Id: {0}", Thread.CurrentThread.ManagedThreadId);

            t1.Start();
            t1.Join();
        }

        public static void ThreadPoolTest()
        {
            int minWorker;
            int minPort;

            int maxWorker;
            int maxPort;

            int availableWorker;
            int availablePort;

            ThreadPool.GetMinThreads(out minWorker, out minPort);
            ThreadPool.GetMaxThreads(out maxWorker, out maxPort);
            ThreadPool.GetMaxThreads(out availableWorker, out availablePort);

            Console.WriteLine("Thread Pool: ");
            Console.WriteLine("Thread Pool Threads, Min: {0}, Max: {1}, Available: {2}", minWorker, maxWorker, availableWorker);
            ThreadPool.QueueUserWorkItem((s) =>
                {
                    Console.WriteLine("Thread Pool Thread - May Show up Randomly");
                });
        }
    }

    public class TaskTesting
    {
        public static void TaskTest()
        {
            Task<int> something = new Task<int>(TaskMethod);

            Console.WriteLine("Completed: {0}", something.IsCompleted);

            something.Start();

            something.Wait();

            Console.WriteLine("Completed: {0}", something.IsCompleted);
            Console.WriteLine("Value: {0}", something.Result);
        }

        public static void ContinuationOnCompletionTasks()
        {
            Task<int> something = new Task<int>(TaskMethod);

            Task<int> somethingElse = something.ContinueWith((i) =>
                {
                    return i.Result / 2;
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            something.Start();

            somethingElse.Wait();

            Console.WriteLine("Value: {0}", somethingElse.Result);
        }

        public static void ContinuationOnFaultedTask()
        {
            try
            {
                Task<int> somethingBad = new Task<int>(BadTaskMethod);

                Task somethingBadHandler = somethingBad.ContinueWith((i) =>
                {
                    Console.WriteLine("Error: {0}", i.Exception.Message);
                }, TaskContinuationOptions.OnlyOnFaulted);

                Task shouldNotRun = somethingBad.ContinueWith((i) =>
                {
                    Console.WriteLine("This Should not have Run.");
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                somethingBad.Start();

                somethingBadHandler.Wait();
                shouldNotRun.Wait();
            }
            catch
            {
            }
        }

        public static void CancelationTesting()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            // Using an anonymous method to create a closure around the CancelableMethod
            // This is to easily pass in the token parameter, however it could be used
            // to pass in any parameter instead of a cancelation token.
            Task<int> canceledTask = new Task<int>(() => { return CancelableMethod(token); }, token);

            Task afterCanceledTask = canceledTask.ContinueWith(i =>
            {
                //i.Exception.Handle((e) => true);
                Console.WriteLine("Canceled the Task Correctly - May Show up Randomly");
            }, TaskContinuationOptions.OnlyOnCanceled);

            Task shouldNotRun = canceledTask.ContinueWith((i) =>
            {
                Console.WriteLine("This Should not have Run.");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            canceledTask.Start();

            tokenSource.Cancel();

            // Waiting on a Task that gets canceled will generate an Aggregate Exception
            // This is to break the Wait method and unblock the thread, since a canceled 
            // state is not expected.  The aggregate exception will not be thrown if there 
            // is no wait.

            //afterCanceledTask.Wait(token);
            //shouldNotRun.Wait(token);
        }

        public static void ParentAndChildTasks()
        {
            Task<int> parent = new Task<int>(() =>
                {
                    int i = 0;

                    Task<int> t = new Task<int>(() => { Thread.Sleep(100); return 55; }, TaskCreationOptions.AttachedToParent);
                    t.Start();

                    return t.Result;
                }, TaskCreationOptions.None);

            Task<int> afterParent = parent.ContinueWith<int>(parentTask => { return parentTask.Result; }, TaskContinuationOptions.OnlyOnRanToCompletion);

            parent.Start();

            afterParent.Wait();

            Console.WriteLine("Expecting {0} to be {1}", afterParent.Result, 55);
        }

        public static void MultipleTasks()
        {
            TaskFactory<int> taskFactory = new TaskFactory<int>(TaskCreationOptions.DenyChildAttach, TaskContinuationOptions.PreferFairness);

            Task<int>[] tasks = new Task<int>[3];

            tasks[0] = taskFactory.StartNew(() => { Thread.Sleep(200); return 1; });
            tasks[1] = taskFactory.StartNew(() => { Thread.Sleep(100); return 2; });
            tasks[2] = taskFactory.StartNew(() => { Thread.Sleep(300); return 3; });

            int firstIndex = Task.WaitAny(tasks);

            Console.WriteLine("First Task Done: {0}", tasks[firstIndex].Result);

            Task.WaitAll(tasks);

            Console.WriteLine("Total: {0}", tasks[0].Result + tasks[1].Result + tasks[2].Result);
        }

        private static int TaskMethod()
        {
            int num = 1;
            for (int i = 1; i < 100; i++)
            {
                num += i;
            };

            return num;
        }

        private static int BadTaskMethod()
        {
            throw new Exception("BOOM!");
        }

        private static int CancelableMethod(CancellationToken cancel)
        {
            //Added Token as parameter to show that Dev must explictly add it to a method signature
            //or pass it in in the scope of its caller if the method is anonymous.

            int i = 0;

            while (!cancel.IsCancellationRequested)
            {
                i++; 
            }

            if (cancel.IsCancellationRequested)
            {
                cancel.ThrowIfCancellationRequested();
            }

            return i;
        }
    }

    public static class ParallelTesting
    {
        public static void TestParallelForEach()
        {
            int[] oldNumbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Parallel.For(0, numbers.Length, i => numbers[i] += 5);

            Console.Write("Old Sequence: {0}", oldNumbers[0]);

            for (int i = 1; i < oldNumbers.Length; i++)
            {
                Console.Write(", {0}",oldNumbers[i]);
            }

            Console.WriteLine();

            Console.Write("New Sequence: {0}", numbers[0]);

            for (int i = 1; i < numbers.Length; i++)
            {
                Console.Write(", {0}", numbers[i]);
            }

            Console.WriteLine();
        }
    }

    public static class ParallelCollectionTesting
    {
        readonly static int[] numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public static void BlockingCollectionTesting()
        {
            BlockingCollection<int> bc = new BlockingCollection<int>();
            Task slowWrite = Task.Run(() =>
                {
                    foreach (int number in numbers)
                    {
                        bc.Add(number);
                        Thread.Sleep(100);
                    }
                });

            Task read = Task.Run(() =>
            {
                Console.Write("Blocking Collection Numbers:");
                foreach (int number in bc.GetConsumingEnumerable())
                {
                    if (number != 1)
                    {
                        Console.Write(",");
                    }

                    Console.Write(" {0}", number);
                }

                Console.WriteLine();
            });
        }

        public static void ConcurrentBagTesting()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
        }
    }
}