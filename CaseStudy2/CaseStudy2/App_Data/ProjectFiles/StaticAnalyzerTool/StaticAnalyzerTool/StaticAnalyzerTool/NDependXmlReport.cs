using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace StaticAnalyzerTool
{
    public static class NDependXmlReport
    {
        public static void GenerateNDependReport(string nDependXmlOutputPath)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(nDependXmlOutputPath);
                List<string> attributeName = new List<string>();
                string[] attributeValue = null;

                int flag = 0;

                while (reader.Read() && flag == 0)
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "Metric")
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "Name")
                                    {
                                        attributeName.Add(reader.Value);
                                    }
                                }

                            }

                            if (reader.Name == "R" && flag == 0)
                            {
                                int i = 0;
                                while (reader.MoveToNextAttribute()) // Read the attributes.
                                {
                                    if (i == 2 && reader.Name == "V")
                                    {

                                        string answere = reader.Value;
                                        attributeValue = answere.Split('|');
                                    }

                                    i++;
                                }

                                flag = 1;
                            }

                            break;

                        case XmlNodeType.Text:
                            break;
                        case XmlNodeType.EndElement:
                            break;
                    }
                }

                StringBuilder strbuild = new StringBuilder();
                string text = "NDepend_Report";
                strbuild.AppendLine(text);
                if (attributeValue!= null)
                {
                    for (int k = 0; k < attributeValue.Length; k++)
                    {
                        string val = attributeName[k].Trim(new Char[] { '#' });
                        text = String.Format("ErrorMessage_Parameter = {0} Error_Count = {1}", val,
                            attributeValue[k].ToString());
                        strbuild.AppendLine(text);
                    }
                }

                try
                {
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter(@"..\..\..\..\FinalOutput.txt", true))
                    {
                        file.Write(strbuild.ToString());
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                    System.Environment.Exit(1);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                System.Environment.Exit(1);
            }
        }
    }
}
