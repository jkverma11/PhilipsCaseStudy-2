using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriterContractsLib
{
    public interface IReportWriter
    {
        #region Method

        bool Write(List<DataModelsLib.DataModel> dataModels);
        #endregion
    }
}
