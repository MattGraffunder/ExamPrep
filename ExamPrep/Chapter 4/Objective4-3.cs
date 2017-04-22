using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExamPrep.Chapter4
{
    public class LinqTester
    {
        public static string LinqXml()
        {
            XDocument doc = XDocument.Parse(XmlTesting.CreateXMLDocument());

            var hamTypes = doc.Element("Hams").Elements("Ham").Select(h => h.Element("Type").Value);
            
            StringBuilder builder = new StringBuilder();

            foreach (var type in hamTypes)
            {
                builder.Append(type);
                builder.Append(" ");
            }

            return builder.ToString();
        }
    }
}
