using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.Windows;

/// <summary>
/// Notepad utility class for opening directories and files.
/// </summary>
[SupportedOSPlatform("windows")]
public static class WindowsNotepad
{
    public static void OpenFile(string filePath) => Process.Start(new ProcessStartInfo
    {
        FileName = "notepad",
        Arguments = $"\"{filePath}\"",
        UseShellExecute = false
    });
}
