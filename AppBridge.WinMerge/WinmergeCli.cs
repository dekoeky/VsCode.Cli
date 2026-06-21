using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge;

/// <summary>
/// Winmerge Utility class that allows with interactions with Winmerge, when installed.
/// </summary>
/// <seealso href="https://manual.winmerge.org/en/Command_line.html"/>
[SupportedOSPlatform("windows")]
public static class WinmergeCli
{
    #region Installation Checking

    private static IEnumerable<string> TypicalPaths()
    {
        yield return $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\WinMerge\WinMergeU.exe";
        yield return $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\WinMerge\WinMergeU.exe";
        yield return $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles)}\WinMerge\WinMergeU.exe";
        yield return $@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86)}\WinMerge\WinMergeU.exe";

        // Default Installation Path when installed per user
        yield return $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Programs\WinMerge\WinMergeU.exe";
    }

    /// <summary>
    /// Find VsCode installation path from typical installation paths.
    /// </summary>
    /// <param name="installPath">The path at which the installation is found.</param>
    /// <returns>True if VsCode installation was found.</returns>
    internal static bool IsInstalledInTypicalInstallationPath(out string installPath)
    {
        foreach (var typicalPath in TypicalPaths())
        {
            if (!File.Exists(typicalPath)) continue;

            installPath = typicalPath;
            return true;
        }

        // VsCode installation not found in typical paths
        installPath = string.Empty;
        return false;
    }

    private static void ThrowIfNotInstalled()
    {
        if (!Installed)
            throw new NotInstalledException();
    }

    /// <summary>
    /// Exception thrown when VsCode is not installed on the machine.
    /// </summary>
    public class NotInstalledException() : Exception("VsCode was not installed");

    #endregion

    /// <summary>
    /// Whether VsCode is installed on this machine.
    /// </summary>
    public static readonly bool Installed;

    /// <summary>
    /// The path of the VsCode executable.
    /// </summary>
    public static readonly string InstallPath;

    static WinmergeCli()
    {
        Installed = IsInstalledInTypicalInstallationPath(out InstallPath);
    }

    /// <summary>
    /// Opens WinMerge.
    /// </summary>
    public static void Open()
    {
        // Validate VsCode Installation
        ThrowIfNotInstalled();

        Process.Start(InstallPath, string.Empty);
    }

    /// <summary>
    /// Retrieves the version of WinMerge.
    /// </summary>
    public static string Version()
    {
        var versionInfo = FileVersionInfo.GetVersionInfo(InstallPath);

        return versionInfo.ProductVersion
            ?? versionInfo.FileVersion
            ?? throw new InvalidOperationException("Could not retrieve version");
    }

    /// <summary>
    /// Launches the CLI Help window.
    /// </summary>
    public static void LaunchHelp(bool singleInstance = true)
    {
        List<string> arguments = [WinmergeCliParameters.Help];

        if (singleInstance)
            arguments.Add(WinmergeCliParameters.SingleInstance);

        var startInfo = new ProcessStartInfo
        {
            FileName = InstallPath,
            Arguments = string.Join(' ', arguments),
        };

        // Using -> Immediately release the unmanaged resources, without killing the process
        using var process = Process.Start(startInfo);
    }
}