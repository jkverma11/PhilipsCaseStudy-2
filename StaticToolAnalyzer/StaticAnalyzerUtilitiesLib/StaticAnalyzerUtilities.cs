﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StaticAnalyzerUtilitiesLib
{
    public static class StaticAnalyzerUtilities
    {
        #region Method RunAnalyzerProcess
        public static bool RunAnalyzerProcess(string arguments, string analyzerExePath, ProcessWindowStyle windowStyle)
        {
            bool successStatus = false;
            try
            {
                Process proc = new Process
                {
                    StartInfo = {WindowStyle = windowStyle, Arguments = arguments, FileName = analyzerExePath}
                };
                proc.Start();
                proc.WaitForExit();
                successStatus = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                successStatus = false;
            }

            return successStatus;
        }
        #endregion

        #region Method ChangeSolutionPath
        public static bool ChangeSolutionPath(string analyzerRuleFilePath, string userCodePath, string ElementName, string AttributeName)
        {
            bool successStatus = false;
            try
            {
                var xmlDoc = XElement.Load(analyzerRuleFilePath);
                var staticTools = (from element in xmlDoc.DescendantsAndSelf()
                    where element.Name == ElementName
                    select element).FirstOrDefault();
                staticTools?.Attribute(AttributeName)?.SetValue(userCodePath);
                xmlDoc.Save(analyzerRuleFilePath);
                successStatus = true;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                successStatus = false;

            }
            return successStatus;
        }
        #endregion
    }
}
