This file contains information about NDepend files when unzipped.
Don't unzip these files in '%ProgramFiles%\NDepend'.
This might provoke problems because of Windows protection.

Information about getting started with NDepend can be found here:
https://www.ndepend.com/docs/getting-started-with-ndepend



------------- NDepend.VisualStudioExtension.Installer.exe -----------

This file is a standalone UI program that installs the NDepend
extension for a range of Visual Studio versions.
When you install this VS extension, it introduces a dependency 
on the unzipped files. Therefore, extract (unzip) the files into 
permanent directory rather than a temp directory.
NDepend features in VS are accessed from the 'NDepend' top menu
and through a circle icon on the bottom right side of the VS status bar.



------------- VisualNDepend.exe -------------

This file provides all NDepend features in a standalone UI.
This is useful if you want to keep NDepend independent of VS
or if you want to quickly check something with NDepend without
starting up VS.



------------- NDepend.Console.exe -----------

This file starts a command line executable that is used to
run an analysis with NDepend and build a report. 
For help with this executable either type:
>NDepend.Console.exe /help
or refer to documentation at:
https://www.ndepend.com/docs/ndepend-console



------------- NDepend.PowerTools.exe -----------

This file is the Power Tools executable. Power Tools are
a set of small programs based on NDepend API.
Power Tools are open source and are used for demonstrating 
NDepend API syntax and capabilities.
Edit the Power Tools source code from the VS solution found in:
.\NDepend.PowerTools.SourceCode\NDepend.PowerTools.sln
Find documentation about NDepend API here: 
https://www.ndepend.com/api/webframe.html?NDepend.API_gettingstarted.html



.\Lib and .\Integration directories contain NDepend DLLs.
NDepend executables cannot run without these DLLs
unzipped into these directories.



NDepend doesn't rely on the registry. 
You can move NDepend files to another directory 
as long as you first uninstall the VS extension 
and re-install it after moving.