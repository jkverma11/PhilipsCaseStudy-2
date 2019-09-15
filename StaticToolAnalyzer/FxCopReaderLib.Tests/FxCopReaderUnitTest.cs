using System;
using System.Collections.Generic;
using System.IO;
using DataModelsLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FxCopReaderLib;
using StaticAnalyzerConfigurationsContractsLib;

namespace FxCopReaderLib.Tests
{
    [TestClass]
    public class FxCopReaderUnitTest
    {
        [TestMethod]
        public void Given_XmlFilewithOneIssueAsElement_When_ReadIsInvoked_Then_CountOfDataModelsListIsOne()
        {
           // var xmlfilepath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePaths.xml";
            IStaticAnalyzerConfigurations readTestConfiguration=new FakeReadConfigurationStub();
            var fxCopRead=new FxCopReaderLib.FxCopReader(readTestConfiguration);
            System.Collections.Generic.List<DataModel> testDataModels = fxCopRead.Read();
            int actualOutcome = testDataModels.Count;
            int expectedOutcome = 1;
            Assert.AreEqual(expectedOutcome,actualOutcome);
        }
    }
}
