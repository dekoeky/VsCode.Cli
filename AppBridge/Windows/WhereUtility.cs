using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.Windows;

[SupportedOSPlatform("windows")]
public static class WhereUtility
{
    private static readonly char[] NewLineChars = ['\r', '\n'];

    public static WhereResult Find(
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

        var lines = stdout
            .Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);

        var matches = new List<string>();

        foreach (var line in lines)
        {
            // Remove quotes if /f was used
            var cleaned = line.Trim().Trim('"');

            // Remove timestamp if /t was used (format: "<path> <size> <date> <time>")
            if (includeTimestamp)
            {
                var firstSpace = cleaned.IndexOf(' ');
                if (firstSpace > 0)
                    cleaned = cleaned.Substring(0, firstSpace);
            }

            if (File.Exists(cleaned) || Directory.Exists(cleaned))
                matches.Add(cleaned);
        }

        var exitCode = (ExitCode)proc.ExitCode;

        return new WhereResult
        {
            Success = exitCode == ExitCode.AtLeastOneMatch,
            Matches = matches,
            Error = stderr.Length > 0 ? stderr : null,
            ExitCode = exitCode,
        };
    }
}
