# AsynScriptAssist
A Visual Studio plugin for asyn-script

A Visual Studio plugin for Asyn-script was created to help debug and write asyn-script in Visual Studio. For the initial revision, a function was added so that we can navigate in asyn-script codes step by step in same the way asyn-script has been executed, which was achieved by loading a log file where the asyn-script debugger output the code location(file and line number) when the script was run in the virtual machine. This file is very helpful when you need to diagnose your program in a non-debug environment, since asyn-script debugger can be attached to the program at run time then produces the running records you need to analyze whether the code is being executed as intended.
