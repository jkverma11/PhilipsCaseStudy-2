using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public static bool ChangeSolutionPath(string analyzerRuleFilePath, List<string> userCodePath,string node, string elementName, string attributeName)
        {
            bool successStatus = false;
            if (CreateDuplicateNodes(analyzerRuleFilePath, node, elementName, userCodePath.Count))
            {
                try
                {
                    var xmlDoc = XElement.Load(analyzerRuleFilePath);
                    var desiredElements = (from element in xmlDoc.DescendantsAndSelf()
                        where element.Name == elementName
                        select element);
                    int index = 0;
                    foreach (var desiredElement in desiredElements)
                    {
                        desiredElement.Attribute(attributeName)?.SetValue(userCodePath[index]);
                        index++;
                    }
                    xmlDoc.Save(analyzerRuleFilePath);
                    successStatus = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            return successStatus;
        }
        #endregion

        #region GetPaths
        public static List<string> GetPaths(string directoryPath, string extension)
        {
            List<string> filePathsList = new List<string>();
            string[] files =
                Directory.GetFiles(directoryPath, extension, SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (!file.Contains("obj") && !file.Contains("vshost") && !filePathsList.Contains(file))
                {
                    filePathsList.Add(file);
                }
            }

            return filePathsList;
        }
        #endregion

        #region Private Methods

        private static bool CreateDuplicateNodes(string xmlPath, string node, string element, int count)
        {
            bool isSuccess = false;
            try
            {
                var doc = XDocument.Load(xmlPath);
                var targetElements = doc.Root?.Element(node);
                if (targetElements != null)
                {
                    var firstNode = targetElements.Descendants().FirstOrDefault(x => x.Name.LocalName == element);
                    targetElements.RemoveAll();
                    while (count != 0)
                    {
                        targetElements.Add(firstNode);
                        count--;
                    }
                    doc.Save(xmlPath);
                    isSuccess = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return isSuccess;
        }
        #endregion
    }
}
