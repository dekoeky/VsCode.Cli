using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.Windows.Tests;

/// <summary>
/// <see cref="WhereUtility"/> related tests.
/// </summary>
[TestClass]
[OSCondition(OperatingSystems.Windows)]
[SupportedOSPlatform("windows")]
public class WhereUtilityTests
{
    [TestMethod]
    [DataRow("where.exe")]
    [DataRow("explorer.exe")]
    [DataRow("notepad.exe")]
    public void Find(string pattern)
    {
        // Act
        var result = WhereUtility.Find(pattern);
        Console.WriteLine(string.Join(Environment.NewLine, result.Matches));

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.Error);
        Assert.AreEqual(ExitCode.AtLeastOneMatch, result.ExitCode);
    }
}
