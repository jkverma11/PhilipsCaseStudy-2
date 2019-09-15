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
        {
            _analyzersDataList = analyzersDataList;
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
                _analyzersData = _analyzersDataList.Find(x => x.Name.Contains(analyzer.AnalyzerName));
                    analyzer.AnalyzersData = _analyzersData;
                    successStatus=analyzer.ProcessInput()&&analyzer.ProcessOutput();                   
            }
            foreach (var reportReader in _reportReaders)
            {
<<<<<<< HEAD
                _analyzersData = _analyzersDataList.Find(x => x.Name.Contains(reportReader.Name));
                    var dataModelsList = reportReader.Read(_analyzersData.OutputFilePath);
                    successStatus = successStatus && _writer.Write(dataModelsList);
=======
                var dataModelsList = reportReader.Read();
                successStatus = successStatus && _writer.Write(dataModelsList);
>>>>>>> 3c25f97ea691b0fcd5b702498a82170d04e9d065
            }
            return successStatus;
        }

        #endregion
    }
}
