using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriterContractsLib
{
    public interface IWriter
    {
        #region Method
        void Write(List<DataModelsLib.DataModel> dataModels);
        #endregion
    }
}
