using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPrep.Attributes;

namespace ExamPrep.Chapter_1
{
    [ChapterTestClass(1)]
    public static class Chapter1Tests
    {
        [ChapterTest(1, Description = "Thread Testing")]
        public static void Objective_1_Threads()
        {
            ThreadTesting.TestThreading();
        }

        [ChapterTest(1, Description = "Cancel Thread")]
        public static void Objective_1_Cancel_Thread()
        {
            ThreadTesting.CancelThread();
        }

        [ChapterTest(1, Description = "Thread Static Test")]
        public static void Objective_1_Thread_Static()
        {
            ThreadTesting.ThreadStaticTest();
        }

        [ChapterTest(1, Description = "Thread Local Test")]
        public static void Objective_1_Thread_Local()
        {
            ThreadTesting.ThreadLocalTest();
        }

        [ChapterTest(1, Description = "Thread Pool Test")]
        public static void Objective_1_Thread_Pool()
        {
            ThreadTesting.ThreadPoolTest();
        }

        [ChapterTest(1, Description = "Tasks")]
        public static void Objective_1_Tasks()
        {
            TaskTesting.TaskTest();
        }

        [ChapterTest(1, Description = "Continue on Completion")]
        public static void Objective_1_Continue_on_Completion()
        {
            TaskTesting.ContinuationOnCompletionTasks();
            TaskTesting.ContinuationOnFaultedTask();
        }

        [ChapterTest(1, Description = "Cancelation, and Continue on Canceled")]
        public static void Objective_1_Canceled_and_Continue()
        {
            TaskTesting.CancelationTesting();
        }

        [ChapterTest(1, Description = "Parent and Child Tasks")]
        public static void Objective_1_ChildTasks()
        {
            TaskTesting.ParentAndChildTasks();
        }

        [ChapterTest(1, Description = "Multiple Tasks")]
        public static void Objective_1_MultipleTasks()
        {
            TaskTesting.MultipleTasks();
        }

        [ChapterTest(1, Description = "Parallel For")]
        public static void Objective_1_Parallel()
        {
            ParallelTesting.TestParallelForEach();
        }

        [ChapterTest(1, Description = "Blocking Collections")]
        public static void Objective_1_Blocking()
        {
            ParallelCollectionTesting.BlockingCollectionTesting();
        }

        [ChapterTest(2, Description = "Synchronization")]
        public static void Objective_2_Unsynchronized()
        {
            int unsynchronizedResult = SynchronizationTesting.Unsynchronized();
            int synchronizedResult = SynchronizationTesting.Synchronized();
            int interlockedResult = SynchronizationTesting.InterlockedTest();

            Console.WriteLine("Unsynchronized: {0}, Synchronized: {1}, Interlocked: {2}", unsynchronizedResult, synchronizedResult, interlockedResult);
        }

        [ChapterTest(3, Description = "Jump Testing")]
        public static void Objective_3_Jumping()
        {
            JumpTesting.JumpTest(3);
            JumpTesting.JumpTest(8);
            JumpTesting.JumpTest(5);
        }

        [ChapterTest(3, Description = "Switch Testing")]
        public static void Objective_3_Switching()
        {
            for (int i = 1; i < 6; i++)
            {
                SwitchTesting.SwitchTest(i);
            }
        }

        [ChapterTest(4, Description = "Custom Delegate")]
        public static void Objective_4_CustomDelegate()
        {
            DelegateTesting dt = new DelegateTesting();

            dt.CustomDelegate(5, 3);
        }

        [ChapterTest(4, Description = "Multicast Delegate")]
        public static void Objective_4_MultiCastDelegate()
        {
            DelegateTesting dt = new DelegateTesting();

            dt.MulitcastDelegate();
        }

        [ChapterTest(4, Description = "Lambda Delegate")]
        public static void Objective_4_LambdaDelegate()
        {
            DelegateTesting dt = new DelegateTesting();

            dt.LambdaDelegate(5, 3);
        }

        [ChapterTest(4, Description = "Generic Delegate")]
        public static void Objective_4_GenericDelegate()
        {
            DelegateTesting dt = new DelegateTesting();

            dt.GenericDelegate(5, 3);
        }

        [ChapterTest(4, Description = "Events")]
        public static void Objective_4_EventTesting()
        {
            EventController controller = new EventController();

            controller.TestFire();
        }

        [ChapterTest(4, Description = "Events with Exceptions")]
        public static void Objective_4_EventExceptionTesting()
        {
            EventController controller = new EventController();

            controller.CustomTestFire();
        }

        [ChapterTest(5, Description = "Fast Failure")]
        public static void Objective_5_FastFail()
        {
            ExceptionHandling.FailQuickly();
        }

        [ChapterTest(5, Description = "Exception Dispatch")]
        public static void Objective_5_Dispatching()
        {
            try
            {
                ExceptionHandling.ExeceptionDispatch();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Type: {0}", ex.GetType());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}