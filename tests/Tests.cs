using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

[TestClass]
public class Tests
{
    [TestMethod]
    public void FenTest()
    {
        // Arrange
        string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

        // Act
        Fen.Decode(fen);
        string fenResult = Fen.Encode();

        // Assert
        Assert.AreEqual(fen, fenResult);
    }
}
