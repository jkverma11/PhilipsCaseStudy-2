using System;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;


namespace StaticAnalyzerTool
{
    class FxCopAnalyzer : IStaticAnalyzer
    {
        public string PropertyName { get; set; }
        readonly string _fxCopOutput = XmlParser.XmlParse("File", "FxCopOutputXml");

        readonly string _fxCopXml = XmlParser.XmlParse("File", "FxCopInputFile");

        readonly string _fxCopExe = XmlParser.XmlParse("Tool", "FxCop");

        public FxCopAnalyzer()
        {
            PropertyName = "apple";
        }

        public void ProcessInput(Paths projFilePath)
        {
            XmlWriter.EditSolutionName(_fxCopXml,projFilePath.processPath,"Target","Name");
        }

        public void ProcessOutput()
        {
            string arguments = @"/p:" + _fxCopXml + @" /out:" + _fxCopOutput;
            try
            {
                Process proc = new Process();
                
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.FileName = _fxCopExe;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                System.Environment.Exit(1);
            }
            FxCopReport.GenerateFxCopReport(_fxCopOutput);
        }
    }
}
