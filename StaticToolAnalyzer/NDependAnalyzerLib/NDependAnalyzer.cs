using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticAnalyzerContractsLib;
using AnalyzersDataLib;
using StaticAnalyzerUtilitiesLib;
using StaticAnalyzerUtilitiesContractsLib;

namespace NDependAnalyzerLib
{
    public class NDependAnalyzer : IStaticAnalyzers
    {
        #region private Fields

        private readonly string _userFilePath;
        private readonly StaticAnalyzerUtilitiesContractsLib.IStaticAnalyzerUtilities _staticAnalyzerUtility;

        #endregion

        #region Properties
        public string AnalyzerName { get; } = "NDepend";

        public AnalyzersDataModel AnalyzersData { get; set; }

        #endregion

        #region Initializer

        public NDependAnalyzer(string userFilePath, IStaticAnalyzerUtilities staticAnalyzerUtility)
        {
            _userFilePath = userFilePath;
            this._staticAnalyzerUtility = staticAnalyzerUtility;
        }

        #endregion

        #region Public Methods

        public bool ProcessInput()
        {
            bool successStatus = false;
            List<string> assembliesList = _staticAnalyzerUtility.GetPaths(_userFilePath, "*.sln");
            if (assembliesList.Count > 0)
            {
                successStatus = _staticAnalyzerUtility.ChangeSolutionPath(AnalyzersData.RuleFilePath, assembliesList, "IDEFiles", "IDEFile", "FilePath");
            }
            return successStatus;
        }

        public bool ProcessOutput()
        {
            string arguments = AnalyzersData.RuleFilePath + @" /LogTrendMetrics /OutDir " + AnalyzersData.OutputFilePath;
            bool successStatus =
                _staticAnalyzerUtility.RunAnalyzerProcess(arguments, AnalyzersData.ExePath, ProcessWindowStyle.Hidden);
            return successStatus;
        }

        #endregion

    }
}
