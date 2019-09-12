using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelsLib;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

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
        
            //List<string> text = new List<string>();
            StringBuilder primaryStringBuilder = new StringBuilder();
            StringBuilder secondaryStringBuilder = new StringBuilder();
        
        public bool CheckFileExistence()
        {
            bool Status = false;
            try
            {
                if (!File.Exists(_path))
                {
                    File.CreateText(_path).Close();
                    Status = true;

                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return Status;
        }

        public bool WriteHeader(List<DataModel> dataModels)
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
            CheckFileExistence();
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
            if (count == dataModels.Count)
                status = true;

            return status;
            
        }
            
        #endregion
    }
}
