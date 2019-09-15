using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticAnalyzerContractsLib;
using AnalyzersDataLib;
using StaticAnalyzerUtilitiesLib;

namespace NDependAnalyzerLib
{
    public class NDependAnalyzer : IStaticAnalyzers
    {
        #region private Fields

        private readonly string _userFilePath;

        #endregion

        #region Properties
        public string AnalyzerName { get; } = "NDepend";

        public AnalyzersDataModel AnalyzersData { get; set; }

        #endregion

        #region Initializer

        public NDependAnalyzer(string userFilePath)
        {
            _userFilePath = userFilePath;
        }

        #endregion

        #region Public Methods

        public bool ProcessInput()
        {
            bool successStatus = false;
            List<string> assembliesList = StaticAnalyzerUtilities.GetPaths(_userFilePath, "*.sln");
            if (assembliesList.Count > 0)
            {
                successStatus = StaticAnalyzerUtilities.ChangeSolutionPath(AnalyzersData.RuleFilePath, assembliesList, "IDEFiles", "IDEFile", "FilePath");
            }
            return successStatus;
        }

        public bool ProcessOutput()
        {
            string arguments = AnalyzersData.RuleFilePath + @" /LogTrendMetrics /OutDir " + AnalyzersData.OutputFilePath;
            bool successStatus =
                StaticAnalyzerUtilities.RunAnalyzerProcess(arguments, AnalyzersData.ExePath, ProcessWindowStyle.Hidden);
            return successStatus;
        }

        #endregion

    }
}
