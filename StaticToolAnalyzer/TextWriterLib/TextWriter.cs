using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;
using System.IO;
using System.Reflection;

namespace TextWriterLib
{
    public class TextWriter : WriterContractsLib.IWriter
    {
        #region ConstantFields
        private readonly string _path = "AnalyzerReport.txt";
        #endregion
        
        #region Constructor
        public TextWriter(string outPutFilePath)
        {
            if (outPutFilePath != null)
            {
                _path = outPutFilePath;
            }
        }
        #endregion

        #region Method
        public bool Write(List<DataModel> dataModels)
        {
            bool successStatus = false;
            List<string> text = new List<string>();
            StringBuilder primaryStringBuilder = new StringBuilder();
            StringBuilder secondaryStringBuilder = new StringBuilder();
            try
            {
                if (!File.Exists(_path))
                {
                    File.CreateText(_path).Close();

                }

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
                    }
                }

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
                }
                File.AppendAllText(_path, primaryStringBuilder.ToString());
                successStatus = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return successStatus;
        }
        #endregion
    }
}
