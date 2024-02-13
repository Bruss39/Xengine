using System;
using System.Collections.Generic;

public class Board
{
    public static int[,] Pieces = new int[8, 8];

    public static bool IsOpponentTurn = false;


    public static List<Move> GenerateAllMoves()
    {
        List<Move> allMoves = new();

        foreach (Coordinate pos in NonEmptySquares())
        {
            allMoves.AddRange(GenerateMovesForPiece(pos));
        }

        return allMoves;
    }

    private static List<Move> GenerateMovesForPiece(Coordinate pos)
    {
        List<Move> moves = new();

        switch (Pieces[pos.X, pos.Y])
        {
            case Piece.WhitePawn:
                // Se e' libera la posizione sopra, si puo' muovere.
                if (IsEmpty(new(pos.X, pos.Y - 1)))
                {
                    moves.Add(new(pos, new(pos.X, pos.Y - 1)));

                    if (pos.Y == 6 && IsEmpty(new(pos.X, pos.Y - 2)))
                    {
                        moves.Add(new(pos, new(pos.X, pos.Y - 2)));
                    }
                }

                if (Pieces[pos.X - 1, pos.Y - 1].IsBlack())
                    moves.Add(new(pos, new(pos.X - 1, pos.Y - 1)));

                if (Pieces[pos.X + 1, pos.Y - 1].IsBlack())
                    moves.Add(new(pos, new(pos.X + 1, pos.Y - 1)));

                break;
        }

        return moves;
    }

    private static List<Coordinate> NonEmptySquares()
    {
        List<Coordinate> result = new();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (Pieces[x, y] != Piece.Empty) result.Add(new(x, y));
            }
        }

        return result;
    }

    private static bool IsEmpty(Coordinate pos) => Pieces[pos.X, pos.Y] == Piece.Empty;
}
