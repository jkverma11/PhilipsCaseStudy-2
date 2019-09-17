using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzerUtilitiesContractsLib
{
    public interface IStaticAnalyzerUtilities
    {
        bool RunAnalyzerProcess(string arguments, string analyzerExePath, ProcessWindowStyle windowStyle);
        bool ChangeSolutionPath(string analyzerRuleFilePath, List<string> userCodePath, string node, string elementName, string attributeName);
        List<string> GetPaths(string directoryPath, string extension);
        bool CreateDuplicateNodes(string xmlPath, string node, string element, int count);
    }
}
