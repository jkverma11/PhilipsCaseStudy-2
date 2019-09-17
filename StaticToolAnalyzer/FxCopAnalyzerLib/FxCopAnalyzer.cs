using System.Collections.Generic;
using System.Diagnostics;
using StaticAnalyzerContractsLib;
using StaticAnalyzerUtilitiesLib;
using System;
using AnalyzersDataLib;
using StaticAnalyzerUtilitiesContractsLib;

namespace FxCopAnalyzerLib
{
    public sealed class FxCopAnalyzer : IStaticAnalyzers
    {
        #region Private Fields
        private readonly string _userFilePath;
        private IStaticAnalyzerUtilities _staticAnalyzerUtilities;

        public string AnalyzerName { get; } = "FxCop";
        public AnalyzersDataModel AnalyzersData { get; set; }

        #endregion

        #region Initialyzer
        public FxCopAnalyzer(string userFilePath,IStaticAnalyzerUtilities StaticAnalyzerUtilities)
        {
            
            _userFilePath = userFilePath;
            this._staticAnalyzerUtilities = StaticAnalyzerUtilities;
        }
        #endregion

        //#region Property
        //public IStaticAnalyzerUtilities StaticAnalyzerUtilities
        //{ get { return this._staticAnalyzerUtilities; }
        //    set { this._staticAnalyzerUtilities = value; } }
        //#endregion

        #region Public Methods

        public bool ProcessInput()

        {
            bool successStatus = false;
            List<string> assembliesList = _staticAnalyzerUtilities.GetPaths(_userFilePath, "*.exe");
            if (assembliesList.Count > 0)
            {
                successStatus = _staticAnalyzerUtilities.ChangeSolutionPath(AnalyzersData.RuleFilePath, assembliesList, "Targets", "Target", "Name");
            }
            return successStatus;
        }

        public bool ProcessOutput()
        {
            string arguments = @"/p:" + AnalyzersData.RuleFilePath + @" /out:" + AnalyzersData.OutputFilePath;
            bool successStatus =
                _staticAnalyzerUtilities.RunAnalyzerProcess(arguments, AnalyzersData.ExePath, ProcessWindowStyle.Hidden);
            return successStatus;
        }

        #endregion

    }
}
