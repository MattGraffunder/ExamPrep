using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExamPrep.Chapter4
{
    public class XmlTesting
    {
        public static string CreateXMLDocument()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode hamsNode = doc.CreateNode(XmlNodeType.Element, "Hams", "");

            XmlNode ham1 = doc.CreateNode(XmlNodeType.Element, "Ham", "");
            XmlNode ham1Type = doc.CreateNode(XmlNodeType.Element, "Type", "");
            ham1Type.InnerText = "Glazed";

            XmlAttribute ham1Size = doc.CreateAttribute("Weight");
            ham1Size.Value = "5lbs";

            //ham1.Attributes.Append(ham1Type);
            ham1.AppendChild(ham1Type);
            ham1.Attributes.Append(ham1Size);

            XmlNode ham2 = doc.CreateNode(XmlNodeType.Element, "Ham", "");
            XmlNode ham2Type = doc.CreateNode(XmlNodeType.Element, "Type", "");
            ham2Type.InnerText = "Smoked";

            XmlAttribute ham2Size = doc.CreateAttribute("Weight");
            ham2Size.Value = "3lbs";

            ham2.AppendChild(ham2Type);
            //ham2.Attributes.Append(ham2Type);
            ham2.Attributes.Append(ham2Size);

            hamsNode.AppendChild(ham1);
            hamsNode.AppendChild(ham2);

            doc.AppendChild(hamsNode);

            return doc.OuterXml;
        }
    }
}