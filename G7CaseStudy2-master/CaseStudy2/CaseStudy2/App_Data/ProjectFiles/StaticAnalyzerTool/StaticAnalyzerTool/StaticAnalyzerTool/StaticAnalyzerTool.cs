using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzerTool
{
    struct Paths
    {
        public string processPath;
        public string solutionPath;
    }
    interface IStaticAnalyzer
    {
        void ProcessInput(Paths projFilePath);
        void ProcessOutput();
    }
}
 