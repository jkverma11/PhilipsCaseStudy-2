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
        private const string filePath = "AnalyzerReport.txt";
        #endregion

        #region Properties
        public string FilePath { get; set; }
        #endregion

        #region Constructor
        public TextWriter()
        {
            FilePath = filePath;
        }
        #endregion

        #region Method
        public void Write(List<DataModel> dataModels)
        {
            List<string> text = new List<string>();

            StringBuilder strbuild = new StringBuilder();
            if (!File.Exists(FilePath))
               File.CreateText(FilePath);

            if (new FileInfo(FilePath).Length == 0)
            {
                StringBuilder tempStrBuild = new StringBuilder(); 
                if (dataModels.Count > 0) { 
                   Type type = dataModels[0].GetType();
                   System.Reflection.PropertyInfo[] props = type.GetProperties();
                   foreach (var pr in props)
                       tempStrBuild.Append(pr.Name +"\t");
                   
                   strbuild.AppendLine(tempStrBuild.ToString());
                    
                }

            }
            
            foreach (var properties in dataModels)
            {
                StringBuilder tempStrBuild = new StringBuilder();
                Type type = properties.GetType();
                System.Reflection.PropertyInfo[] props = type.GetProperties();
                foreach(var pr in props)
                {
                    tempStrBuild.Append(pr.GetValue(properties) + "\t");
                }
                strbuild.AppendLine(tempStrBuild.ToString());

            }
            File.AppendAllText(FilePath, strbuild.ToString());

        }
        #endregion
    }
}
