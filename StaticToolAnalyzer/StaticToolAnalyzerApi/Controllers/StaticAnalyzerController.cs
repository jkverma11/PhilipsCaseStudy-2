using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography.X509Certificates;
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
using System.Diagnostics;
using LoggersContractLib;
using StaticAnalyzerUtilitiesContractsLib;
using ConsoleLoggerLib;

namespace StaticToolAnalyzerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticAnalyzerController : ControllerBase
    {
        private readonly string _dataPath = Directory.GetCurrentDirectory() + "\\App_Data\\";
        private readonly string _outputReportPath = Directory.GetCurrentDirectory()+ "\\App_Data\\AnalyzerOutput.csv";

       // GET api/values
        [HttpGet]
        public string Get()
        {
            if (System.IO.File.Exists(_outputReportPath))
            {
                //Process.Start(_outputReportPath);
                return _outputReportPath;
               
            }
            return "Run the HttpPost Method First";          

        }

        // POST api/values
        [HttpPost]
        public void ExtractZipFile([FromBody] string zipFilePath)
        {
            string extractedFilePath = zipFilePath.Substring(0, zipFilePath.IndexOf(".zip", StringComparison.Ordinal));

            if (System.IO.File.Exists(zipFilePath))
            {
                if (Directory.Exists(extractedFilePath))
                {
                    Directory.Delete(extractedFilePath, true);
                }

                ZipFile.ExtractToDirectory(zipFilePath, extractedFilePath);


                 StaticAnalyzerProcessor(extractedFilePath, _dataPath);
            }
        }


        private bool StaticAnalyzerProcessor(string extractedFilePath, string dataPath)
        {
            string fxCopOutputFilePath = dataPath + "AnalyzerTools\\FxCopReport.xml";
            string fxCopExePath = dataPath + "AnalyzerTools\\Microsoft_Fxcop_10.0\\FxCopCmd.exe";
            string fxCopRulesFilePath = dataPath + "AnalyzerTools\\Microsoft_Fxcop_10.0\\common_fx_cop_file.FxCop";
            string analyzerOutputFile = _outputReportPath;
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


            IStaticAnalyzers fxCopAnalyzer = new FxCopAnalyzer(extractedFilePath, new StaticAnalyzerUtilities(new ConsoleLogger()));

            IStaticAnalyzers nDependAnalyzer = new NDependAnalyzer(extractedFilePath, new StaticAnalyzerUtilities(new ConsoleLogger()));
            IReader fxCopReader = new FxCopReader(new ConsoleLogger());
            IReader nDependReader = new NDependReader(new ConsoleLogger());
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
            bool successStatus = manager.Process();

            #endregion
            return successStatus;
        }
    }
}
