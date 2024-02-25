using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

[TestClass]
public class Tests
{
    [TestMethod]
    public void FenTest()
    {
        // Arrange
        string fen = "8/2K5/8/4k3/8/8/8/8";
        // string rights = " w KQkq - 0 1";

        // Act
        Fen.Decode(fen);
        string fenResult = Fen.Encode();

        // Assert
        Assert.AreEqual(fen, fenResult);
    }
}
