using System;
using System.Collections.Generic;

public class Evaluation
{
    public static readonly Dictionary<int, int> PieceValues = new()
    {
        [Piece.WhiteRook]   = 5,
        [Piece.WhiteKnight] = 3,
        [Piece.WhiteBishop] = 3,
        [Piece.WhiteQueen]  = 9,
        [Piece.WhitePawn]   = 1,
        [Piece.BlackRook]   = 5,
        [Piece.BlackKnight] = 3,
        [Piece.BlackBishop] = 3,
        [Piece.BlackQueen]  = 9,
        [Piece.BlackPawn]   = 1
    };
}
