
using System;

namespace DataModelsLib
{
    public class DataModel
    {
        #region Properties
        public DateTime TimeStamp { get; set; }
        public string StaticAnalyzerTool { get; set; }
        public string ErrorMsg { get; set; }
        public string ErrorType { get; set; }
        public string ErrorCertainty { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string LineNumber { get; set; }

        public string ErrorCount { get; set; }

       
        #endregion



    }
}
