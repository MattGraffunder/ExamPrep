using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExamPrep.TestRunner;

namespace ExamPrep.ConsoleView
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Select Test to Run: ");

            string input = Console.ReadLine();

            ITestRunner testRunner = new ReflectionTestRunner();

            testRunner.RunTest(input);
        }        
    }
}
