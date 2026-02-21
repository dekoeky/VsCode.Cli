using Dekoeky.AppBridge;
using System.Runtime.Versioning;

namespace dekoeky;

[TestClass]
[SupportedOSPlatform("windows")]
[SupportedOSPlatform("linux")]
[OSCondition(ConditionMode.Include, OperatingSystems.Windows | OperatingSystems.Linux, IgnoreMessage = "Not Supported On This OS")]
public class VsCodeTests
{
    [TestMethod]
    public void IsInstalledInTypicalInstallationPath()
    {
        // Act
        var installed = VsCode.IsInstalledInTypicalInstallationPath(out var installPath);

        // Assert
        Console.WriteLine(installPath);
        Assert.IsTrue(installed);
        Assert.IsFalse(string.IsNullOrEmpty(installPath));
    }

    [TestCategory("Explicit")]
    [TestMethod]
    [DataRow("testfiles/csv/people1.csv")]                                                          // Open 1 file
    [DataRow("testfiles/csv/people1.csv", "testfiles/csv/people2.csv")]                             // Open 2 files, same dir
    [DataRow("testfiles/csv/people1.csv", "testfiles/csv/people2.csv", "testfiles/csv/cars.csv")]   // Open 3 files, same dir
    [DataRow("testfiles/csv/people1.csv", "testfiles/c#/Greeting.cs")]                              // Open 2 files, different dir
    [DataRow("testfiles/csv")]                                                                      // Open 1 directory
    [DataRow("testfiles/csv", "testfiles/c#")]                                                      // Open 2 directories
    public void Open(params string[] paths)
    {
        // Act
        VsCode.Open(WindowOptions.NewWindow, paths);
    }

    [TestMethod]
    [DataRow("testfiles/csv/People1.csv", "testfiles/csv/People2.csv")]
    [DataRow("testfiles/c#/Greeting.cs", "testfiles/c#/Goodbye.cs")]
    public void Diff(string file1, string file2)
    {
        // Act
        VsCode.Diff(file1, file2);
    }

    [TestMethod]
    public void Version()
    {
        // Act
        VsCode.Version(out var version, out var githubCommitId, out var architecture);

        Console.WriteLine($"Version:            {version}");
        Console.WriteLine($"Github Commit Id:   {githubCommitId}");
        Console.WriteLine($"Architecture:       {architecture}");
    }

    [TestMethod]
    public void Help()
    {
        // Act
        var output = VsCode.Help();

        // Assert
        Console.WriteLine(output);
        Assert.IsFalse(string.IsNullOrWhiteSpace(output));
    }
}