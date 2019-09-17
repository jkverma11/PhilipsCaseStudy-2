using LoggersContractLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace StaticAnalyzerUtilitiesLib
{
    public class StaticAnalyzerUtilities:StaticAnalyzerUtilitiesContractsLib.IStaticAnalyzerUtilities

    {
        #region Private fields
        private LoggersContractLib.ILogger _loggerRef;
        #endregion

        #region Properties
        
        #endregion

        #region Initializer
        public StaticAnalyzerUtilities(ILogger LoggerRef)
        {
            this._loggerRef = LoggerRef;
        }
        #endregion

        #region Method RunAnalyzerProcess
        public bool RunAnalyzerProcess(string arguments, string analyzerExePath, ProcessWindowStyle windowStyle)
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
                this._loggerRef.Write(e);
                successStatus = false;
            }

            return successStatus;
        }
        #endregion

        #region Method ChangeSolutionPath
        public  bool ChangeSolutionPath(string analyzerRuleFilePath, List<string> userCodePath,string node, string elementName, string attributeName)
        {
            bool successStatus = false;
            if (CreateDuplicateNodes(analyzerRuleFilePath, node, elementName, userCodePath.Count))
            {
                try
                {
                    var xmlDoc = XElement.Load(analyzerRuleFilePath);

                    var desiredElements = GetAllElements(xmlDoc, elementName);
                    int index = 0;
                    SetPathValues(desiredElements, userCodePath, index, attributeName);
                    xmlDoc.Save(analyzerRuleFilePath);
                    successStatus = true;
                }
                catch (Exception exception)
                {
                    this._loggerRef.Write(exception);
                }
            }
            return successStatus;
        }
        #endregion

        #region GetPaths
        public  List<string> GetPaths(string directoryPath, string extension)
        {
            List<string> filePathsList = new List<string>();
            string[] files =
                Directory.GetFiles(directoryPath, extension, SearchOption.AllDirectories);
            filePathsList = GetFilePathList(filePathsList, files);

            return filePathsList;
        }
        #endregion

        #region Private Methods

        public bool CreateDuplicateNodes(string xmlPath, string node, string element, int count)
        {
            bool isSuccess = false;
            try
            {
                var doc = XDocument.Load(xmlPath);
                var targetElements = doc.Root?.Element(node);
                var firstNode = GetFirstNode(targetElements, element);
                isSuccess = AddDuplicateNodes(targetElements,count,firstNode,xmlPath,doc);
            }
            catch (Exception e)
            {
                this._loggerRef.Write(e);
                
            }
            return isSuccess;
        }

        private IEnumerable<XElement> GetAllElements(XElement XDoc,string elementName)
        {
            var desiredElements = (from element in XDoc.DescendantsAndSelf()
                                   where element.Name == elementName
                                   select element);
            return desiredElements;

        }

        private void SetPathValues(IEnumerable<XElement> desiredElements, List<string> userCodePath, int index ,string attributeName )
        {
            foreach (var desiredElement in desiredElements)
            {
                desiredElement.Attribute(attributeName)?.SetValue(userCodePath[index++]);
                
            }
        }

        private XElement GetFirstNode(XElement targetElements, string element)
        {
            XElement FirstNode=null;
            if (targetElements != null)
            {
                FirstNode = targetElements.Descendants().FirstOrDefault(x => x.Name.LocalName == element);
                //targetElements.RemoveAll();
            }
            return FirstNode;
        }

        private bool AddDuplicateNodes(XElement targetElements,int count,XElement firstNode,string xmlPath,XDocument doc)
        {
            targetElements.RemoveAll();
            while (count != 0)
            {
                targetElements.Add(firstNode);
                count--;
            }
            doc.Save(xmlPath);
            return true;

        }

        private List<string> GetFilePathList(List<string> filePathsList,string [] files)
        {
            foreach (var file in files)
            {
               filePathsList= CheckFile(file, filePathsList);
            }
            return filePathsList;

        }

        private List<string> CheckFile (string file,List<string> filePathsList)
        {
            if (CheckFileExtensions(file) && !filePathsList.Contains(file))
            {
                filePathsList.Add(file);
            }
            return filePathsList;
        }

        private bool CheckFileExtensions(string file)
        {
            if (!file.Contains("obj") && !file.Contains("vshost"))
            {
                return true;
            }
            return false;

        }


        #endregion
    }
}
