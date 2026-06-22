using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.Tests;

/// <summary>
/// <see cref="WindowsExplorerCli"/> related tests.
/// </summary>
[TestClass]
[OSCondition(OperatingSystems.Windows)]
[SupportedOSPlatform("windows")]
public class WindowsExplorerCliTests
{
    [TestMethod]
    public void Open()
    {
        // Act
        WindowsExplorerCli.Open();
    }

    [TestMethod]
    [DataRow(@"C:\")]
    public void OpenDirectory(string directory)
    {
        // Act
        WindowsExplorerCli.OpenDirectory(directory);
    }
}
