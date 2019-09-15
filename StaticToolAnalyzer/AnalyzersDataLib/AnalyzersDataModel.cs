using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzersDataLib
{
    public class AnalyzersDataModel
    {
        public string Name { get; set; }
        public string ExePath { get; set; }
        public string RuleFilePath { get; set; }
        public string OutputFilePath { get; set; }
    }
}
