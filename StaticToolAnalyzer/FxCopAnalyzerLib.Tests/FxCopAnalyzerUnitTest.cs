using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticAnalyzerConfigurationsContractsLib;
using StaticAnalyzerXmlConfigurationsLib;

namespace FxCopAnalyzerLib.Tests
{
    [TestClass]
    public class FxCopAnalyzerUnitTest
    {
        [TestMethod]
        public void Given_FxCopRulesFilePathAndUserCode_When_ProcessInputIsCalled_Then_SuccessStatusIsTrue()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePaths.xml";
            IStaticAnalyzerConfigurations configFilePath=new StaticAnalyzerXmlConfigurations(xmlFilePath);
            bool expectedOutcome=new FxCopAnalyzer(configFilePath).ProcessInput();
            bool actualOutcome = true;
            Assert.AreEqual(expectedOutcome,actualOutcome);


        }

        [TestMethod]
        public void Given_InputConfigurations_When_ProcessOutputIsCalled_Then_SuccessStatusIsTrue()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePaths.xml";
            IStaticAnalyzerConfigurations configFilePath = new StaticAnalyzerXmlConfigurations(xmlFilePath);
            bool expectedOutcome = new FxCopAnalyzer(configFilePath).ProcessOutput();
            bool actualOutcome = true;
            Assert.AreEqual(expectedOutcome, actualOutcome);


        }

        [TestMethod]
        public void Given_WrongFilePathConfigurations_When_ProcessInputIsCalled_Then_SuccessStatusIsFalse()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePath.xml";
            IStaticAnalyzerConfigurations configFilePath = new StaticAnalyzerXmlConfigurations(xmlFilePath);
            bool expectedOutcome = new FxCopAnalyzer(configFilePath).ProcessInput();
            bool actualOutcome = false;
            Assert.AreEqual(expectedOutcome, actualOutcome);


        }

        [TestMethod]
        public void Given_WrongFilePathConfigurations_When_ProcessOutputIsCalled_Then_SuccessStatusIsFalse()
        {
            var xmlFilePath = @"C:\Users\320067256\PhilipsCaseStudy-2\TestInputExePath.xml";
            IStaticAnalyzerConfigurations configFilePath = new StaticAnalyzerXmlConfigurations(xmlFilePath);
            bool expectedOutcome = new FxCopAnalyzer(configFilePath).ProcessInput();
            bool actualOutcome = false;
            Assert.AreEqual(expectedOutcome, actualOutcome);


        }
    }
}
