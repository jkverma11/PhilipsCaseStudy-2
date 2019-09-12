using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace StaticAnalyzerXmlConfigurations.Tests
{
    [TestClass]
    public class StaticAnalyzerXmlConfigurationsUnitTest
    {
        [TestMethod]
        public void Given_XmlFilePath_When_GetAnalyzerExePathInvoked_Then_AnalyzerExePathChecked()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePaths.xml";
            var expectedoutcome = new StaticAnalyzerXmlConfigurationsLib.StaticAnalyzerXmlConfigurations(xmlFilePath).GetAnalyzerExePath("FxCop");
            var actualOutcome = @"C:\Program Files (x86)\Microsoft Fxcop 10.0\FxCopCmd.exe";
            Assert.AreEqual(expectedoutcome,actualOutcome);
        }

        [TestMethod]
        public void Given_XmlFilePath_When_GetUserCodeExePathInvoked_Then_UserCodeExePathExpected()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePaths.xml";
            var expectedoutcome = new StaticAnalyzerXmlConfigurationsLib.StaticAnalyzerXmlConfigurations(xmlFilePath).GetUserCodeExePath();
            var actualOutcome = @"C:\Users\320067256\StaticAnalysisTool\StaticAnalyzerTool\StaticAnalyzerTool\bin\Debug\StaticAnalyzerTool.exe";
            Assert.AreEqual(expectedoutcome, actualOutcome);
        }

        [TestMethod]
        public void Given_XmlFilePath_When_GetUserCodeSlnPathInvoked_Then_UserCodeSlnPathExpected()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePaths.xml";
            var expectedoutcome = new StaticAnalyzerXmlConfigurationsLib.StaticAnalyzerXmlConfigurations(xmlFilePath).GetUserCodeSolutionPath();
            var actualOutcome = @"C:\Users\320067256\ChangesSaved\CaseStudy1-brancth_patch1\CaseStudy1-brancth_patch1\AlertSystemServer\AlertSystemServer.sln";
            Assert.AreEqual(expectedoutcome, actualOutcome);
        }

        [TestMethod]
        public void Given_XmlFilePath_When_GetAnalyzerRulesFilePathInvoked_Then_AnalyzerRuleFilePathExpected()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePaths.xml";
            var expectedoutcome = new StaticAnalyzerXmlConfigurationsLib.StaticAnalyzerXmlConfigurations(xmlFilePath).GetAnalyzerRulesFilePath("FxCop");
            var actualOutcome = @"C:\Users\320067256\StaticAnalysisTool\common_fx_cop_file.FxCop";
            Assert.AreEqual(expectedoutcome, actualOutcome);
        }

        [TestMethod]
        public void Given_XmlFilePath_When_GetAnalyzerOutputFilePathInvoked_Then_AnalyzerOutputFilePathChecked()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePaths.xml";
            var expectedoutcome = new StaticAnalyzerXmlConfigurationsLib.StaticAnalyzerXmlConfigurations(xmlFilePath).GetAnalyzerOutputFilePath("FxCop");
            var actualOutcome = @"C:\Users\320067256\PhilipsCaseStudy-2\TestFxCop.xml";
            Assert.AreEqual(expectedoutcome, actualOutcome);
        }


        
        
        public void Given_WrongXmlFilePath_When_GetAnalyzerOutputFilePathInvoked_Then_FileNotFoundMessageExpected()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePath.xml";
            var expectedoutcome = new StaticAnalyzerXmlConfigurationsLib.StaticAnalyzerXmlConfigurations(xmlFilePath).GetAnalyzerOutputFilePath("FxCop");
            var actualOutcome = @"C:\Users\320067256\PhilipsCaseStudy-2\TestFxCop.xml";
            Assert.AreEqual(expectedoutcome, actualOutcome);
        }

    }
}
