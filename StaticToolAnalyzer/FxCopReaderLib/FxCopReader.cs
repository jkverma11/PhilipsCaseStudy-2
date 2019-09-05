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
        private IStaticAnalyzerConfigurations _readConfiguration;
        List<DataModelsLib.DataModel> dataModels = new List<DataModelsLib.DataModel>();
        #endregion

        #region Constructor
        public FxCopReader(IStaticAnalyzerConfigurations ReadConfiguration)
        {
            this._readConfiguration = ReadConfiguration;
        }
        #endregion

        #region Method
        public List<DataModelsLib.DataModel> Read()
        {
            var xmlDoc = XElement.Load(_readConfiguration.GetAnalyzerOutputFilePath("FxCop"));
            var messages = from element in xmlDoc.Descendants()
                           where element.Name == "Issue"
                           select element;
            
            
            foreach (var msg in messages)
            {
                DataModel DataModelTemp = new DataModel();
                DataModelTemp.ErrorMsg=msg.Value;
                DataModelTemp.ErrorType = msg.Attribute("Name")?.ToString();
                DataModelTemp.ErrorCertainity=int.Parse(msg.Attribute("Certainty")?.ToString());
                DataModelTemp.FilePath = msg.Attribute("Path")?.ToString();
                DataModelTemp.FileName = msg.Attribute("File")?.ToString();
                DataModelTemp.LineNumber = int.Parse(msg.Attribute("Line")?.ToString());
                DataModelTemp.StaticAnalyzerTool = "FxCop";
                dataModels.Add(DataModelTemp);
            }

            return dataModels;
        }
        #endregion



    }
}
