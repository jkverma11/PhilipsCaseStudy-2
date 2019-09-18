using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportReaderContractsLib;
using DataModelsLib;

namespace NDependReaderLib.Tests
{
    [TestClass]
    public class NDependReaderUnitTests
    {
        private IReader _nDependReaderRef;
        [TestInitialize]
        public void Setup()
        {
            _nDependReaderRef = new NDependReader(new FakeLoggerStub());
            // Runs before each test. (Optional)
        }
        [TestMethod]
        public void Given_XmlFilePath_When_Invoked_Read_Expected_dataModelsList_Count_GreaterThan_Zero()
        {
            List<DataModel> dataModelsList=_nDependReaderRef.Read(Directory.GetCurrentDirectory()+
                "\\..\\..\\..\\StaticToolAnalyzerApi\\App_Data\\AnalyzerTools\\NDependOutput");
            Assert.IsTrue(dataModelsList.Count>0);
        }

        [TestMethod]
        public void Given_WrongXmlPath_Read_Method_Invoked__Expected_dataModelsList_Count_IsZero()
        {
            List<DataModel> dataModelsList = _nDependReaderRef.Read("");
            Assert.IsTrue(dataModelsList.Count== 0);
        }
    }
}
