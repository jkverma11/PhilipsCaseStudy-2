using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;


namespace ReportReaderContractsLib
{
    public interface IReader
    {
        #region Method
        List<DataModel> Read();
        #endregion
    }
}
