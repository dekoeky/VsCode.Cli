using System.Diagnostics;

namespace Dekoeky.AppBridge.Windows;

/// <summary>
/// Notepad++ utility class for opening directories and files.
/// </summary>
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