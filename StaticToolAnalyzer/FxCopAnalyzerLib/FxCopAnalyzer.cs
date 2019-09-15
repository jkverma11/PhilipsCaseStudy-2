using System.Collections.Generic;
using System.Diagnostics;
using StaticAnalyzerContractsLib;
using StaticAnalyzerUtilitiesLib;
using System;
using AnalyzersDataLib;

namespace FxCopAnalyzerLib
{
    public sealed class FxCopAnalyzer : IStaticAnalyzers
    {
        #region Private Fields
        private readonly string _userFilePath;

        public string AnalyzerName { get; } = "FxCop";
        public AnalyzersDataModel AnalyzersData { get; set; }

        #endregion

        #region Initialyzer
        public FxCopAnalyzer(string userFilePath)
        {
            
            _userFilePath = userFilePath;
        }
        #endregion

        #region Public Methods
<<<<<<< HEAD
        public bool ProcessInput()
=======
       public bool ProcessInput()
>>>>>>> 3c25f97ea691b0fcd5b702498a82170d04e9d065
        {
            bool successStatus = false;
            List<string> assembliesList = StaticAnalyzerUtilities.GetPaths(_userFilePath, "*.exe");
            if (assembliesList.Count > 0)
            {
                successStatus = StaticAnalyzerUtilities.ChangeSolutionPath(AnalyzersData.RuleFilePath, assembliesList, "Targets", "Target", "Name");
            }
            return successStatus;
        }

        public bool ProcessOutput()
        {
            string arguments = @"/p:" + AnalyzersData.RuleFilePath + @" /out:" + AnalyzersData.OutputFilePath;
            bool successStatus =
                StaticAnalyzerUtilities.RunAnalyzerProcess(arguments, AnalyzersData.ExePath, ProcessWindowStyle.Hidden);
            return successStatus;
        }

        #endregion

    }
}
