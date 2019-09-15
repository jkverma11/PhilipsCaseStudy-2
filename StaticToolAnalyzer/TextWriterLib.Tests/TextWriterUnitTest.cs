using System;
using System.Collections.Generic;
using DataModelsLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextWriterLib.Tests
{
    [TestClass]
    public class TextWriterUnitTest
    {
       

        [TestMethod]
        public void Given_OutputFilePath_WhenWriteIsInvokedAndFileIsCreated_ThenSuccessIsExpected()
        {
            var outputTestFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\AnalyzerTestOutput.txt";
            List<DataModel> dataModels = new List<DataModel>();
            DataModel dataModel = new DataModel
            {
                StaticAnalyzerTool = "FxCop",
                ErrorCertainty = "12",
                ErrorCount = "20",
                ErrorMsg = "xxx",
                ErrorType = "filenotfound",
                FileName = "sdf.txt",
                FilePath = "http://google.com",
                LineNumber = "20"
            };
            dataModels.Add(dataModel);
            TextWriter textWriter = new TextWriter(outputTestFilePath);
            bool expectedOutcome = textWriter.Write(dataModels);
            bool actualOutcome = true;
            Assert.AreEqual(expectedOutcome, actualOutcome);
        }


    }
}
