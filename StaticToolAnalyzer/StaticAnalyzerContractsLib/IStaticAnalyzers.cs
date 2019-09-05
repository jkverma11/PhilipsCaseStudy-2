using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzerContractsLib
{
    public interface IStaticAnalyzers
    {
        bool ProcessInput();
        bool ProcessOutput();
    }
}
