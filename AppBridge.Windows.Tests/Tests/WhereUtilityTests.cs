using System.Diagnostics;
using System.Runtime.Versioning;

namespace Dekoeky.AppBridge.Tests;

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

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNull(result.Error);
        Assert.AreEqual(ExitCode.Successful, result.ExitCode);
        foreach (var match in result.Matches) Debug.WriteLine(match);
    }
}
