using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_1
{
    public class ExceptionHandling
    {
        //Fail Fast
        public static void FailQuickly()
        {
            try
            {
                // Throws System.ExecutionEngineException instead of an exception derived
                // from System.Exception

                //Environment.FailFast("Testing");
            }
            finally
            {
                Console.WriteLine("Fail Fast is Commented Out");
            }
        }

        //ExceptionDispatchInfo        
        public static void ExeceptionDispatch()
        {
            // Can be used to throw exceptions onto different threads.
            ExceptionDispatchInfo edi = null;

            Task t = Task.Run(() =>
            {
                try
                {
                    int i = int.Parse("Ain't gonna work");
                }
                catch (Exception ex)
                {
                    edi = ExceptionDispatchInfo.Capture(ex);
                }
            });

            t.Wait();

            Console.WriteLine("Task threw {0}, rethrowing", edi.SourceException.GetType());

            edi.Throw();
        }

        //Custom Exception Contructors
        [Serializable]
        public class TestException : Exception
        {
            public TestException() { }
            public TestException(string message) : base(message) { }
            public TestException(string message, Exception inner) : base(message, inner) { }
            protected TestException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        }
    }
}