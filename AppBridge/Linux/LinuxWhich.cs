using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.Linux;

[SupportedOSPlatform("linux")]
internal static class LinuxWhich
{
    public static void Locate(string path)
    {
        Process.Start("which", path);
    }
}