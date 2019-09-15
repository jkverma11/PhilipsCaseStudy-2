using AnalyzersDataLib;
namespace StaticAnalyzerContractsLib
{
    public interface IStaticAnalyzers
    {
        #region Public Methods
        bool ProcessInput();
        bool ProcessOutput();

        string AnalyzerName { get; }
        AnalyzersDataModel AnalyzersData { get; set; } 
        #endregion
    }
}
