using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;
using WriterContractsLib;

namespace StaticToolsProcessorLib.Tests
{
    class FakeWriterStub : IReportWriter
    {
        public bool Write(List<DataModel> dataModelsList)
        {
            bool successStatus = dataModelsList != null;
            return successStatus;
        }
    }
}
