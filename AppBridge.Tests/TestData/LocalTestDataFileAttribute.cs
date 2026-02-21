using System.Reflection;

namespace Dekoeky.AppBridge.TestData;

/// <summary>
/// Attribute to define in-line data for a test method.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class LocalTestDataFileAttribute : Attribute, ITestDataSource, ITestDataSourceIgnoreCapability
{
    public const string TestDataDirectory = "TestData";

    public LocalTestDataFileAttribute(params string?[]? relativePaths)
    {
        RelativePaths = relativePaths;
    }

    /// <summary>
    /// Gets data for calling test method.
    /// </summary>
    public string?[] RelativePaths { get; }

    /// <summary>
    /// Gets or sets display name in test results for customization.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets a reason to ignore the specific test case. Setting the property to non-null value will ignore the test case.
    /// </summary>
    public string? IgnoreMessage { get; set; }

    /// <inheritdoc />
    public IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        yield return RelativePaths.Select(path => path is null
            ? null
            : (object?)Path.GetFullPath(Path.Combine(TestDataDirectory, path))
        ).ToArray();
    }

    /// <inheritdoc />
    public virtual string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        //TODO: normalize slashes
        return string.Join('\r', data.Select(d => Path.GetFullPath(d as string)));
    }
}