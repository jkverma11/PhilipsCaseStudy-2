using System;
using LoggersContractLib;

namespace FxCopReaderLib.Tests
{
    class FakeLoggerStub : ILogger
    {
        public void Write(object message)
        {

        }
    }
}

