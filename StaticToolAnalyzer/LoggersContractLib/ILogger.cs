﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggersContractLib
{
    public interface ILogger
    {
        void Write(object message);
    }
}
