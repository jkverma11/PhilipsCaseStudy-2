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
        #region Private Fields
        private readonly string _fxCopExePath;
        private readonly string _userCodeExePath;
        private readonly string _fxCopRulesFilePath;
        private readonly string _fxCopOutputFilePath;
        #endregion

        #region Initialyzer
        public FxCopAnalyzer(IStaticAnalyzerConfigurations configurations)
        {
            var configurationsRef = configurations;
            _fxCopExePath = configurationsRef.GetAnalyzerExePath("FxCop");
            _userCodeExePath = configurationsRef.GetUserCodeExePath();
            _fxCopRulesFilePath = configurationsRef.GetAnalyzerRulesFilePath("FxCop");
            _fxCopOutputFilePath = configurationsRef.GetAnalyzerOutputFilePath("FxCop");
        }

        #endregion

        #region Public Methods
       public bool ProcessInput()
        {
            bool successStatus = StaticAnalyzerUtilities.ChangeSolutionPath(_fxCopRulesFilePath, _userCodeExePath, "Target", "Name");

            return successStatus;
        }

        public bool ProcessOutput()
        {
            string arguments = @"/p:" + _fxCopRulesFilePath + @" /out:" + _fxCopOutputFilePath;
            bool successStatus =
                StaticAnalyzerUtilities.RunAnalyzerProcess(arguments, _fxCopExePath, ProcessWindowStyle.Hidden);
            return successStatus;
        }


        #endregion

    }
}
