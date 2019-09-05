using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;
using System.Xml.Linq;
using StaticAnalyzerConfigurationsContractsLib;


namespace FxCopReaderLib
{
    public class FxCopReader:ReportReaderContractsLib.IReader
    {
        #region Fields
        private readonly IStaticAnalyzerConfigurations _readConfiguration;
        private readonly List<DataModelsLib.DataModel> _dataModels = new List<DataModelsLib.DataModel>();
        #endregion

        #region Constructor
        public FxCopReader(IStaticAnalyzerConfigurations readConfiguration)
        {
            this._readConfiguration = readConfiguration;
        }
        #endregion

        #region Method
        public List<DataModelsLib.DataModel> Read()
        {
            try
            {
                var xmlDoc = XElement.Load(_readConfiguration.GetAnalyzerOutputFilePath("FxCop"));
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
                        StaticAnalyzerTool = "FxCop"
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
