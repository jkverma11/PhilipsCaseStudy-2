using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace StaticAnalyzerTool
{
    
    class Program
    {
        static void Main(string[] args)
        {
            //Paths path = new Paths();
            //path.processPath = XmlParser.XmlParse("Process", "ExePath");
            //path.solutionPath = XmlParser.XmlParse("Process", "SolutionPath");

            IStaticAnalyzer fxcop = new FxCopAnalyzer();
            //fxcop.ProcessInput(path);
            //fxcop.ProcessOutput();

            //IStaticAnalyzer nDepend = new NDependAnalyzer();
            //nDepend.ProcessInput(path);
            //nDepend.ProcessOutput();

            foreach (var prop in fxcop.GetType().GetProperties())
            {
                Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(fxcop, null));
            }
        }
    }
}
