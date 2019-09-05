using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelsLib
{
    public class DataModel
    {
        #region Properties
        public string StaticAnalyzerTool { get; set; }
        public string ErrorMsg { get; set; }
        public string ErrorType { get; set; }
        public int ErrorCertainity { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }

        public int ErrorCount { get; set; }
        #endregion



    }
}
