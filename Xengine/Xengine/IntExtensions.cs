public static class IntExtensions
{
    public static bool IsWhite(this int piece) => piece <= 6;

    public static bool IsKing(this int piece) => piece == Piece.WhiteKing || piece == Piece.BlackKing;
}