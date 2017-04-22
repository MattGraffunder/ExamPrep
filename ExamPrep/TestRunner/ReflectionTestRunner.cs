using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ExamPrep.Attributes;

namespace ExamPrep.TestRunner
{
    class ReflectionTestRunner : ITestRunner
    {
        public void RunTest(string testName)
        {
            int chapter, objective = 0;

            if (testName == null)
            {
                throw new ArgumentNullException();
            }

            if (testName.Length != 3 ||
                !int.TryParse(testName[0].ToString(), out chapter)
                || !int.TryParse(testName[2].ToString(), out objective)
                )
            {
                throw new ArgumentException();
            }

            //Get All the Classes from the Current Assembly
            IEnumerable<Type> types = typeof(ReflectionTestRunner).Assembly.GetTypes();

            //Get the Classes that contain tests for this chapter
            var testClasses = types.Where(t => t.GetCustomAttributes().Any(
                att => att is ChapterTestClassAttribute && ((ChapterTestClassAttribute)att).Chapter == chapter));

            //Get the Methods that are tests for this objective
            var testMethods = testClasses.SelectMany(c => c.GetMethods()).Where(m=>m.GetCustomAttributes().Any(att=>att is ChapterTestAttribute && ((ChapterTestAttribute)att).Objective == objective));

            if (testMethods.Count() > 0)
            {
                //Run all Tests
                RunTests(testMethods, chapter, objective);
            }
            else
            {
                TestRunError(chapter, objective);
            }
        }

        private void TestRunError(int chapter, int objective)
        {
            Console.WriteLine("Could not find any tests for Chapter {0}, Objective {1}", chapter, objective);

            Console.ReadKey();
        }

        private void RunTests(IEnumerable<MethodInfo> methods, int chapter, int objective)
        {
            Console.WriteLine("Running Tests for Chapter: {0}, Objective {1}", chapter, objective);
            Console.WriteLine("----------------------------------------");

            foreach (var method in methods)
            {
                RunTest(method);
            }

            Console.ReadKey();
        }

        private void RunTest(MethodInfo method)
        {
            ChapterTestAttribute attribute = method.GetCustomAttribute(typeof(ChapterTestAttribute)) as ChapterTestAttribute;

            if (attribute == null)
            {
                throw new ArgumentException(string.Format("{0} is not a test.", method.Name));
            }
            
            Console.WriteLine("");
            Console.WriteLine("Testing {0}", attribute.Description);            
            Console.WriteLine("");

            method.Invoke(null, null);

            Console.WriteLine("----------------------------------------");
        }
    }
}