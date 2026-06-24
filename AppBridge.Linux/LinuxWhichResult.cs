namespace Dekoeky.AppBridge;

public sealed class LinuxWhichResult
{
    public bool Success { get; init; }
    public IReadOnlyList<string> Matches { get; init; } = [];
    public string? Error { get; init; }
    public int ExitCode { get; init; }
}
