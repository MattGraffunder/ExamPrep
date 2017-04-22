using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;


namespace ExamPrep.Chapter_2
{
    class StringInterning
    {
        public static void InternTesting()
        {
            string s1 = "Something";
            string s2 = "Some" + "thing";
            string builder = new StringBuilder().Append("Some").Append("thing").ToString();
            string intern = String.Intern(builder);

            Console.WriteLine("s1 == '{0}'", s1);
            Console.WriteLine("s2 == '{0}'", s2);
            Console.WriteLine("builder == '{0}'", builder);
            Console.WriteLine("intern == '{0}'", intern);
            Console.WriteLine("Is s2 the same reference as s1?: {0}", (Object)s2 == (Object)s1);
            Console.WriteLine("Is \"builder\" the same reference as s1?: {0}", (Object)builder == (Object)s1);
            Console.WriteLine("Is \"Intern\" the same reference as s1?: {0}", (Object)intern == (Object)s1);
        }                
    }

    public class StringReaderAndWriterTesting
    {
        public static string StringWriterTesting()
        {
            StringWriter s = new StringWriter();

            using (XmlWriter x = XmlWriter.Create(s))
            {
                x.WriteStartElement("Somethings");
                x.WriteStartElement("Thing");
                x.WriteElementString("Name", "Thing1");
                x.WriteElementString("WhatItIs", "Awesome");
                x.WriteEndElement();
                x.WriteStartElement("Thing");
                x.WriteElementString("Name", "OtherThing");
                x.WriteElementString("WhatItIs", "Neat");
                x.WriteEndElement();
                x.WriteEndElement();

                x.Flush();
            }

            return s.ToString();
        }

        public static string StringReaderTesting(string xml, string nodeToReturn)
        {
            StringReader s = new StringReader(xml);
            using (XmlReader r = XmlReader.Create(s))
            {
                if (r.ReadToFollowing(nodeToReturn))
                {
                    return r.ReadInnerXml();
                }
                else
                {
                    return string.Format("Can't find \"{0}\"", nodeToReturn);
                }
            }
        }
    }

    public static class StringCompareFun
    {
        private const string s1 = "aeble";
        private const string s2 = "Æble";


        private const string s3 = "ångström";

        static string[] ses = new string[] { s1, s2, s3 };

        public static void CompareFun()
        {
            Console.WriteLine("Words: {0}, {1}, {2}", ses[0], ses[1], ses[2]);

            CompareAll();

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("se-SE");

            CompareAll();
        }

        private static void CompareAll()
        {
            var culture = Thread.CurrentThread.CurrentCulture;

            Console.WriteLine();
            Console.WriteLine("Current Culture: {0}", culture.Name);

            Console.WriteLine("Current Culture Ignore Case: {0} == {1} : {2}", s1, s2, String.Equals(s1,s2, StringComparison.CurrentCultureIgnoreCase));
            Console.WriteLine("Ordinal Ignore Case: {0} == {1} : {2}", s1, s2, String.Equals(s1, s2, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Number that Start with \"ae\": {0}", ses.Count(s => s.StartsWith("ae", StringComparison.CurrentCultureIgnoreCase)));
        }              
    }

    public static class RegexMachine
    {
        public static void RegexTesting()
        {
            string ham = "Ham is Good!";
            Regex r = new Regex("Ham");

            Console.WriteLine("\"{0}\" contains Ham? {1}", ham, r.IsMatch(ham));
        }
    }

    public class Address : IFormattable
    {
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string StreetType { get; set; }
        public string Apt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        public override string ToString()
        {
            //Return the Default Format
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {

            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            if (String.IsNullOrWhiteSpace(format))
            {
                format = "L";
            }

            format = format.Trim().ToUpperInvariant();

            if (format.Equals("G", StringComparison.Ordinal))
            {
                format = "L";
            }

            if (!format.Equals("L", StringComparison.Ordinal) && !format.Equals("S", StringComparison.Ordinal))
            {
                throw new FormatException(string.Format("The format {0} is invalid", format));
            }

            StringBuilder text = new StringBuilder();
            text.Append(StreetNumber);
            text.Append(" ");
            text.Append(StreetName);
            text.Append(" ");
            text.Append(StreetType);

            if (Apt != null && Apt.Length > 0)
            {
                text.Append(" ");
                text.Append(Apt);
            }

            if (format.Equals("L", StringComparison.Ordinal))
            {
                text.Append(" ");
                text.Append(City);
                text.Append(", ");
                text.Append(State);
                text.Append(" ");
                text.Append(ZipCode);
                
            }

            return text.ToString();
        }
    }
}