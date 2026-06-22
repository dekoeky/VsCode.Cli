namespace Dekoeky.AppBridge;

public sealed class WindowsWhereResult
{
    public bool Success { get; init; }
    public IReadOnlyList<string> Matches { get; init; } = Array.Empty<string>();
    public string? Error { get; init; }
    public ExitCode ExitCode { get; init; }
}
