﻿using System;
using System.Collections.Generic;
using System.Text;
using DataModelsLib;
using System.IO;

namespace TextWriterLib
{
    public class TextReportWriter : WriterContractsLib.IReportWriter
    {
        #region ConstantFields
        private readonly string _path = "AnalyzerReport.txt";
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
                Console.WriteLine(exception);
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
                    foreach (var property in properties)
                    {
                        secondaryStringBuilder.Append(property.Name + "\t");
                    }
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
                    foreach (var property in properties)
                    {
                        secondaryStringBuilder.Append(property.GetValue(dataModel) + "\t");
                    }
                    primaryStringBuilder.AppendLine(secondaryStringBuilder.ToString());
                    secondaryStringBuilder.Clear();
                    count++;
                }

                File.AppendAllText(_path, primaryStringBuilder.ToString());
                primaryStringBuilder.Clear();
                if (count == dataModels.Count)
                    status = true;
                
            }
            return status;

        }
            
        #endregion
    }
}
