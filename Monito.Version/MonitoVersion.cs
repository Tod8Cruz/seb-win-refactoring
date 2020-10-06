using System.Diagnostics;
using System.Reflection;

namespace Monito.Version
{
    public static class MonitoVersion
    {
	    public static string GetMonitoVersion()
	    {
		    var assembly = Assembly.GetAssembly(typeof(MonitoVersion));
		    var version = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
		    return version;
	    }
    }
}
