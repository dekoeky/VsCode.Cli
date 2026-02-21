using Dekoeky.AppBridge.TestData;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.VsCode.Tests;

/// <summary>
/// <see cref="VsCodeCli"/> tests.
/// </summary>
[TestClass]
[SupportedOSPlatform("windows")]
[SupportedOSPlatform("linux")]
[OSCondition(ConditionMode.Include, OperatingSystems.Windows | OperatingSystems.Linux, IgnoreMessage = "Not Supported On This OS")]
public class VsCodeCliTests
{
    [TestMethod]
    public void IsInstalledInTypicalInstallationPath()
    {
        // Act
        var installed = VsCodeCli.IsInstalledInTypicalInstallationPath(out var installPath);

        // Assert
        Console.WriteLine(installPath);
        Assert.IsTrue(installed);
        Assert.IsFalse(string.IsNullOrEmpty(installPath));
    }

    [TestCategory("Explicit")]
    [TestMethod]
    [LocalTestDataFile($"csv/people1.csv")]                                                 // Open 1 file
    [LocalTestDataFile($"csv/people1.csv", $"csv/people2.csv")]                             // Open 2 files, same dir
    [LocalTestDataFile($"csv/people1.csv", $"csv/people2.csv", "testfiles/csv/cars.csv")]   // Open 3 files, same dir
    [LocalTestDataFile($"csv/people1.csv", $"c#/Greeting.cs")]                              // Open 2 files, different dir
    [LocalTestDataFile($"csv")]                                                             // Open 1 directory
    [LocalTestDataFile($"csv", $"c#")]                                                      // Open 2 directories
    public void Open(params string[] paths)
    {
        Console.WriteLine(string.Join(Environment.NewLine, paths));
        // Act
        VsCodeCli.Open(WindowOptions.NewWindow, paths);
    }

    [TestMethod]
    [LocalTestDataFile("csv/People1.csv", "csv/People2.csv")]
    [LocalTestDataFile("c#/Greeting.cs", "c#/Goodbye.cs")]
    public void Diff(string file1, string file2)
    {
        // Act
        VsCodeCli.Diff(file1, file2);
    }

    [TestMethod]
    public void Version()
    {
        // Act
        VsCodeCli.Version(out var version, out var githubCommitId, out var architecture);

        Console.WriteLine($"Version:            {version}");
        Console.WriteLine($"Github Commit Id:   {githubCommitId}");
        Console.WriteLine($"Architecture:       {architecture}");
    }

    [TestMethod]
    public void Help()
    {
        // Act
        var output = VsCodeCli.Help();

        // Assert
        Console.WriteLine(output);
        Assert.IsFalse(string.IsNullOrWhiteSpace(output));
    }
}