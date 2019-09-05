using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StaticAnalyzerConfigurationsContractsLib;

namespace StaticAnalyzerXmlConfigurationsLib
{
    public class StaticAnalyzerXmlConfigurations:IStaticAnalyzerConfigurations
    {
        #region ConstantFields

        private const string File = "";

        #endregion
        #region Properties

        public string FileName { get; set; }


        #endregion

        #region Initializer

        public StaticAnalyzerXmlConfigurations()
        {
            FileName = File;
        }

        #endregion
        #region Public Methods
        public string GetAnalyzerExePath(string toolName) => XmlParser("Tool", toolName);

        public string GetUserCodeExePath() => XmlParser("Process", "ExePath");


        public string GetUserCodeSolutionPath() => XmlParser("Process", "SolutionPath");

        public string GetAnalyzerRulesFilePath(string toolName) => XmlParser("File", toolName);

        public string GetAnalyzerOutputFilePath(string toolName) => XmlParser("File", toolName);

        #endregion

        #region PrivateMethod

        private string XmlParser(string tag, string value)
        {
            string returnString = String.Empty;
            try
            {
                var xmlDoc = XElement.Load(FileName);
                var paths = from element in xmlDoc.Descendants()
                    where element.Name == tag
                    select element;
                foreach (var msg in paths)
                {
                    if (msg.Attribute("Name")?.Value == value)
                        returnString = msg.Attribute("Path")?.Value;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);                
            }
            return returnString;
        }

        #endregion
    }
}
