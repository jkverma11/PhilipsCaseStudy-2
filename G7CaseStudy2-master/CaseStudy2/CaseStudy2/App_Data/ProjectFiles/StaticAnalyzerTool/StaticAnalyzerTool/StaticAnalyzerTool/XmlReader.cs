using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;

namespace StaticAnalyzerTool
{
    class XmlReader
    {
        static void Main(string[] args)
        {
            List<string> Path = new List<string>();
            List<string> CommandLineArguments =new List<string>();
            string xmlpath = @"C:\Users\320050491\StaticAnalysisTool\InputTools.xml";
            var xmlDoc = XElement.Load(xmlpath);

            var staticTools = from element in xmlDoc.Elements()
                              where element.Name == "Tools"
                              select element;
            foreach(var staticTool in staticTools)
            {
                string toolName = string.Empty;
                foreach(var tool in staticTool.Elements())
                {
                    if (tool.Name == "Path")
                        Path.Add("@" + tool.Value);
                    if (tool.Name == "Arguments")
                        CommandLineArguments.Add("@" + tool.Value);
                }
            }
            for( int i = 0; i < Path.Count; i++)
            {
                try
                {
                    Process.Start(Path[i], CommandLineArguments[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }
    }
}
