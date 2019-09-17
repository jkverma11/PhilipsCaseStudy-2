using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLoggerLib
{
    public class ConsoleLogger:LoggersContractLib.ILogger
    {
        #region Method
        public void Write(object message)
        {
            Console.WriteLine(message);
        }
        #endregion
    }
}
