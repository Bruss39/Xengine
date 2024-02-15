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


    public static int Evaluate()
    {
        int score = 0;

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                int piece = Board.Pieces[x, y];

                if (piece == Piece.Empty) continue;
                if (piece.IsKing()) continue;


                if (piece.IsWhite())
                    // Se score positivo, e' in vantaggio il bianco.
                    score += PieceValues[piece];

                else if (piece.IsBlack())
                    // Se score negativo, e' in vantaggio il nero.
                    score -= PieceValues[piece];
            }
        }

        return score;
    }
}
