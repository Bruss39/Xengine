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
                // 33 <- example pos
                // X.
                while (true)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (pos.X + i <= 7)
                        {
                            Coordinate position = new(pos.X + i, pos.Y);
                            if (!IntExtensions.IsEmpty(position))
                                break;
                            else
                                moves.Add(new(pos, position));
                        }
                        else
                            break;
                    }
                    break;
                }

                // -Y.
                while (true)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (pos.Y + i <= 7)
                        {
                            Coordinate position = new(pos.X, pos.Y + i);
                            if (!IntExtensions.IsEmpty(position))
                                break;
                            else
                                moves.Add(new(pos, position));
                        }
                        else
                            break;
                    }
                    break;
                }

                // -X.
                while (true)
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        if (pos.X - i >= 0)
                        {
                            Coordinate position = new(pos.X - i, pos.Y);
                            if (!IntExtensions.IsEmpty(position))
                                break;
                            else
                                moves.Add(new(pos, position));
                        }
                        else
                            break;
                    }
                    break;
                }

                // Y.
                while (true)
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        if (pos.Y - i >= 0)
                        {
                            Coordinate position = new(pos.X, pos.Y - i);
                            if (!IntExtensions.IsEmpty(position))
                                break;
                            else
                                moves.Add(new(pos, position));
                        }
                        else
                            break;
                    }
                    break;
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
