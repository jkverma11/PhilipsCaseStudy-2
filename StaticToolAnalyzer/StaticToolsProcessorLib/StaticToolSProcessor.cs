using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticAnalyzerContractsLib;
using ReportReaderContractsLib;
using WriterContractsLib;

namespace StaticToolsProcessorLib
{
    public class StaticToolsProcessor
    {
        #region Private Fields
        private readonly List<IStaticAnalyzers> _staticAnalyzers;
        private readonly List<IReader> _reportReaders;
        private readonly IWriter _writer;
        #endregion

        #region Initializer

        public StaticToolsProcessor(List<IStaticAnalyzers> staticAnalyzers,List<IReader> readers,IWriter writer)
        {
            _staticAnalyzers = staticAnalyzers;
            _reportReaders = readers;
            _writer = writer;
        }

        #endregion


        #region Public Methods

        public bool Process()
        {
            bool successStatus = false;
            foreach (var analyzer in _staticAnalyzers)
            {
                successStatus = analyzer.ProcessInput() && analyzer.ProcessOutput();
            }

            foreach (var reportReader in _reportReaders)
            {
                var dataModelsList = reportReader.Read();
                successStatus=successStatus && _writer.Write(dataModelsList);
            }

            return successStatus;
        }

        #endregion
    }
}
