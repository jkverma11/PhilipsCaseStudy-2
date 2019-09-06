using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using TextWriterLib;
using FxCopAnalyzerLib;
using FxCopReaderLib;
using ReportReaderContractsLib;
using StaticAnalyzerConfigurationsContractsLib;
using StaticAnalyzerContractsLib;
using StaticAnalyzerXmlConfigurationsLib;
using StaticToolsProcessorLib;
using WriterContractsLib;
using System.Configuration;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {

            string inputConfigFilePath = ConfigurationManager.AppSettings.Get("InputConfigFilePath");
            string outputFilePath = ConfigurationManager.AppSettings.Get("OutputFilePath");

            IStaticAnalyzerConfigurations xmlConfigurations = new StaticAnalyzerXmlConfigurations(inputConfigFilePath);
            IStaticAnalyzers fxCopAnalyzer = new FxCopAnalyzer(xmlConfigurations);
            IReader fxCopReader = new FxCopReader(xmlConfigurations);
            IWriter textWriter = new TextWriter(outputFilePath);

            #region  Analyzers and Readers List

            List<IStaticAnalyzers> analyzers = new List<IStaticAnalyzers>();
            List<IReader> readers = new List<IReader>();

            #endregion

            analyzers.Add(fxCopAnalyzer);
            readers.Add(fxCopReader);
            readers.Add(fxCopReader);
            var manager = new StaticToolsProcessor(analyzers, readers, textWriter);

            manager.Process();


        }      
    }
}
