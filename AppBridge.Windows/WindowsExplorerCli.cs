using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge;

[SupportedOSPlatform("windows")]
internal static class WindowsExplorerCli
{
    private const string ShortExePath = "explorer.exe";
    private const string DefaultLongExePath = "C:\\Windows\\explorer.exe";

    /// <summary>
    /// Opens a new Windows Explorer window.
    /// </summary>
    public static void Open() => Process.Start(ShortExePath);

    public static void OpenDirectory(string path)
    {
        Process.Start(ShortExePath, path);
    }

    // TODO: Shell paths
    // shell:Downloads
}
