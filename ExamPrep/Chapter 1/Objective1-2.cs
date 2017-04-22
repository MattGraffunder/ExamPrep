using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_1
{
    public class SynchronizationTesting
    {
        private static volatile int _volitileInt;
        private static volatile int _otherInt;

        public static int Unsynchronized()
        {
            int n = 0;

            Task increment = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    n++;
                }
            });

            for (int i = 0; i < 1000000; i++)
            {
                n--;
            }

            increment.Wait();
            return n;
        }

        public static int Synchronized()
        {
            int n = 0;

            object _lock = new object();

            Task increment = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    lock (_lock)
                    {
                        n++; 
                    }
                }
            });

            for (int i = 0; i < 1000000; i++)
            {
                lock (_lock)
                {
                    n--; 
                }
            }

            increment.Wait();
            return n;
        }

        public static int InterlockedTest()
        {
            int n = 0;

            Task increment = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Interlocked.Increment(ref n);
                }
            });

            for (int i = 0; i < 1000000; i++)
            {
                Interlocked.Decrement(ref n);
            }

            increment.Wait();
            
            return n;
        }

        public static void VolitileTesting()
        {
            //The Complier won't rearrange the below statements since they are marked "volitle"
            //This comes a performace cost since the compiler can't optimize.

            _volitileInt = 1;
            _otherInt= 2;

            _volitileInt = 2;
        }
    }
}