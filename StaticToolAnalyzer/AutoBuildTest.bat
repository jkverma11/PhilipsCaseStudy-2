@echo off
set "BAT_PATH=%~dp0"

@echo off

echo %BAT_PATH%

echo Executing batch file

"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv" "%BAT_PATH%\StaticToolAnalyzer.sln" /build Debug  

pause

"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" /testcontainer:"%BAT_PATH%FxCopAnalyzerLib.Tests\bin\Debug\FxCopAnalyzerLib.Tests.dll" /test:Given_UserFilePath_When_ProcessInput_Invoked_Then_ExpectedTo_Return_False

"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" /testContainer:"%BAT_PATH%FxCopReaderLib.Tests\bin\Debug\FxCopReaderLib.Tests.dll" /test:Given_WrongXmlPath_Read_Method_Invoked__Expected_dataModelsList_Count_IsZero

"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" /testContainer:"%BAT_PATH%NDependAnalyzerLib.Tests\bin\Debug\NDependAnalyzerLib.Tests.dll" /test:Given_UserFilePath_When_ProcessInput_Invoked_Then_ExpectedTo_Return_False

"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" /testContainer:"%BAT_PATH%NDependReaderLib.Tests\bin\Debug\NDependReaderLib.Tests.dll" /test:Given_WrongXmlPath_Read_Method_Invoked__Expected_dataModelsList_Count_IsZero

"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" /testContainer:"%BAT_PATH%StaticAnalyzerUtilitiesLib.Tests\bin\Debug\StaticAnalyzerUtilitiesLib.Tests.dll" /test:Given_WrongAnalyzerExePath_When_RunAnalyzerProcess_Then_StatusIsFalse

"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" /testContainer:"%BAT_PATH%StaticToolsProcessorLib.Tests\bin\Debug\StaticToolsProcessorLib.Tests.dll" /test:Given_Correct_AnalyzerData_When_Process_Method_Invoked_Expected_Value_IsTrue

"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" /testContainer:"%BAT_PATH%TextWriterLib.Tests\bin\Debug\TextWriterLib.Tests.dll" /test:Given_OutputFilePath_WhenWriteIsInvokedAndFileIsCreated_ThenSuccessIsExpected
 

pause
 
set "SIM_PATH=D:\Softwares\Simian\Simian\bin\simian-2.5.10.exe"

"%SIM_PATH%" "%BAT_PATH%\**\*.cs"  

pause

start "" https://localhost:44328/api/StaticAnalyzer

pause