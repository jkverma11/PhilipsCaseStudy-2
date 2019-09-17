using System;
using System.Collections.Generic;
using System.Text;
using DataModelsLib;
using System.IO;
using LoggersContractLib;
using System.Reflection;

namespace TextWriterLib
{
    public class TextReportWriter : WriterContractsLib.IReportWriter
    {
        #region ConstantFields
        private readonly string _path = "AnalyzerReport.txt";
        private ILogger _loggerRef;
        #endregion
        
        #region Constructor
        public TextReportWriter(string outPutFilePath,ILogger LoggerRef)
        {
            if (outPutFilePath != null)
            {
                _path = outPutFilePath;
            }

            this._loggerRef = LoggerRef;
        }
        #endregion

        #region Method
        
            //List<string> text = new List<string>();
            StringBuilder primaryStringBuilder = new StringBuilder();
            StringBuilder secondaryStringBuilder = new StringBuilder();
        
        private bool CheckFileExistence()
        {
            bool Status = false;
            try
            {
                if (!File.Exists(_path))
                {
                    File.CreateText(_path).Close();
                    Status = true;

                }

                if (File.Exists(_path))
                    Status = true;

            }

            catch (Exception exception)
            {
                this._loggerRef.Write(exception);
            }

            return Status;
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
                    //foreach (var property in properties)
                    //{
                    //    secondaryStringBuilder.Append(property.Name + "\t");
                    //}
                    GetPropertyNames(properties);
                    primaryStringBuilder.AppendLine(secondaryStringBuilder.ToString());
                    secondaryStringBuilder.Clear();
                    status = true;
                }
            }
            return status;

        }

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
                    //foreach (var property in properties)
                    //{
                    //    secondaryStringBuilder.Append(property.GetValue(dataModel) + "\t");
                    //}
                    GetPropertyValues(properties, dataModel);
                    primaryStringBuilder.AppendLine(secondaryStringBuilder.ToString());
                    secondaryStringBuilder.Clear();
                    count++;
                }

                File.AppendAllText(_path, primaryStringBuilder.ToString());
                primaryStringBuilder.Clear();
                status = CheckStatus(count, dataModels);
            }
            return status;

        }

        #endregion

        #region PrivateMethods
        private void GetPropertyValues(PropertyInfo[] properties, DataModel dataModel)
        {
            foreach (var property in properties)
            {
                secondaryStringBuilder.Append(property.GetValue(dataModel) + "\t");
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
                secondaryStringBuilder.Append(property.Name + "\t");
            }

        }
        #endregion
    }
}
