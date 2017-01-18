// Guids.cs
// MUST match guids.h
using System;

namespace Eureka.AsynScriptAssist
{
    static class GuidList
    {
        public const string guidAsynScriptAssistPkgString = "b2f400a6-5f2e-4d0e-96a4-79acb6c3db7a";
        public const string guidAsynScriptAssistCmdSetString = "6774790e-42ad-4f7a-b4e8-1c2b7707553e";

        public static readonly Guid guidAsynScriptAssistCmdSet = new Guid(guidAsynScriptAssistCmdSetString);
    };
}