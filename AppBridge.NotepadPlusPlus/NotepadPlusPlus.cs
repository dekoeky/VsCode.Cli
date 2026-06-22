using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge;

/// <summary>
/// Notepad++ utility class for opening directories and files.
/// </summary>
[SupportedOSPlatform("windows")]
public static class NotepadPlusPlus
{
    private static readonly string InstallPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\Notepad++\notepad++.exe";

    public static void OpenFile(string filePath) => Process.Start(new ProcessStartInfo
    {
        FileName = InstallPath,
        Arguments = $"\"{filePath}\"",
        UseShellExecute = false
    });
}