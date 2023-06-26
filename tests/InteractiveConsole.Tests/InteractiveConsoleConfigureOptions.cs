using System.Reflection;

namespace InteractiveConsole.Tests;

[TestClass]
public class InteractiveConsoleConfigureOptions
{
    [TestMethod]
    public void EnsureInteractiveConsoleOptionsConfigureOptionsSupportsAllProperties()
    {
        // NOTE: if this test fails, it is because a property was added to one of the following types.
        // When adding a new property to one of these types, ensure the corresponding
        // IConfigureOptions class is updated for the new property.
        var flags = BindingFlags.Public | BindingFlags.Instance;
        Assert.AreEqual(9, typeof(ConsoleInputOptions).GetProperties(flags).Length);
    }
}
