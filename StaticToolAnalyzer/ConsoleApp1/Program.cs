using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextWriterLib;
using DataModelsLib;
using System.Reflection;
using FxCopAnalyzerLib;
using FxCopReaderLib;
using ReportReaderContractsLib;
using StaticAnalyzerConfigurationsContractsLib;
using StaticAnalyzerContractsLib;
using StaticAnalyzerXmlConfigurationsLib;
using StaticToolsProcessorLib;
using WriterContractsLib;

namespace ConsoleApp1
{
    class Program
    {


        static void Main(string[] args)
        {

            IStaticAnalyzerConfigurations xmlConfigurations = new StaticAnalyzerXmlConfigurations();
            IStaticAnalyzers fxCopAnalyzer = new FxCopAnalyzer(xmlConfigurations);
            IReader fxCopReader = new FxCopReader(xmlConfigurations);
            IWriter textWriter = new TextWriter();

            #region  Analyzers and Readers List

            List<IStaticAnalyzers> analyzers = new List<IStaticAnalyzers>();
            List<IReader> readers = new List<IReader>();

            #endregion

            analyzers.Add(fxCopAnalyzer);
            readers.Add(fxCopReader);
            var manager = new StaticToolsProcessor(analyzers, readers, textWriter);

            manager.Process();


        }      
    }
}
