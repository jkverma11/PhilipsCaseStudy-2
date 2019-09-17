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
            successStatus = GetReadAndWriteAnalyzerReport(successStatus);
            return successStatus;
        }

        private bool GetAnalyzerToProcess()
        {
            var successStatus1 = false;
            var successStatus = true;
            foreach (var analyzer in _staticAnalyzers)
            {
                //_analyzersData = _analyzersDataList.Find(x => x.Name.Contains(analyzer.AnalyzerName));
                //analyzer.AnalyzersData = _analyzersData;
                successStatus1 = AnalyzerProcess(analyzer);
                successStatus = successStatus1 && successStatus;
            }
            return successStatus;
        }

        private bool GetReadAndWriteAnalyzerReport(bool successStatus)
        {
            foreach (var reportReader in _reportReaders)
            {
                //_analyzersData = _analyzersDataList.Find(x => x.Name.Contains(reportReader.Name));
                //var dataModelsList = reportReader.Read(_analyzersData.OutputFilePath);
                successStatus = ReadAndWriteAnalyzerReport(successStatus, reportReader);
                
            }
            return successStatus;
        }

        private bool AnalyzerProcess(IStaticAnalyzers analyzer )
        {
            var successStatus = false;
            _analyzersData = _analyzersDataList.Find(x => x.Name.Contains(analyzer.AnalyzerName));
            analyzer.AnalyzersData = _analyzersData;
            successStatus = analyzer.ProcessInput() && analyzer.ProcessOutput();
            return successStatus;
        }

        private bool ReadAndWriteAnalyzerReport(bool successStatus, IReader reportReader)
        {
            _analyzersData = _analyzersDataList.Find(x => x.Name.Contains(reportReader.Name));
            var dataModelsList = reportReader.Read(_analyzersData.OutputFilePath);
            successStatus = successStatus && _writer.Write(dataModelsList);
            return successStatus;

        }
        
        #endregion
    }
}
