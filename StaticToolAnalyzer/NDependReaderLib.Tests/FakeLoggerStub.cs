using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggersContractLib;

namespace NDependReaderLib.Tests
{
    class FakeLoggerStub : ILogger
    {
        public void Write(object message)
        {
            
        }
    }
}
