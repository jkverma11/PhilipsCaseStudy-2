using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;
using ReportReaderContractsLib;

namespace StaticToolsProcessorLib.Tests
{
    class FakeReportReaderStub : IReader
    {
        public List<DataModel> Read(string analyzerOutputFilePath)
        {
            List<DataModel> dataModelsList = new List<DataModel>();
            if (analyzerOutputFilePath != "")
            {
                dataModelsList.Add(new DataModel
                {
                    ErrorMsg = "",
                    ErrorCount = "",
                    ErrorType = "",
                    ErrorCertainty = "",
                    FileName = "",
                    FilePath="",
                    LineNumber = "",
                    StaticAnalyzerTool="",
                    TimeStamp = DateTime.Now

                });
            }

            return dataModelsList;
        }

        public string Name { get; } = "FakeAnalyzer";
    }
}
