namespace Dekoeky.AppBridge;

public sealed class WindowsWhereResult
{
    public bool Success { get; init; }
    public IReadOnlyList<string> Matches { get; init; } = [];
    public string? Error { get; init; }
    public ExitCode ExitCode { get; init; }
}
