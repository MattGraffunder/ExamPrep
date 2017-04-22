using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPrep.Attributes;

namespace ExamPrep.Chapter_2
{
    [ChapterTestClass(2)]
    public class Chapter2Tests
    {
        [ChapterTest(1, Description = "Named and Optional Paramters, Indexers, Generics, and Extension Methods")]
        public static void Chapter_2_1()
        {
            OptionalArgumentsTesting optional = new OptionalArgumentsTesting();

            Console.WriteLine(optional.optionalArgs(1, "Ham", true));

            Console.WriteLine(optional.optionalArgs(2));

            Console.WriteLine(optional.optionalArgs(3, notNeeded: false));

            List<Something> things = new List<Something>() { new Something(), new Something(), new Something() };

            IndexerCollection<Something> somethingCollection = new IndexerCollection<Something>(things);
            IndexerCollection<Somethinger> somethingerCollection = new IndexerCollection<Somethinger>();
            somethingerCollection.Somethings.Add(new Somethinger());
            somethingerCollection.Somethings.Add(new Somethinger());

            somethingCollection.MakeUniform("Something Else");

            somethingerCollection.MakeUniform("Something Else");

            Console.WriteLine(string.Format("It is {0}", somethingCollection[1]));
            Console.WriteLine(string.Format("It is {0}", somethingerCollection[0]));
            Console.WriteLine(string.Format("It is {0}", somethingerCollection[1].SomethingMore()));
        }

        [ChapterTest(2, Description = "Implicit and Explicit Conversions, User Defined Conversions, \"is\" keyword, dynamic types, and Dynamic Object Class")]
        public static void Chapter_2_2()
        {
            SomethingNumbery numbery = new SomethingNumbery((decimal)1.23);

            decimal numberyDecimal = numbery;

            int numberyInt = (int)numbery;

            Console.WriteLine("String: {0}", numbery);
            Console.WriteLine("Decimal: {0}", numberyDecimal);
            Console.WriteLine("Int: {0}", numberyInt);

            Console.WriteLine();

            SomethingDynamic d = new SomethingDynamic();
            d.DoSomethingDynamic();
            d.DoSomethingExpandy();
        }

        [ChapterTest(4, Description = "IEnumerable Implementation")]
        public static void Chapter_2_4()
        {
            EnumerableTesting enumerable = new EnumerableTesting();

            enumerable.EnumerateHams();
        }

        [ChapterTest(5, Description = "Reflections Things")]
        public static void Chapter_2_5()
        {
            CodeDomGenerator.GenerateAndRunExe();
            Console.WriteLine("Launched Ham Time");
        }

        [ChapterTest(5, Description = "Other Things")]
        public static void Chapter_2_5_Other()
        {
            Func<int, int> doubleNum = ExpressionTester.AddThreeOrSquare(false);
            Func<int, int> squareNum = ExpressionTester.AddThreeOrSquare(true);

            int numberToTest = 4;

            Console.WriteLine("Should Add Three to {0}: {1}", numberToTest, doubleNum(numberToTest));
            Console.WriteLine("Should Square {0}: {1}", numberToTest, squareNum(numberToTest));
        }

        [ChapterTest(6, Description = "Weak References, and Garbage Collection")]
        public static void Chapter_2_6()
        {
            WeakTesting weak = new WeakTesting();

            List<int> weakList = weak.GetList();

            Console.WriteLine("Got List First Time, Id: {0}", weakList[0]);

            weakList = null;

            weakList = weak.GetList();

            Console.WriteLine("Got List Second Time, Id: {0}", weakList[0]);

            weakList = null;

            GC.Collect(0);

            Console.WriteLine("Ran Garbage Collector");

            weakList = weak.GetList();

            Console.WriteLine("Got List Third Time, Id: {0}", weakList[0]);
        }

        [ChapterTest(7, Description = "String Interning")]
        public static void Chapter_2_7_Interning()
        {
            StringInterning.InternTesting();
        }

        [ChapterTest(7, Description = "String Writer and String Reader")]
        public static void Chapter_2_7_StringWriterAndReader()
        {
            Console.WriteLine("XML:");

            string xml = StringReaderAndWriterTesting.StringWriterTesting();

            Console.WriteLine(xml);

            string nodeToFind="Name";
            string invalidNode = "Hams";

            Console.WriteLine();
            Console.WriteLine("Searching XML for \"{0}\"", nodeToFind);
            Console.WriteLine(StringReaderAndWriterTesting.StringReaderTesting(xml, nodeToFind));

            Console.WriteLine();
            Console.WriteLine("Searching XML for \"{0}\"", invalidNode);
            Console.WriteLine(StringReaderAndWriterTesting.StringReaderTesting(xml, invalidNode));
        }

        [ChapterTest(7, Description = "String Comparison Object")]
        public static void Chapter_2_7_StringComparison()
        {
            StringCompareFun.CompareFun();
        }

        [ChapterTest(7, Description = "Regular Expressions")]
        public static void Chapter_2_7_RegEx()
        {
            RegexMachine.RegexTesting();
        }

        [ChapterTest(7, Description = "Culture Info")]
        public static void Chapter_2_7_Culture()
        {
            decimal d = 12411.25m;

            Console.WriteLine("Formating a cost decimal based on culture.");
            Console.WriteLine("US: {0}", d.ToString("C", new System.Globalization.CultureInfo("en-US")));
            Console.WriteLine("Sweden: {0}", d.ToString("C", new System.Globalization.CultureInfo("se-SE")));
        }

        [ChapterTest(7, Description = "IFormattable and IFormatProvider")]
        public static void Chapter_2_7_Formating()
        {
            Address a = new Address();
            a.StreetNumber = 1119;
            a.StreetName = "Main";
            a.StreetType = "St.";
            a.City = "Hopkins";
            a.State = "MN";
            a.ZipCode = 55343;

            Address a2 = new Address();
            a2.StreetNumber = 1234;
            a2.StreetName = "Fake";
            a2.StreetType = "St.";
            a.Apt = "111";
            a2.City = "Somewhere";
            a2.State = "MN";
            a2.ZipCode = 55343;

            Console.WriteLine("Address: {0}", a.ToString());
            Console.WriteLine();
            Console.WriteLine("Address Formating ({0}) : {1}", "G", a2);
            Console.WriteLine("Address Formating ({0}) : {1:L}", "L", a2);
            Console.WriteLine("Address Formating ({0}) : {1:s}", "s", a2);
        }
    }
}
