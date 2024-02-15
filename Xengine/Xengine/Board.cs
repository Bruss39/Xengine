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
                // Se la posizione sopra e' libera, si puo' muovere.
                if (IntExtensions.IsEmpty(new(pos.X, pos.Y - 1)))
                {
                    moves.Add(new(pos, new(pos.X, pos.Y - 1)));

                    // Se 2 posizioni sopra e' libero, si puo' muovere.
                    if (pos.Y == 6 && IntExtensions.IsEmpty(new(pos.X, pos.Y - 2)))
                    {
                        moves.Add(new(pos, new(pos.X, pos.Y - 2)));
                    }
                }

                if (pos.X > 0 && Pieces[pos.X - 1, pos.Y - 1].IsBlack())
                    moves.Add(new(pos, new(pos.X - 1, pos.Y - 1)));

                if (pos.X < 7 && Pieces[pos.X + 1, pos.Y - 1].IsBlack())
                    moves.Add(new(pos, new(pos.X + 1, pos.Y - 1)));

                break;


            case Piece.WhiteRook:

                List<Move> possibleMovesX = new();
                List<Move> possibleMovesY = new();

                foreach (int position in Enumerable.Range(0, 8))
                {
                    if (position == pos.X) continue;

                    // X rows.
                    if (IntExtensions.IsEmpty(new(position, pos.Y)))
                    {
                        Console.WriteLine($"{position} in {pos.Y}Y is empty.");
                        // if (position < pos.X)
                        //     foreach (int num in Enumerable.Range(position + 1, pos.X))
                        //         if (num > 0)
                        //             possibleMovesX.Add();
                    }

                    // Y columns.
                    if (IntExtensions.IsEmpty(new(pos.X, position)))
                    {
                        Console.WriteLine($"{position} in {pos.X}X is empty.");

                    }
                }

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
}
