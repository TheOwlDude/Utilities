using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;

namespace Cylance.Eos.Platform.Services.TemplateInstantiation.FrameworkIdentification
{
    public static class FrameworkIdentifier
    {
        internal static FrameworkType GetFrameworkType(bool printToConsole = false)
        {
            string NetCoreIdentifyingString = ".NETCoreApp";
        
            string NetFrameworkIdentifyingString = ".NETFramework";

            StackTrace currenStackTrace = new StackTrace();
            foreach (StackFrame stackFrame in currenStackTrace.GetFrames())
            {
                Assembly fromAssembly = stackFrame.GetMethod().DeclaringType.Assembly;
                
                TargetFrameworkAttribute frameworkAttribute = 
                    fromAssembly.GetCustomAttributes(typeof(TargetFrameworkAttribute)).FirstOrDefault() as TargetFrameworkAttribute;
                
                if (frameworkAttribute != null && frameworkAttribute.FrameworkName != null)
                {
                    if (printToConsole)
                    {
                        Console.WriteLine($"{fromAssembly.FullName} : {frameworkAttribute?.FrameworkName}");
                    }
                    if (frameworkAttribute.FrameworkName.Contains(NetCoreIdentifyingString))
                    {
                        return FrameworkType.NetCore;
                    }
                    else if (frameworkAttribute.FrameworkName.Contains(NetFrameworkIdentifyingString))
                    {
                        return FrameworkType.NetFramework;
                    }                                            
                }
            }

            return FrameworkType.Unknown;
        }
    }
}