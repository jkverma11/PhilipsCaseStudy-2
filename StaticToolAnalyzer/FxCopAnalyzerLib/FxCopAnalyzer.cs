using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticAnalyzerContractsLib;
using StaticAnalyzerConfigurationsContractsLib;
using StaticAnalyzerUtilitiesLib;

namespace FxCopAnalyzerLib
{
    public class FxCopAnalyzer : IStaticAnalyzers
    {
        private IStaticAnalyzerConfigurations _configurationsRef;
        private string _fxCopExePath;
        private string _userCodeExePath;
        private string _fxCopRulesFilePath;
        private string _fxCopOutputFilePath;
        public FxCopAnalyzer(IStaticAnalyzerConfigurations configurationsRef)
        {
            _configurationsRef = configurationsRef;
            _fxCopExePath = _configurationsRef.GetAnalyzerExePath("FxCop");
            _userCodeExePath = _configurationsRef.GetUserCodeExePath();
            _fxCopRulesFilePath = _configurationsRef.GetAnalyzerRulesFilePath("FxCop");
            _fxCopOutputFilePath = _configurationsRef.GetAnalyzerOutputFilePath("FxCop");
        }
        bool IStaticAnalyzers.ProcessInput()
        {
            bool successStatus = StaticAnalyzerUtilities.ChangeSolutionPath(_fxCopRulesFilePath, _userCodeExePath, "Target", "Name");

            return successStatus;
        }

        bool IStaticAnalyzers.ProcessOutput()
        {
            string arguments = @"/p:" + _fxCopRulesFilePath + @" /out:" + _fxCopOutputFilePath;
            bool successStatus =
                StaticAnalyzerUtilities.RunAnalyzerProcess(arguments, _fxCopExePath, ProcessWindowStyle.Hidden);
            return successStatus;
        }
    }
}
