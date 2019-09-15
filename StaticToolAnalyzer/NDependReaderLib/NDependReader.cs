using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DataModelsLib;
using ReportReaderContractsLib;

namespace NDependReaderLib
{
    public class NDependReader : IReader
    {
        #region Fileds
        private readonly List<string> _metricNameList = new List<string>();
        private readonly List< string> _metricValueList = new List<string>();
        private readonly List<DataModel> _dataModels= new List<DataModel>();
        #endregion

        #region Properties

        public string Name { get; } = "NDepend";

        #endregion

        #region Public Methods

        public List<DataModel> Read(string analyzerOutputFilePath)
        {
            analyzerOutputFilePath = analyzerOutputFilePath + @"\TrendMetrics\NDependTrendData2019.xml";

            ReadMetrics(analyzerOutputFilePath);
            for (var i = 0; i < _metricValueList.Count; i++)
            {
                var dataModelTemp = new DataModel
                {
                    StaticAnalyzerTool = "NDepend",
                    ErrorMsg = _metricNameList[i],
                    ErrorCount = _metricValueList[i],
                    TimeStamp = DateTime.Now
                };
                _dataModels.Add(dataModelTemp);
            }

            return _dataModels;
        }

        public void ReadMetrics(string filePath)
        {
            try
            {
                var xmlDoc = XElement.Load(filePath);
                var messages = GetElements(xmlDoc, "Metric");
                foreach (var message in messages)
                {
                    _metricNameList.Add(AttributeValidator(message.Attribute("Name")));
                }
                var metricValueElements = GetElements(xmlDoc, "R").FirstOrDefault();
                var metricValues = AttributeValidator(metricValueElements?.Attribute("V"));
                var splitValues = SplitList(metricValues, '|');
                foreach (var value in splitValues)
                {
                    _metricValueList.Add(value);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        #endregion



        #region Private Heleper Methods

        private IEnumerable<XElement> GetElements(XElement xmlDoc, string elementName)
        {
            var getElements = from element in xmlDoc.Descendants()
                where element.Name == elementName
                select element;
            return getElements;
        }

        private IEnumerable<string> SplitList(string inputValue, char separator)
        {

            var splitValues = inputValue.Split(separator);
            return splitValues.ToList();
        }

        private static string AttributeValidator(XAttribute attribute)
        {
            var value = "";
            if (attribute != null)
            {
                value = attribute.Value;
            }
            return value;
        }

        #endregion

    }
}
