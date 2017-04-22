using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_3
{
    static class Tracer
    {
        static TraceSource trace;

        static Tracer()
        {
            trace = new TraceSource("TestTracer", SourceLevels.All);
        }

        public static void DoStuff(int something, string somethingElse)
        {
            trace.TraceInformation("Starting to Do Stuff");
            trace.TraceData(TraceEventType.Verbose, 123, new object[] { something, somethingElse });

            if (something > 5)
            {
                trace.TraceEvent(TraceEventType.Critical, 000, "Can't Do Stuff Above 5");
            }
            else
            {
                trace.TraceEvent(TraceEventType.Information, 11, "Doing Stuff");
            }

            trace.TraceEvent(TraceEventType.Error, 2, string.Format("Didn't Expect {0}", somethingElse));
        }
    }

    class PerformanceCounters
    {
        private const string firstCounterName = "FirstCounter";
        private const string secondCounterName = "SecondCounter";
        private const string counterCategoryName = "ExamPerformance";

        public long ReadMemory()
        {
            using (PerformanceCounter pc = new PerformanceCounter("Memory", "Available Bytes"))
            {
                return pc.RawValue;
            }
        }

        public void CreatePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists(counterCategoryName))
            {
                CounterCreationDataCollection counters = new CounterCreationDataCollection()
                {
                    new CounterCreationData(firstCounterName, "Counts Stuff", PerformanceCounterType.NumberOfItems32),
                    new CounterCreationData(secondCounterName, "Counts Stuff per Second", PerformanceCounterType.RateOfCountsPerSecond32)
                };

                PerformanceCounterCategory.Create(counterCategoryName, "Exam Preparation Categories", PerformanceCounterCategoryType.MultiInstance, counters);
            }
        }

        public void IncrementPerformanceCounters()
        {
            var opsCounter = new PerformanceCounter(counterCategoryName, firstCounterName, false);
            var opsPerSecondCounter = new PerformanceCounter(counterCategoryName, secondCounterName, false);

            opsCounter.Increment();
            opsPerSecondCounter.Increment();
        }        
    }
}
