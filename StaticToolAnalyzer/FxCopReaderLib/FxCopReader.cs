using System;
using System.Collections.Generic;
using System.Linq;
using DataModelsLib;
using System.Xml.Linq;
using LoggersContractLib;

namespace FxCopReaderLib
{
    public class FxCopReader:ReportReaderContractsLib.IReader
    {
        #region Fields

        private readonly List<DataModelsLib.DataModel> _dataModels = new List<DataModelsLib.DataModel>();
        private ILogger _loggerRef;
        private bool _validatorSuccessStatus=false;

        public string Name { get; } = "FxCop";
        
        #endregion

        #region Constructor
        public FxCopReader(ILogger LoggerRef)
        {
            this._loggerRef = LoggerRef;
        }
        #endregion

        #region Method
        public List<DataModelsLib.DataModel> Read(string analyzerOutputFilePath)
        {
            try
            {
                var xmlDoc = XElement.Load(analyzerOutputFilePath);
                var messages = GetAllElements(xmlDoc, "Issue");
                CreateDataModelList(messages);
                
            }
            catch(Exception exception)
            {
                _loggerRef.Write(exception);
            }
            return _dataModels;
        }
        #endregion

        #region Private Method
        private string Validator(XAttribute obj)
        {
            if(obj!=null)
            {
                return obj.ToString();

            }
            return string.Empty;

        }

        private void CreateDataModelList(IEnumerable<XElement> messages)
        {
            foreach (var message in messages)
            {
                var dataModelTemp = new DataModel
                {
                    ErrorMsg = message.Value,
                    ErrorType = Validator(message.Attribute("Name")),
                    ErrorCertainty = Validator(message.Attribute("Certainty")),
                    FilePath = Validator(message.Attribute("Path")),
                    FileName = Validator(message.Attribute("File")),
                    LineNumber = Validator(message.Attribute("Line")),
                    StaticAnalyzerTool = "FxCop",
                    TimeStamp = DateTime.Now
                };
                _dataModels.Add(dataModelTemp);
            }

        }

        private IEnumerable<XElement> GetAllElements(XElement XDoc,string elementName)
        {
            var messages=from element in XDoc.Descendants()
            where element.Name == elementName
            select element;
            return messages;
        }
        #endregion



    }
}
