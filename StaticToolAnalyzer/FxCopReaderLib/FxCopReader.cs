using System;
using System.Collections.Generic;
using System.Linq;
using DataModelsLib;
using System.Xml.Linq;


namespace FxCopReaderLib
{
    public class FxCopReader:ReportReaderContractsLib.IReader
    {
        #region Fields

        private readonly List<DataModelsLib.DataModel> _dataModels = new List<DataModelsLib.DataModel>();

        public string Name { get; } = "FxCop";
        
        #endregion

        #region Constructor

        #endregion

        #region Method
        public List<DataModelsLib.DataModel> Read(string analyzerOutputFilePath)
        {
            try
            {
                var xmlDoc = XElement.Load(analyzerOutputFilePath);
                var messages = from element in xmlDoc.Descendants()
                    where element.Name == "Issue"
                    select element;
                foreach (var message in messages)
                {
                    var dataModelTemp = new DataModel
                    {
                        ErrorMsg = message.Value,
                        ErrorType = message.Attribute("Name")?.ToString(),
                        ErrorCertainty = message.Attribute("Certainty")?.ToString(),
                        FilePath = message.Attribute("Path")?.ToString(),
                        FileName = message.Attribute("File")?.ToString(),
                        LineNumber = message.Attribute("Line")?.ToString(),
                        StaticAnalyzerTool = "FxCop",
                        TimeStamp = DateTime.Now
                    };
                    _dataModels.Add(dataModelTemp);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }
            return _dataModels;
        }
        #endregion



    }
}
