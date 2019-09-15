using System.Collections.Generic;
using DataModelsLib;


namespace ReportReaderContractsLib
{
    public interface IReader
    {
        #region Method
        List<DataModel> Read(string analyzerOutputFilePath);
        #endregion

        #region Property
        string Name { get; }
        #endregion
    }
}
