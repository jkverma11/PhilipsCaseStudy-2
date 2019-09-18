using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticAnalyzerUtilitiesContractsLib;

namespace FxCopAnalyzerLib.Tests
{
    class FakeUtilitiesStub: IStaticAnalyzerUtilities
    {
        public bool RunAnalyzerProcess(string arguments, string analyzerExePath, ProcessWindowStyle windowStyle)
        {
            return true;
        }

        public bool ChangeSolutionPath(string analyzerRuleFilePath, List<string> userCodePath, string node, string elementName,
            string attributeName)
        {
            if (analyzerRuleFilePath != "")
            {
                return true;
            }
            return false;
        }

        public List<string> GetPaths(string directoryPath, string extension)
        {
            List<string> pathsList = new List<string>();
            if (directoryPath == "dir")
            {
                pathsList.Add("");
            }

            return pathsList;
        }
    }
}
