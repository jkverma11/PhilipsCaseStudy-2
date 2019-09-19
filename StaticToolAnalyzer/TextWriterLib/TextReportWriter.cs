using System;
using System.Collections.Generic;
using System.Text;
using DataModelsLib;
using System.IO;
using System.Reflection;

namespace TextWriterLib
{
    public class TextReportWriter : WriterContractsLib.IReportWriter
    {
        #region ConstantFields
        private readonly string _path;
        #endregion
        
        #region Constructor
        public TextReportWriter(string outPutFilePath)
        {
            if (outPutFilePath != null)
            {
                _path = outPutFilePath;
            }
        }
        #endregion

        #region Method
        
            //List<string> text = new List<string>();
            readonly StringBuilder _primaryStringBuilder = new StringBuilder();
            readonly StringBuilder _secondaryStringBuilder = new StringBuilder();
      
        public bool Write(List<DataModel> dataModels)
        {
            int count = 0;
            bool status = false;
            if (CheckFileExistence())
            {
                WriteHeader(dataModels);
                foreach (var dataModel in dataModels)
                {
                    Type type = dataModel.GetType();
                    System.Reflection.PropertyInfo[] properties = type.GetProperties();
                    GetPropertyValues(properties, dataModel);
                    _primaryStringBuilder.AppendLine(_secondaryStringBuilder.ToString());
                    _secondaryStringBuilder.Clear();
                    count++;
                }

                File.AppendAllText(_path, _primaryStringBuilder.ToString());
                _primaryStringBuilder.Clear();
                status = CheckStatus(count, dataModels);
            }
            return status;
        }

        #endregion

        #region PrivateMethods

        private bool CheckFileExistence()
        {
            bool status = false;
                if (!File.Exists(_path))
                {
                    File.CreateText(_path).Close();
                    status = true;
                }
                if (File.Exists(_path))
                    status = true;
            return status;
        }

        private bool WriteHeader(List<DataModel> dataModels)
        {
            bool status = false;

            if (new FileInfo(_path).Length == 0)
            {
                if (dataModels.Count > 0)
                {
                    Type type = dataModels[0].GetType();
                    System.Reflection.PropertyInfo[] properties = type.GetProperties();
                    GetPropertyNames(properties);
                    _primaryStringBuilder.AppendLine(_secondaryStringBuilder.ToString());
                    _secondaryStringBuilder.Clear();
                    status = true;
                }
            }
            return status;
        }
        private void GetPropertyValues(PropertyInfo[] properties, DataModel dataModel)
        {
            foreach (var property in properties)
            {
                if (property.GetValue(dataModel) == null)
                {
                    _secondaryStringBuilder.Append("NA" + ",");
                }
                else
                {
                    var tempValue = property.GetValue(dataModel).ToString();
                    tempValue = tempValue.Replace(',', ';');
                    _secondaryStringBuilder.Append(tempValue + ",");
                }
            }
        }

        private bool CheckStatus(int count, List<DataModel> dataModels)
        {
            if (count == dataModels.Count)
                return true;
            return false;
        }

        private void GetPropertyNames(PropertyInfo[] properties)
        {
            foreach (var property in properties)
            {
                _secondaryStringBuilder.Append(property.Name + ",");
            }

        }
        #endregion
    }
}
