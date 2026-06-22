using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge;

[SupportedOSPlatform("windows")]
public static class WhereUtility
{
    private static readonly char[] NewLineChars = ['\r', '\n'];

    public static WindowsWhereResult Find(
        string pattern,
        string? recursiveDir = null,
        bool quote = false,
        bool includeTimestamp = false)
    {
        var args = new List<string>();

        if (!string.IsNullOrWhiteSpace(recursiveDir))
            args.Add($"/r \"{recursiveDir}\"");

        if (quote)
            args.Add("/f");

        if (includeTimestamp)
            args.Add("/t");

        args.Add(pattern);

        var psi = new ProcessStartInfo
        {
            FileName = "where.exe",
            Arguments = string.Join(" ", args),
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

            // Remove timestamp if /t was used (format: "<path> <size> <date> <time>")
            if (includeTimestamp)
            {
                var firstSpace = cleaned.IndexOf(' ');
                if (firstSpace > 0)
                    cleaned = cleaned[..firstSpace];
            }

            var cleanedStr = cleaned.ToString();

            if (File.Exists(cleanedStr) || Directory.Exists(cleanedStr))
                matches.Add(cleanedStr);
        }

        var exitCode = (ExitCode)proc.ExitCode;

        return new WindowsWhereResult
        {
            Success = exitCode == ExitCode.Successful,
            Matches = matches,
            Error = stderr.Length > 0 ? stderr : null,
            ExitCode = exitCode,
        };
    }
}
