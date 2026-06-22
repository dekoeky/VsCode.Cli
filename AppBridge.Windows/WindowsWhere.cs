using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge;

[SupportedOSPlatform("windows")]
internal static class WindowsWhere
{
    public static void OpenDirectory(string path)
    {
        // C:\Windows\System32\where.exe
        Process.Start("where.exe", path);
    }

    // where.exe searches:
    // The current directory
    // All directories listed in your PATH environment variable
    // Optionally, any directory you specify with /r

    // Key switches include:
    // /r \<dir> — recursive search starting at a directory
    // /q — quiet mode, exit code only
    // /f — quote results
    // /t — show size and timestamp
}
