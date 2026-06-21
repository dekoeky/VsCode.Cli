using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.Tests;

/// <summary>
/// <see cref="WinmergeCli"/> tests.
/// </summary>
[TestClass]
[SupportedOSPlatform("windows")]
[OSCondition(ConditionMode.Include, OperatingSystems.Windows, IgnoreMessage = "Not Supported On This OS")]
public class WinmergeCliTests
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

    //[TestCategory("Explicit")]
    //[TestMethod]
    //[LocalTestDataFile($"csv/people1.csv")]                                                 // Open 1 file
    //[LocalTestDataFile($"csv/people1.csv", $"csv/people2.csv")]                             // Open 2 files, same dir
    //[LocalTestDataFile($"csv/people1.csv", $"csv/people2.csv", "testfiles/csv/cars.csv")]   // Open 3 files, same dir
    //[LocalTestDataFile($"csv/people1.csv", $"c#/Greeting.cs")]                              // Open 2 files, different dir
    //[LocalTestDataFile($"csv")]                                                             // Open 1 directory
    //[LocalTestDataFile($"csv", $"c#")]                                                      // Open 2 directories
    //public void Open(params string[] paths)
    //{
    //    Console.WriteLine(string.Join(Environment.NewLine, paths));

    //    // Act
    //    WinmergeCli.Open(paths);
    //}

    //[TestMethod]
    //[LocalTestDataFile("csv/People1.csv", "csv/People2.csv")]
    //[LocalTestDataFile("c#/Greeting.cs", "c#/Goodbye.cs")]
    //public void Diff(string file1, string file2)
    //{
    //    // Act
    //    VsCodeCli.Diff(file1, file2);
    //}

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
}