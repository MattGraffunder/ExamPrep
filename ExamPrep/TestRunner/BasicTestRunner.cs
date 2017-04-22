using ExamPrep.Chapter_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPrep.TestRunner;

namespace ExamPrep
{
    public class BasicTestRunner : ITestRunner
    {
        public void RunTest(string chapterAndObjective)
        {
            switch (chapterAndObjective)
            {
                case "2-1":
                    Chapter2Tests.Chapter_2_1();
                    break;
                case "2-2":
                    Chapter2Tests.Chapter_2_2();
                    break;
                case "2-4":
                    Chapter2Tests.Chapter_2_4();
                    break;
                case "2-6":
                    Chapter2Tests.Chapter_2_6();
                    break;
                default:
                    InvalidTestOption();
                    break;
            }
        }

        //private static void Chapter_2_1()
        //{
        //    OptionalArgumentsTesting optional = new OptionalArgumentsTesting();

        //    Console.WriteLine(optional.optionalArgs(1, "Ham", true));

        //    Console.WriteLine(optional.optionalArgs(2));

        //    Console.WriteLine(optional.optionalArgs(3, notNeeded: false));

        //    List<Something> things = new List<Something>() {new Something(), new Something(), new Something()};

        //    IndexerCollection<Something> somethingCollection = new IndexerCollection<Something>(things);
        //    IndexerCollection<Somethinger> somethingerCollection = new IndexerCollection<Somethinger>();
        //    somethingerCollection.Somethings.Add(new Somethinger());
        //    somethingerCollection.Somethings.Add(new Somethinger());

        //    somethingCollection.MakeUniform("Something Else");

        //    somethingerCollection.MakeUniform("Something Else");

        //    Console.WriteLine(string.Format("It is {0}", somethingCollection[1]));
        //    Console.WriteLine(string.Format("It is {0}", somethingerCollection[0]));
        //    Console.WriteLine(string.Format("It is {0}", somethingerCollection[1].SomethingMore()));
        //}

        //private static void Chapter_2_2()
        //{
        //    SomethingNumbery numbery = new SomethingNumbery((decimal)1.23);

        //    decimal numberyDecimal = numbery;

        //    int numberyInt = (int)numbery;
            
        //    Console.WriteLine("String: {0}", numbery);
        //    Console.WriteLine("Decimal: {0}", numberyDecimal);
        //    Console.WriteLine("Int: {0}", numberyInt);

        //    Console.WriteLine();

        //    SomethingDynamic d = new SomethingDynamic();
        //    d.DoSomethingDynamic();
        //    d.DoSomethingExpandy();
        //}

        //private static void Chapter_2_4()
        //{
        //    EnumerableTesting enumerable = new EnumerableTesting();

        //    enumerable.EnumerateHams();
        //}

        //private static void Chapter_2_6()
        //{
        //    WeakTesting weak = new WeakTesting();

        //    List<int> weakList = weak.GetList();

        //    Console.WriteLine("Got List First Time, Id: {0}", weakList[0]);

        //    weakList = null;

        //    weakList = weak.GetList();

        //    Console.WriteLine("Got List Second Time, Id: {0}", weakList[0]);

        //    weakList = null;

        //    GC.Collect(0);

        //    Console.WriteLine("Ran Garbage Collector");

        //    weakList = weak.GetList();

        //    Console.WriteLine("Got List Third Time, Id: {0}", weakList[0]);
        //}

        private static void InvalidTestOption()
        {
            Console.WriteLine("Invalid Option");
        }
    }
}
