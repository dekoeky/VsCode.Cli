using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge;

/// <summary>
/// <see cref="LinuxWhich"/> related tests.
/// </summary>
[TestClass]
[OSCondition(OperatingSystems.Linux)]
[SupportedOSPlatform("linux")]
public class LinuxWhichTests
{
    [TestMethod]
    [DataRow("which")]
    [DataRow("ls")]
    [DataRow("cat")]
    public void Locate(string pattern)
    {
        // Arrange
        var expectedExitCode = 0;

        // Act
        var result = LinuxWhich.Locate(pattern);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.Error);
        Assert.AreEqual(expectedExitCode, result.ExitCode);
        foreach (var match in result.Matches) Debug.WriteLine(match);
    }
}
