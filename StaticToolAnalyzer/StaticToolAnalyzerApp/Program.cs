using AnalyzersDataLib;
using System.Collections.Generic;
using FxCopAnalyzerLib;
using FxCopReaderLib;
using NDependAnalyzerLib;
using NDependReaderLib;
using ReportReaderContractsLib;
using StaticAnalyzerContractsLib;
using StaticToolsProcessorLib;
using TextWriterLib;
using WriterContractsLib;
using ConsoleLoggerLib;
using StaticAnalyzerUtilitiesLib;

namespace StaticToolAnalyzerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string appDataPath = @"C:\\Users\\320050765\\PhilipsCaseStudy-2\\CaseStudy2\\CaseStudy2\\App_Data\\";
            string fxCopOutputFilePath = appDataPath + @"AnalyzerTools\\FxCopReport.xml";
            string fxCopExePath = appDataPath + @"AnalyzerTools\\Microsoft_Fxcop_10.0\\FxCopCmd.exe";
            string fxCopRulesFilePath = appDataPath + "AnalyzerTools\\Microsoft_Fxcop_10.0\\common_fx_cop_file.FxCop";
            string userFilePath = appDataPath + "ProjectFiles\\StaticAnalyzerTool";
            string analyzerOutputFile = appDataPath + "AnalyzerOutput.csv";
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

           

            IStaticAnalyzers fxCopAnalyzer = new FxCopAnalyzer(userFilePath,new StaticAnalyzerUtilities(new ConsoleLogger()));
            
            IStaticAnalyzers nDependAnalyzer = new NDependAnalyzer(userFilePath, new StaticAnalyzerUtilities(new ConsoleLogger()));
            IReader fxCopReader = new FxCopReader(new ConsoleLogger());
            IReader nDependReader = new NDependReader(new ConsoleLogger());
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
