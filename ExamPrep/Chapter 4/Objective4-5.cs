using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter4
{
    public static class StackTesting
    {
        public static void StackTest(int[] numbers)
        {
            Stack<int> stack = new Stack<int>();

            foreach (int number in numbers)
            {
                stack.Push(number);
            }

            while (stack.Count > 0)
            {
                if (stack.Peek() % 2 == 0)
                {
                    Console.WriteLine("{0} is Even", stack.Pop());
                }
                else
                {
                    Console.WriteLine("{0} is Odd", stack.Pop());
                }
            }
        }
    }

    public static class QueueTesting
    {
        public static void QueueTest(int[] numbers)
        {
            Queue<int> queue = new Queue<int>();

            foreach (int num in numbers)
            {
                queue.Enqueue(num);
            }

            while (queue.Count > 0)
            {
                if (queue.Peek() % 2 == 0)
                {
                    Console.WriteLine("{0} is Even", queue.Dequeue());
                }
                else
                {
                    Console.WriteLine("{0} is Odd", queue.Dequeue());
                }
            }
        }
    }
}
