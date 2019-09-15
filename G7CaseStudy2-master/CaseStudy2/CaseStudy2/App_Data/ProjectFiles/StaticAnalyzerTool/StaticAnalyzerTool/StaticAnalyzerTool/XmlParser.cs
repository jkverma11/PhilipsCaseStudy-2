using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StaticAnalyzerTool
{
    static class XmlParser
    {
        public static string XmlParse(string tag, string value)
        {
            string returnString = String.Empty;
            try
            {
                var xmlDoc = XElement.Load(@"..\..\..\..\InputExePaths.xml");
                var paths = from element in xmlDoc.Descendants()
                            where element.Name == tag
                            select element;
                foreach (var msg in paths)
                {
                    if (msg.Attribute("Name").Value == value)
                        returnString = msg.Attribute("Path").Value;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                System.Environment.Exit(1);
            }
            return returnString;
        }
    }
}
