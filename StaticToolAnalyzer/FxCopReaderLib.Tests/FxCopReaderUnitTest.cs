using System.Collections.Generic;
using System.IO;
using DataModelsLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportReaderContractsLib;

namespace FxCopReaderLib.Tests
{
    [TestClass]
    public class FxCopReaderUnitTest
    {
        private IReader _fxCopReaderRef;
        [TestInitialize]
        public void Setup()
        {
            _fxCopReaderRef = new FxCopReader(new FakeLoggerStub());
        }
        [TestMethod]
        public void Given_XmlFilePath_When_Invoked_Read_Expected_dataModelsList_Count_GreaterThan_Zero()
        {
            List<DataModel> dataModelsList = _fxCopReaderRef.Read(Directory.GetCurrentDirectory() + "\\..\\..\\..\\StaticToolAnalyzerApi\\App_Data\\AnalyzerTools\\FxCopReport.xml");
            Assert.IsTrue(dataModelsList.Count > 0);
        }

        [TestMethod]
        public void Given_WrongXmlPath_Read_Method_Invoked__Expected_dataModelsList_Count_IsZero()
        {
            List<DataModel> dataModelsList = _fxCopReaderRef.Read("");
            Assert.IsTrue(dataModelsList.Count == 0);
        }
    }
}
