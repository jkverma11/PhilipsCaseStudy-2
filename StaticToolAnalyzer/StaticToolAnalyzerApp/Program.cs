using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyzersDataLib;
using FxCopAnalyzerLib;
using FxCopReaderLib;
using NDependAnalyzerLib;
using NDependReaderLib;
using ReportReaderContractsLib;
using StaticAnalyzerContractsLib;
using StaticToolsProcessorLib;
using TextWriterLib;
using WriterContractsLib;

namespace StaticToolAnalyzerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string appDataPath = @"C:\Users\320050765\PhilipsCaseStudy-2\G7CaseStudy2-master\CaseStudy2\CaseStudy2\App_Data\";
            string fxCopOutputFilePath = appDataPath + "AnalyzerTools\\FxCopReport.xml";
            string fxCopExePath = appDataPath + "AnalyzerTools\\Microsoft Fxcop 10.0\\FxCopCmd.exe";
            string fxCopRulesFilePath = appDataPath + "AnalyzerTools\\Microsoft Fxcop 10.0\\common_fx_cop_file.FxCop";
            string userFilePath = appDataPath + "ProjectFiles\\StaticAnalyzerTool";
            string analyzerOutputFile = appDataPath + "AnalyzerOutput.txt";
            string nDependOutputFilePath = appDataPath + "AnalyzerTools\\NDependOutput";
            string nDependExePath = appDataPath + "AnalyzerTools\\NDepend_2019.2.7.9280\\NDepend.Console.exe";
            string nDependRulesFilePath = appDataPath + "AnalyzerTools\\NDepend_2019.2.7.9280\\common.ndproj";


            List<AnalyzersDataModel> analyzersDataModelsList = new List<AnalyzersDataModel>
            {
                new AnalyzersDataModel
                {
                    Name = "FxCop",
                    ExePath = fxCopExePath,
                    OutputFilePath = fxCopOutputFilePath,
                    RuleFilePath = fxCopRulesFilePath
                },
                new AnalyzersDataModel {Name="NDepend",
                    ExePath = nDependExePath,
                    OutputFilePath = nDependOutputFilePath,
                    RuleFilePath = nDependRulesFilePath
                }
            };

            IStaticAnalyzers fxCopAnalyzer = new FxCopAnalyzer(userFilePath);
            IStaticAnalyzers nDependAnalyzer = new NDependAnalyzer(userFilePath);
            IReader fxCopReader = new FxCopReader();
            IReader nDependReader = new NDependReader();
            IReportWriter textWriter = new TextReportWriter(analyzerOutputFile);

            #region  Analyzers and Readers List

            List<IStaticAnalyzers> analyzers = new List<IStaticAnalyzers>();
            List<IReader> readers = new List<IReader>();

            #endregion

            analyzers.Add(fxCopAnalyzer);
            analyzers.Add(nDependAnalyzer);
            readers.Add(fxCopReader);
            readers.Add(nDependReader);
            var manager = new StaticToolsProcessor(analyzersDataModelsList, analyzers, readers, textWriter);
            manager.Process();
        }
    }
}
