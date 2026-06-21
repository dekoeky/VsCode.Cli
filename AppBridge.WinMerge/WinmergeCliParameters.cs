namespace Dekoeky.AppBridge;

internal static class WinmergeCliParameters
{
    public const string Help = "/?";

    /// <summary>
    /// Compares all files in all subfolders (recursive compare). Unique folders (occurring only on one side) are listed in the compare result as separate items. Note that including subfolders can increase compare time significantly. Without this parameter, WinMerge lists only files and subfolders at the top level of the two target folders. It does not compare the subfolders.
    /// </summary>
    public const string RecursiveCompare = "/r";

    /// <summary>
    /// Compares all files within the specified folders but excludes the files and subfolders within its subfolders. This allows for a shorter comparison time.
    /// </summary>
    public const string NonRecursiveCompare = "/r-";

    /// <summary>
    /// Enables you to close WinMerge with a single Esc key press. This is useful when you use WinMerge as an external compare application: you can close WinMerge quickly, like a dialog. Without this parameter, you might have to press Esc multiple times to close all its windows.
    /// </summary>
    public const string CloseWithSingleEsc = "/e";

    /// <summary>
    /// Applies a specified filter to restrict the comparison. The filter can be a filemask like *.h *.cpp, or the name of a file filter like XML/HTML Devel. Add quotation marks around a filter mask or name that contains spaces.
    /// </summary>
    public const string Filter = "/f";

    /// <summary>
    /// Runs WinMerge without displaying message boxes during comparison or report generation. The process terminates automatically when the operation is complete, making it suitable for batch or scripted execution.
    /// </summary>
    public const string NonInteractive = "/noninteractive";

    /// <summary>
    /// Sets the comparison result to the process exit code. 0: identical, 1: different, 2: error
    /// </summary>
    public const string EnableExitCode = "/enableexitcode";

    /// <summary>
    /// Limits WinMerge windows to a single instance. For example, if WinMerge is already running, a new compare opens in the same instance. Without this parameter, multiple windows are allowed: depending on other settings, a new compare might open in the existing window or in a new window.
    /// </summary>
    public const string SingleInstance = "/s";


}