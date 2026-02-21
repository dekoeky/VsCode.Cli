using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.VsCode;

/// <summary>
/// VsCode Utility class that allows with interactions with (possible) VsCode installation.
/// </summary>
/// <seealso href="https://code.visualstudio.com/docs/configure/command-line"/>
[SupportedOSPlatform("windows")]
[SupportedOSPlatform("linux")]
public static class VsCodeCli
{
    #region Installation Checking

    [SupportedOSPlatform("windows")]
    private static IEnumerable<string> TypicalPathsWindows()
    {
        // Default Installation Path when VsCode is installed per user
        yield return $@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Programs\Microsoft VS Code\Code.exe";

        // Default Installation Path when VsCode is installed system-wide
        yield return $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\Microsoft VS Code\Code.exe";
    }

    [SupportedOSPlatform("linux")]
    private static IEnumerable<string> TypicalPathsLinux()
    {
        // When installed via apt
        yield return "/usr/bin/code";           // Executable symlink

        // When installed via Snap
        yield return "/snap/bin/code";          // Executable     
    }

    private static IEnumerable<string> TypicalPaths()
        => OperatingSystem.IsWindows() ? TypicalPathsWindows()
        : OperatingSystem.IsLinux() ? TypicalPathsLinux()
        : [];

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

    /// <summary>
    /// The path of the VsCode shell.
    /// </summary>
    private static readonly string ShellPath;


    static VsCodeCli()
    {
        Installed = IsInstalledInTypicalInstallationPath(out InstallPath);

        ShellPath = OperatingSystem.IsWindows()
            ? @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Programs\Microsoft VS Code\bin\code.cmd"
            : InstallPath;
    }

    /// <summary>
    /// Opens one or more files and/or folders in VsCode.
    /// </summary>
    /// <param name="paths">The paths of the files and/or folders to open.</param>
    public static void Open(params string[] paths)
    {
        // Validate Arguments
        ArgumentNullException.ThrowIfNull(paths);
        if (paths.Length == 0) throw new ArgumentException("At least one path must be provided", nameof(paths));

        // Validate VsCode Installation
        ThrowIfNotInstalled();

        // Put each path in quotes to handle any spaces present
        var quotedPaths = paths.Select(p => $"\"{p}\"");

        // Build the cli arguments
        // The paths are separated by spaces
        // Usage: code.exe [options] [paths...]
        var arguments = string.Join(' ', quotedPaths);

        Process.Start(InstallPath, arguments);
    }

    /// <summary>
    /// Opens one or more files and/or folders in VsCode.
    /// </summary>
    /// <param name="windowOptions"></param>
    /// <param name="paths">The paths of the files and/or folders to open.</param>
    public static void Open(WindowOptions windowOptions, params string[] paths)
    {
        // Validate Arguments
        ArgumentNullException.ThrowIfNull(paths);
        if (paths.Length == 0) throw new ArgumentException("At least one path must be provided", nameof(paths));

        // Validate VsCode Installation
        ThrowIfNotInstalled();

        // Put each path in quotes to handle any spaces present
        var quotedPaths = paths.Select(p => $"\"{p}\"");

        // Build the cli arguments
        // The paths are separated by spaces
        // Usage: code.exe [options] [paths...]
        var arguments = string.Join(' ', quotedPaths);

        var options = windowOptions switch
        {
            WindowOptions.NewWindow => Parameters.NewWindow,
            WindowOptions.ReUseWindow => Parameters.ReUseWindow,
        };

        var total = options + arguments;

        Process.Start(InstallPath, total);
    }

    public static void OpenFileInWorkSpace(string filePath, string? workSpace = null)
    {
        ThrowIfNotInstalled();

        workSpace ??= Path.GetDirectoryName(filePath);

        Process.Start(InstallPath, $"\"{workSpace}\" -g \"{filePath}\"");
    }

    /// <summary>
    /// -d or --diff &lt;file1&gt; &lt;file2&gt;
    /// Open a file difference editor.
    /// Requires two file paths as arguments.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="file1"></param>
    /// <param name="file2"></param>
    public static void Diff(string file1, string file2)
    {
        //-d or --diff <file1> <file2>	Open a file difference editor. Requires two file paths as arguments.

        var arguments = $"--diff \"{file1}\" \"{file2}\"";

        Process.Start(InstallPath, arguments);
    }

    public static void Version(out string version, out string githubCommitId, out string architecture)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = ShellPath,
            Arguments = Parameters.Version,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        if (Process.Start(startInfo) is not { } process)
            throw new InvalidOperationException();

        var versionOutput = process.StandardOutput.ReadToEnd();
        var lines = versionOutput.Split('\r', '\n');

        version = lines[0];
        githubCommitId = lines[1];
        architecture = lines[2];
    }

    /// <summary>
    /// -h or --help
    /// Print usage.
    /// </summary>
    public static string Help()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = ShellPath,
            Arguments = Parameters.Help,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        var proc = Process.Start(startInfo);
        return proc.StandardOutput.ReadToEnd();
    }
}