namespace Dekoeky.AppBridge.Windows;

public sealed class WhereResult
{
    public bool Success { get; init; }
    public IReadOnlyList<string> Matches { get; init; } = Array.Empty<string>();
    public string? Error { get; init; }
    public ExitCode ExitCode { get; init; }
}
