using System.Collections.Generic;
using AnalyzersDataLib;
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
        private readonly IReportWriter _writer;
        private readonly List<AnalyzersDataModel> _analyzersDataList;
        private AnalyzersDataModel _analyzersData;
        #endregion

        #region Initializer

        public StaticToolsProcessor(List<AnalyzersDataModel> analyzersDataList,List<IStaticAnalyzers> staticAnalyzers,List<IReader> readers,IReportWriter writer)
        {            _staticAnalyzers = staticAnalyzers;

            _analyzersDataList = analyzersDataList;
            _reportReaders = readers;
            _writer = writer;
        }

        #endregion


        #region Public Methods

        public bool Process()
        {
            bool successStatus = false;
            successStatus = GetAnalyzerToProcess();
            successStatus = successStatus && GetReadAndWriteAnalyzerReport();
            return successStatus;
        }

        private bool GetAnalyzerToProcess()
        {
            var successStatus = true;
            foreach (var analyzer in _staticAnalyzers)
            {
                var successStatus1 = AnalyzerProcess(analyzer);
                successStatus = successStatus1 && successStatus;
            }
            return successStatus;
        }

        private bool GetReadAndWriteAnalyzerReport()
        {
            bool successStatus = false;
            foreach (var reportReader in _reportReaders)
            {
                successStatus = ReadAndWriteAnalyzerReport(reportReader);
            }
            return successStatus;
        }

        private bool AnalyzerProcess(IStaticAnalyzers analyzer )
        {
            var successStatus = false;
            _analyzersData = _analyzersDataList.Find(x => x.Name.Contains(analyzer.AnalyzerName));
            if (_analyzersData != null)
            {
                analyzer.AnalyzersData = _analyzersData;
                successStatus = analyzer.ProcessInput() && analyzer.ProcessOutput();
            }
            return successStatus;
        }

        private bool ReadAndWriteAnalyzerReport(IReader reportReader)
        {
            bool successStatus = false;
            _analyzersData = _analyzersDataList.Find(x => x.Name.Contains(reportReader.Name));
            if (_analyzersData != null)
            {
                var dataModelsList = reportReader.Read(_analyzersData.OutputFilePath);
                successStatus = _writer.Write(dataModelsList);
            }
            return successStatus;
        }
        
        #endregion
    }
}
