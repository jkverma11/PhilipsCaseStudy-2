using System;
using System.Collections.Generic;
using System.IO;
using DataModelsLib;
using LoggersContractLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WriterContractsLib;

namespace TextWriterLib.Tests
{
    [TestClass]
    public class TextWriterUnitTest
    {       
        [TestMethod]
        public void Given_OutputFilePath_WhenWriteIsInvokedAndFileIsCreated_ThenSuccessIsExpected()
        {
            var outputTestFilePath = Directory.GetCurrentDirectory() + "\\UnitTestOutput.txt";
            IReportWriter reportWriterRef = new TextReportWriter(outputTestFilePath);
            
            List<DataModel> dataModels = new List<DataModel>
            {
                new DataModel
                {
                    ErrorCount = "",
                    ErrorMsg = "",
                    ErrorCertainty = "",
                    ErrorType = "",
                    FileName = "",
                    StaticAnalyzerTool = "",
                    LineNumber = "",
                    FilePath = "",
                    TimeStamp = DateTime.Now
                }
            };
            Assert.IsTrue(reportWriterRef.Write(dataModels));
            File.Delete(outputTestFilePath);
        }
    }
}
