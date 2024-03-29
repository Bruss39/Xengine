﻿public static class IntExtensions
{
    public static bool IsWhite(this int piece) => piece <= 6 && piece != 0;


    public static bool IsBlack(this int piece) => piece > 6;


    public static bool IsKing(this int piece) => piece == Piece.WhiteKing || piece == Piece.BlackKing;


    public static bool IsEmpty(Coordinate pos) => Board.Pieces[pos.X, pos.Y] == Piece.Empty;
}