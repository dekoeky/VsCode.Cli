using System.Runtime.Versioning;

namespace Dekoeky.AppBridge;

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

    [TestMethod]
    [DataRow($"TestData/csv/people1.csv")]
    public void OpenFile(string path)
    {
        // Arrange
        EnsureTestFilesExist(path);

        // Act
        VsCodeCli.Open(VsCodeWindowOptions.NewWindow, path);
    }

    [TestMethod]
    [DataRow($"TestData/csv/people1.csv", $"TestData/csv/people2.csv")]
    [DataRow($"TestData/csv/people1.csv", $"TestData/c#/Greeting.cs")]
    public void OpenTwoFiles(string path1, string path2)
    {
        // Arrange
        EnsureTestFilesExist(path1, path2);

        // Act
        VsCodeCli.Open(VsCodeWindowOptions.NewWindow, path1, path2);
    }

    [TestMethod]
    [DataRow($"TestData/csv/people1.csv", $"TestData/csv/people2.csv", "TestData/csv/cars.csv")]
    public void OpenThreeFiles(string path1, string path2, string path3)
    {
        // Arrange
        EnsureTestFilesExist(path1, path2, path3);

        // Act
        VsCodeCli.Open(VsCodeWindowOptions.NewWindow, path1, path2, path3);
    }

    [TestMethod]
    [DataRow($"TestData/csv")]
    public void OpenOneDir(string path1)
    {
        // Arrange
        EnsureTestDirectoriesExist(path1);

        // Act
        VsCodeCli.Open(VsCodeWindowOptions.NewWindow, path1);
    }

    [TestMethod]
    [DataRow($"TestData/csv", "TestData/c#")]
    public void OpenTwoDirs(string path1, string path2)
    {
        // Arrange
        EnsureTestDirectoriesExist(path1, path2);

        // Act
        VsCodeCli.Open(VsCodeWindowOptions.NewWindow, path1, path2);
    }

    [TestMethod]
    [DataRow("TestData/csv/People1.csv", "TestData/csv/People2.csv")]
    [DataRow("TestData/c#/Greeting.cs", "TestData/c#/Goodbye.cs")]
    public void DiffFiles(string path1, string path2)
    {
        // Act
        VsCodeCli.Diff(path1, path2);
    }

    [TestMethod]
    [DataRow("TestData/csv", "TestData/c#")]
    public void DiffDirectories(string path1, string path2)
    {
        // Act
        VsCodeCli.Diff(path1, path2);
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

    private static void EnsureTestFilesExist(params string[] paths)
    {
        foreach (var path in paths)
            Assert.IsTrue(File.Exists(path), $"File does not exist: '{path}'");
    }

    private static void EnsureTestDirectoriesExist(params string[] paths)
    {
        foreach (var path in paths)
            Assert.IsTrue(Directory.Exists(path), $"Directory does not exist: '{path}'");
    }
}