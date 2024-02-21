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

        switch (Pieces[pos.X, pos.Y])
        {
            case Piece.WhitePawn:
                // Se la posizione sopra e' libera, si puo' muovere.
                if (IntExtensions.IsEmpty(pos.Up()))
                {
                    moves.Add(new(pos, pos.Up()));

                    // Se 2 posizioni sopra e' libero, si puo' muovere.
                    if (pos.Y == 6 && IntExtensions.IsEmpty(new(pos.X, pos.Y - 2)))
                    {
                        moves.Add(new(pos, pos.Up(2)));
                    }
                }
                if (pos.X > 0 && Pieces[pos.X - 1, pos.Y - 1].IsBlack())
                {
                    moves.Add(new(pos, pos.Left().Up()));
                    if (Pieces[pos.X - 1, pos.Y - 1].IsKing())
                        KingEatenError(pos, pos.Left().Up());
                }

                if (pos.X < 7 && Pieces[pos.X + 1, pos.Y - 1].IsBlack())
                {
                    moves.Add(new(pos, pos.Right().Up()));
                    if (Pieces[pos.X + 1, pos.Y - 1].IsKing())
                        KingEatenError(pos, pos.Right().Up());
                }

                break;


            case Piece.WhiteRook:
            case Piece.BlackRook:
                List<Coordinate> movesToAddWhiteRook = new();

                foreach (Coordinate direction in Coordinate.OrthogonalDirections)
                    movesToAddWhiteRook.AddRange(MoveVerify(direction, pos));

                foreach (Coordinate elm in movesToAddWhiteRook)
                    moves.Add(new(pos, elm));

                break;


            case Piece.WhiteBishop:
            case Piece.BlackBishop:
                List<Coordinate> movesToAddWhiteBishop = new();

                foreach (Coordinate direction in Coordinate.DiagonalDirections)
                    movesToAddWhiteBishop.AddRange(MoveVerify(direction, pos));

                foreach (Coordinate elm in movesToAddWhiteBishop)
                    moves.Add(new(pos, elm));

                break;


            case Piece.WhiteQueen:
            case Piece.BlackQueen:
                List<Coordinate> movesToAddWhiteQueen = new();

                foreach (Coordinate direction in Coordinate.AllDirections)
                    movesToAddWhiteQueen.AddRange(MoveVerify(direction, pos));

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


    private static List<Coordinate> MoveVerify(Coordinate movement, Coordinate position)
    {
        List<Coordinate> movesToAdd = new();
        Coordinate pos = position;

        while (true)
        {
            position += movement;

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
