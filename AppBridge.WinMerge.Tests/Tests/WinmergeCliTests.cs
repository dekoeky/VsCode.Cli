using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.Tests;

/// <summary>
/// <see cref="WinmergeCli"/> tests.
/// </summary>
[TestClass]
[SupportedOSPlatform("windows")]
[OSCondition(ConditionMode.Include, OperatingSystems.Windows, IgnoreMessage = "Not Supported On This OS")]
public partial class WinmergeCliTests
{
    [TestMethod]
    public void IsInstalledInTypicalInstallationPath()
    {
        // Act
        var installed = WinmergeCli.IsInstalledInTypicalInstallationPath(out var installPath);

        // Assert
        Assert.IsTrue(installed);
        Assert.IsFalse(string.IsNullOrEmpty(installPath));
        Debug.WriteLine(installPath);
    }

    [TestCategory("Explicit")]
    [TestMethod]
    public void Open()
    {
        // Act
        WinmergeCli.Open();
    }

    [TestMethod]
    [DataRow("TestData/csv/People1.csv", "TestData/csv/People2.csv", ExitCodes.Different)]
    [DataRow("TestData/c#/Greeting.cs", "TestData/c#/Goodbye.cs", ExitCodes.Different)]
    [DataRow("TestData/c#/Greeting.cs", "TestData/c#/GreetingIdentical.cs", ExitCodes.Identical)]
    public async Task CompareFileAsync(string file1, string file2, ExitCodes expected)
    {
        // Arrange
        Assert.That.FileExists(file1);
        Assert.That.FileExists(file2);

        // Act
        var result = await WinmergeCli.CompareAsync(file1, file2, TestContext.CancellationToken);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DataRow("TestData/csv", "TestData/csv", ExitCodes.Identical)]
    [DataRow("TestData/csv", "TestData/c#", ExitCodes.Different)]
    [DataRow("TestData/c#", "TestData/c#", ExitCodes.Identical)]
    public async Task CompareDirectoryAsync(string dir1, string dir2, ExitCodes expected)
    {
        // Arrange
        Assert.That.DirectoryExists(dir1);
        Assert.That.DirectoryExists(dir2);

        // Act
        var result = await WinmergeCli.CompareAsync(dir1, dir2, TestContext.CancellationToken);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Version()
    {
        // Act
        var version = WinmergeCli.Version();

        // Arrange
        Assert.IsNotNull(version);
        Debug.WriteLine($"Version: {version}");
    }

    [TestMethod]
    public void LaunchHelp()
    {
        // Act
        WinmergeCli.LaunchHelp();
    }

    public TestContext TestContext { get; set; }
}