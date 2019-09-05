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
        private const string Path = "AnalyzerReport.txt";
        #endregion

        #region Properties
        public string FilePath { get; set; }
        #endregion

        #region Constructor
        public TextWriter()
        {
            FilePath = Path;
        }
        #endregion

        #region Method
        public void Write(List<DataModel> dataModels)
        {
            List<string> text = new List<string>();
            StringBuilder primaryStringBuilder = new StringBuilder();
            StringBuilder secondaryStringBuilder = new StringBuilder();
            if (!File.Exists(FilePath))
            {
                File.CreateText(FilePath).Close();
                
            }

            if (new FileInfo(FilePath).Length == 0)
            {
                if (dataModels.Count > 0) { 
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
                foreach(var property in properties)
                {
                    secondaryStringBuilder.Append(property.GetValue(dataModel) + "\t");
                }
                primaryStringBuilder.AppendLine(secondaryStringBuilder.ToString());
                secondaryStringBuilder.Clear();
            }
            File.AppendAllText(FilePath, primaryStringBuilder.ToString());

        }
        #endregion
    }
}
