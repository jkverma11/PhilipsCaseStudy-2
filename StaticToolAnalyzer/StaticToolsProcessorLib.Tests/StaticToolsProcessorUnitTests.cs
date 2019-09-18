using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnalyzersDataLib;
using ReportReaderContractsLib;
using StaticAnalyzerContractsLib;
using WriterContractsLib;

namespace StaticToolsProcessorLib.Tests
{
    [TestClass]
    public class StaticToolsProcessorUnitTests
    {


        private List<IStaticAnalyzers> analyzersList;
        private List<IReader> readersList;
        private IReportWriter _reportWriter;
        [TestInitialize]
        public void Setup()
        {
             analyzersList = new List<IStaticAnalyzers> { new FakeStaticAnalyzerStub() };
            readersList = new List<IReader> { new FakeReportReaderStub() };
            _reportWriter = new FakeWriterStub() ;
        }
        [TestMethod]
        public void Given_Correct_AnalyzerData_When_Process_Method_Invoked_Expected_Value_IsTrue()
        {
            List<AnalyzersDataModel> analyzersDataList = new List<AnalyzersDataModel>
            {
                new AnalyzersDataModel
                {
                    Name = "FakeAnalyzer", ExePath = "", OutputFilePath = "OutputPath", RuleFilePath = ""
                }
            };
            StaticToolsProcessor staticToolsProcessorRef = new StaticToolsProcessor(analyzersDataList,analyzersList,readersList,_reportWriter);
            Assert.IsTrue(staticToolsProcessorRef.Process());
        }

        [TestMethod]
        public void Given_Wrong_AnalyzerData_When_Process_Method_Invoked_Expected_Value_IsFalse()
        {
            List<AnalyzersDataModel> analyzersDataList = new List<AnalyzersDataModel>
            {
                new AnalyzersDataModel
                {
                    Name = "", ExePath = "", OutputFilePath = "OutputPath", RuleFilePath = ""
                }
            };
            StaticToolsProcessor staticToolsProcessorRef = new StaticToolsProcessor(analyzersDataList, analyzersList, readersList, _reportWriter);
            Assert.IsFalse(staticToolsProcessorRef.Process());
        }
    }
}
