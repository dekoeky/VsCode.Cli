using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge;

[SupportedOSPlatform("linux")]
internal static class LinuxWhich
{
    private const string WhichPath = "which";

    public static LinuxWhichResult Locate(string path)
    {
        var psi = new ProcessStartInfo
        {
            FileName = WhichPath,
            //ArgumentList =
            //{

            //},
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var proc = Process.Start(psi)!;

        var stdout = proc.StandardOutput.ReadToEnd();
        var stderr = proc.StandardError.ReadToEnd();
        proc.WaitForExit();

        var matches = new List<string>();

        foreach (var line in stdout.EnumerateLines())
        {
            // Remove quotes if /f was used
            var cleaned = line.Trim().Trim('"');
            var cleanedStr = cleaned.ToString();

            if (File.Exists(cleanedStr) || Directory.Exists(cleanedStr))
                matches.Add(cleanedStr);
        }

        var exitCode = proc.ExitCode;

        return new LinuxWhichResult
        {
            Success = exitCode == 0,
            Matches = matches,
            Error = stderr.Length > 0 ? stderr : null,
            ExitCode = exitCode,
        };
    }
}
