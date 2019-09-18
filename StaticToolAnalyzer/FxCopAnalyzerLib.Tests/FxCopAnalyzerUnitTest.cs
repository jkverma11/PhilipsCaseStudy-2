using System;
using AnalyzersDataLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticAnalyzerContractsLib;

namespace FxCopAnalyzerLib.Tests
{
    [TestClass]
    public class FxCopAnalyzerUnitTest
    {
        [TestMethod]
        public void Given_UserFilePath_When_ProcessInput_Invoked_Then_ExpectedTo_Return_False()
        {
            IStaticAnalyzers nDependAnalyzer = new FxCopAnalyzer("dir", new FakeUtilitiesStub());

            nDependAnalyzer.AnalyzersData = new AnalyzersDataModel { ExePath = "", Name = "", OutputFilePath = "", RuleFilePath = "" };
            Assert.IsFalse(nDependAnalyzer.ProcessInput());
        }

        [TestMethod]
        public void Given_UserFilePath_ANd_AnalyzerRuleFilePath_When_ProcessInput_Invoked_Then_ExpectedTo_Return_True()
        {
            IStaticAnalyzers nDependAnalyzer = new FxCopAnalyzer("dir", new FakeUtilitiesStub());

            nDependAnalyzer.AnalyzersData = new AnalyzersDataModel
                { ExePath = "", Name = "", OutputFilePath = "", RuleFilePath = "ruleFile" };
            Assert.IsTrue(nDependAnalyzer.ProcessInput());
        }




        [TestMethod]
        public void Given_AnalyzersData_When_ProcessOutput_Invoked_Then_Expected_True()
        {
            IStaticAnalyzers nDependAnalyzer = new FxCopAnalyzer("", new FakeUtilitiesStub());
            nDependAnalyzer.AnalyzersData = new AnalyzersDataModel { ExePath = "", Name = "", OutputFilePath = "", RuleFilePath = "" };
            Assert.IsTrue(nDependAnalyzer.ProcessOutput());
        }

    }
}
