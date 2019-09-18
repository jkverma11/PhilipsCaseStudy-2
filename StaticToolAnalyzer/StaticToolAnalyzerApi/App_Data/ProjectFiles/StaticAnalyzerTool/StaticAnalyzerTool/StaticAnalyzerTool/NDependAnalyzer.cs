using System;
using System.Diagnostics;

namespace StaticAnalyzerTool
{
    class NDependAnalyzer : IStaticAnalyzer
    {
        readonly string _nDependXml = XmlParser.XmlParse("File", "NDependInputFile");

        readonly string _ndependOut = XmlParser.XmlParse("File", "NDependOutputXml");

        readonly string _nDependExe = XmlParser.XmlParse("Tool", "NDepend");

        public void ProcessInput( Paths projFilePath)
        {
            XmlWriter.EditSolutionName(_nDependXml, projFilePath.solutionPath, "IDEFile", "FilePath");
        }

        public void ProcessOutput()
        {
            string arguments = _nDependXml + @" /LogTrendMetrics /OutDir " + _ndependOut;
            try
            {
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Arguments = arguments;
                proc.StartInfo.FileName = _nDependExe;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                System.Environment.Exit(1);
            }
            string ndependOutputXml = _ndependOut + @"\TrendMetrics\NDependTrendData2019.xml";
            NDependXmlReport.GenerateNDependReport(ndependOutputXml);
        }
    }
}
