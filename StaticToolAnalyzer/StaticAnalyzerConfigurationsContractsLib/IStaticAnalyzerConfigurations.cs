using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzerConfigurationsContractsLib
{
    

    public interface IStaticAnalyzerConfigurations
    {
        #region public Methods
        string GetAnalyzerExePath(string toolName);
        string GetUserCodeExePath();
        string GetUserCodeSolutionPath();
        string GetAnalyzerRulesFilePath(string toolName);
        string GetAnalyzerOutputFilePath(string toolName);
        #endregion
    }




}
