using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        List<Tuple<int, int>> orthogonal = new() {new(-1, 0), new(1, 0), new(0, 1), new(0, -1)};
        List<Tuple<int, int>> diagonal = new() {new(-1, -1), new(1, -1), new(1, 1), new(-1, 1)};

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
                {
                    moves.Add(new(pos, new(pos.X - 1, pos.Y - 1)));
                    if (Pieces[pos.X - 1, pos.Y - 1].IsKing())
                        KingEatenError(pos, new(pos.X - 1, pos.Y - 1));
                }

                if (pos.X < 7 && Pieces[pos.X + 1, pos.Y - 1].IsBlack())
                {
                    moves.Add(new(pos, new(pos.X + 1, pos.Y - 1)));
                    if (Pieces[pos.X + 1, pos.Y - 1].IsKing())
                        KingEatenError(pos, new(pos.X + 1, pos.Y - 1));
                }

                break;


            case Piece.WhiteRook:
            case Piece.BlackRook:
                List<Coordinate> movesToAddWhiteRook = new();

                foreach (Tuple<int, int> tuple in orthogonal)
                    movesToAddWhiteRook.AddRange(MoveVerify(tuple.Item1, tuple.Item2, pos));

                foreach (Coordinate elm in movesToAddWhiteRook)
                    moves.Add(new(pos, elm));

                break;


            case Piece.WhiteBishop:
            case Piece.BlackBishop:
                List<Coordinate> movesToAddWhiteBishop = new();

                foreach (Tuple<int, int> tuple in diagonal)
                    movesToAddWhiteBishop.AddRange(MoveVerify(tuple.Item1, tuple.Item2, pos));

                foreach (Coordinate elm in movesToAddWhiteBishop)
                    moves.Add(new(pos, elm));

                break;


            case Piece.WhiteQueen:
            case Piece.BlackQueen:
                List<Coordinate> movesToAddWhiteQueen = new();

                foreach (Tuple<int, int> tuple in orthogonal)
                    movesToAddWhiteQueen.AddRange(MoveVerify(tuple.Item1, tuple.Item2, pos));

                foreach (Tuple<int, int> tuple in diagonal)
                    movesToAddWhiteQueen.AddRange(MoveVerify(tuple.Item1, tuple.Item2, pos));

                foreach (Coordinate elm in movesToAddWhiteQueen)
                    moves.Add(new(pos, elm));

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


    private static bool OffTheChessBoard(Coordinate position)
    {
        if (position.X >= 0 && position.X <= 7 && position.Y >= 0 && position.Y <= 7)
            return true;
        else
            return false;
    }


    private static List<Coordinate> MoveVerify(int addToX, int addToY, Coordinate position)
    {
        List<Coordinate> movesToAdd = new();
        Coordinate pos = position;

        while (true)
        {
            position.X += addToX;
            position.Y += addToY;

            if (OffTheChessBoard(position))
            {
                // Se posizione e' vuota.
                if (IntExtensions.IsEmpty(position))
                    movesToAdd.Add(position);

                // Se posizione occupata e' nera.
                else if (Pieces[pos.X, pos.Y].IsWhite() && Pieces[position.X, position.Y].IsBlack() || 
                    Pieces[pos.X, pos.Y].IsBlack() && Pieces[position.X, position.Y].IsWhite())
                {
                    if (Pieces[position.X, position.Y].IsKing())
                        KingEatenError(pos, new(position.X, position.Y));
                    movesToAdd.Add(position);
                    return movesToAdd;
                }
                else
                    return movesToAdd;
            }
            else
                return movesToAdd;
        }
    }


    private static void KingEatenError(Coordinate eaterPosition, Coordinate kingPosition)
    {
        Console.WriteLine($"\nThe King cannot be taken by {eaterPosition} in {kingPosition}!");
    }
}
