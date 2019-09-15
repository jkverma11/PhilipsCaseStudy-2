using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using System.IO;
using System.IO.Compression;
using AnalyzersDataLib;
using FxCopAnalyzerLib;
using FxCopReaderLib;
using NDependAnalyzerLib;
using NDependReaderLib;
using ReportReaderContractsLib;
using StaticAnalyzerContractsLib;
using StaticAnalyzerUtilitiesLib;
using StaticToolsProcessorLib;
using TextWriterLib;
using WriterContractsLib;

namespace CaseStudy2.Controllers
{
    public class FileUploadController : ApiController
    {

        [HttpPost()]
        public string UploadFiles()
        {
            int iUploadedCnt = 0;
            string returnValue ="Failed To Generate The AnalyzerReport";

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string startingPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/ProjectFiles/");

            System.Web.HttpFileCollection httpFileCollection = System.Web.HttpContext.Current.Request.Files;
            
            if (!Directory.Exists(startingPath))
            {
                if (startingPath != null) Directory.CreateDirectory(startingPath);
            }
            for (int iCnt = 0; iCnt <= httpFileCollection.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = httpFileCollection[iCnt];
                string zipPath = startingPath + Path.GetFileName(hpf.FileName);

                if (hpf.ContentLength > 0)
                {
                    if (File.Exists(zipPath))
                    {
                        File.Delete(zipPath);
                    }

                    hpf.SaveAs(zipPath);
                    iUploadedCnt = iUploadedCnt + 1;
                }
                string extractPath = startingPath + Path.GetFileNameWithoutExtension(hpf.FileName);
                if (Directory.Exists(extractPath))
                {
                    Directory.Delete(extractPath,true);
                }
                ZipFile.ExtractToDirectory(zipPath, extractPath);
           }

            // RETURN A MESSAGE.
            System.Web.HttpPostedFile hpf1 = httpFileCollection[0];
            if (iUploadedCnt > 0)
            {
                string dataPath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + "App_Data" + "/");
                string userFilePath = startingPath + Path.GetFileNameWithoutExtension(hpf1.FileName);
                StaticAnalyzerProcessor(userFilePath,dataPath);

                returnValue= iUploadedCnt + " Files Uploaded Successfully and Report Is Generated At :" + dataPath+"AnalyzerOutput.txt";
            }
            return returnValue;
        }
        public bool ProcessOutput(string fxCopRulesFilePath,string fxCopExePath,string fxCopOutputFilePath)
        {
            string arguments = @"/p:" + fxCopRulesFilePath + @" /out:" + fxCopOutputFilePath;
            bool successStatus =
                StaticAnalyzerUtilities.RunAnalyzerProcess(arguments, fxCopExePath, ProcessWindowStyle.Hidden);
            return successStatus;
        }

        private bool StaticAnalyzerProcessor(string userFilePath,string dataPath)
        {
            string fxCopOutputFilePath = dataPath + "AnalyzerTools\\FxCopReport.xml";
            string fxCopExePath = dataPath + "AnalyzerTools\\Microsoft Fxcop 10.0\\FxCopCmd.exe";
            string fxCopRulesFilePath = dataPath + "AnalyzerTools\\Microsoft Fxcop 10.0\\common_fx_cop_file.FxCop";
            string analyzerOutputFile = dataPath + "AnalyzerOutput.txt";
            string nDependOutputFilePath = dataPath + "AnalyzerTools\\NDependOutput";
            string nDependExePath = dataPath + "AnalyzerTools\\NDepend_2019.2.7.9280\\NDepend.Console.exe";
            string nDependRulesFilePath = dataPath + "AnalyzerTools\\NDepend_2019.2.7.9280\\common.ndproj";


            List<AnalyzersDataModel> analyzersDataModelsList = new List<AnalyzersDataModel>
            {
                new AnalyzersDataModel
                {
                    Name = "FxCop",
                    ExePath = fxCopExePath,
                    OutputFilePath = fxCopOutputFilePath,
                    RuleFilePath = fxCopRulesFilePath
                },
                new AnalyzersDataModel {Name="NDepend",
                    ExePath = nDependExePath,
                    OutputFilePath = nDependOutputFilePath,
                    RuleFilePath = nDependRulesFilePath
                }
            };

            IStaticAnalyzers fxCopAnalyzer = new FxCopAnalyzer(userFilePath);
            IStaticAnalyzers nDependAnalyzer = new NDependAnalyzer(userFilePath);
            IReader fxCopReader = new FxCopReader();
            IReader nDependReader = new NDependReader();
            IReportWriter textWriter = new TextReportWriter(analyzerOutputFile);

            #region  Analyzers and Readers List

            List<IStaticAnalyzers> analyzers = new List<IStaticAnalyzers>();
            List<IReader> readers = new List<IReader>();
            analyzers.Add(fxCopAnalyzer);
            analyzers.Add(nDependAnalyzer);
            readers.Add(fxCopReader);
            readers.Add(nDependReader);

            #endregion

            #region Calling the processmanger

            var manager = new StaticToolsProcessor(analyzersDataModelsList, analyzers, readers, textWriter);
            bool successStatus=manager.Process();

            #endregion
            return successStatus;
        }
    }
}
