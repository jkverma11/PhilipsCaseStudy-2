using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticAnalyzerUtilitiesLib.Tests
{
    class FakeLogger : LoggersContractLib.ILogger
    {
        public void Write(object message)
        {
            Console.WriteLine(message);
        }

    }
}
