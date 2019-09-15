using System;
using System.Linq;
using System.Xml.Linq;
using StaticAnalyzerConfigurationsContractsLib;

namespace StaticAnalyzerXmlConfigurationsLib
{
    public class StaticAnalyzerXmlConfigurations:IStaticAnalyzerConfigurations
    {
        #region ConstantFields

        private readonly string _xmlFilePath;

        #endregion
        #region Properties

        


        #endregion

        #region Initializer

        public StaticAnalyzerXmlConfigurations(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }

        #endregion
        #region Public Methods
        public string GetAnalyzerExePath(string toolName) => XmlParser("Tool", toolName);

        public string GetUserCodeExePath() => XmlParser("Process", "ExePath");


        public string GetUserCodeSolutionPath() => XmlParser("Process", "SolutionPath");

        public string GetAnalyzerRulesFilePath(string toolName) => XmlParser(toolName, "RuleFilePath");

        public string GetAnalyzerOutputFilePath(string toolName) => XmlParser(toolName, "OutputFilePath");

        #endregion

        #region PrivateMethod

        private string XmlParser(string tag, string value)
        {
            string returnString = string.Empty;
            try
            {
                var xmlDoc = XElement.Load(_xmlFilePath);
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
