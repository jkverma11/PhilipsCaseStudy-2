using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticAnalyzerUtilitiesContractsLib;
using System.IO;



namespace StaticAnalyzerUtilitiesLib.Tests
{
    [TestClass]
    public class StaticAnalyzerUtilitiesUnitTest
    {
        [TestMethod]
        public void Given_AnalyzerExePath_When_RunAnalyzerProcess_Then_StatusIsTrue()
        {
            IStaticAnalyzerUtilities staticAnalyzerUtilities=new StaticAnalyzerUtilities(new FakeLogger());
            bool expectedOutcome=staticAnalyzerUtilities.RunAnalyzerProcess("","calc.exe", ProcessWindowStyle.Hidden);
            Assert.AreEqual(expectedOutcome,true);
        }

        [TestMethod]
        
        public void Given_WrongAnalyzerExePath_When_RunAnalyzerProcess_Then_StatusIsFalse()
        {
            IStaticAnalyzerUtilities staticAnalyzerUtilities = new StaticAnalyzerUtilities(new FakeLogger());
            bool expectedOutcome = staticAnalyzerUtilities.RunAnalyzerProcess("", "cal.exe", ProcessWindowStyle.Hidden);
            Assert.AreEqual(expectedOutcome, false);
        }

        [TestMethod]
        public void Given_DirectoryPath_When_GetPathIsInvoked_Then_CountIsThree()
        {
            var dirPath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\StaticToolAnalyzerApi\\App_Data\\TestDirectories";
            Console.WriteLine(dirPath);
            IStaticAnalyzerUtilities staticAnalyzerUtilities = new StaticAnalyzerUtilities(new FakeLogger());
            List<string> filePaths = staticAnalyzerUtilities.GetPaths(dirPath, "*.txt");
            int expectedOutcome = filePaths.Count;
            int actualOutcome = 3;
            Assert.AreEqual(expectedOutcome,actualOutcome);

        }

        [TestMethod]
        public void Given_XmlPath_When_CreateDuplicateNodesInvoked_Then_StatusIsTrue()
        {
            var ruleXmlPath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\StaticToolAnalyzerApi\\App_Data\\TestDirectories\\test.xml";
            List<string> userFilePathList=new List<string>();
            userFilePathList.Add("app.txt");
            IStaticAnalyzerUtilities staticAnalyzerUtilities = new StaticAnalyzerUtilities(new FakeLogger());
            bool expectedOutcome=staticAnalyzerUtilities.ChangeSolutionPath(ruleXmlPath, userFilePathList, "Targets", "Target", "Name");
            Assert.AreEqual(expectedOutcome,true);
        }

    }
}
