using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FxCopReaderLib.Tests
{
    class FakeReadConfigurationStub : StaticAnalyzerConfigurationsContractsLib.IStaticAnalyzerConfigurations
    {
        public string GetAnalyzerExePath(string toolName)
        {
            throw new NotImplementedException();
        }

        public string GetAnalyzerOutputFilePath(string toolName)
        {
            return @"C:\Users\320067256\PhilipsCaseStudy-2\TestFxcopForReader.xml";
        }

        public string GetAnalyzerRulesFilePath(string toolName)
        {
            throw new NotImplementedException();
        }

        public string GetUserCodeExePath()
        {
            throw new NotImplementedException();
        }

        public string GetUserCodeSolutionPath()
        {
            throw new NotImplementedException();
        }
    }
}
