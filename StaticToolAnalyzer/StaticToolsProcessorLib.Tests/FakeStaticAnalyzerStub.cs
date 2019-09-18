using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyzersDataLib;
using StaticAnalyzerContractsLib;

namespace StaticToolsProcessorLib.Tests
{
    class FakeStaticAnalyzerStub : IStaticAnalyzers
    {
        public bool ProcessInput()
        {
            return true;
        }

        public bool ProcessOutput()
        {
            return true;
        }

        public string AnalyzerName { get; } = "FakeAnalyzer";
        public AnalyzersDataModel AnalyzersData { get; set; }
    }
}
