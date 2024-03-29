﻿using System;
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
                        KingTakenError(pos, pos.Left().Up());
                }

                if (pos.X < 7 && Pieces[pos.X + 1, pos.Y - 1].IsBlack())
                {
                    moves.Add(new(pos, pos.Right().Up()));
                    if (Pieces[pos.X + 1, pos.Y - 1].IsKing())
                        KingTakenError(pos, pos.Right().Up());
                }

                break;


            case Piece.WhiteRook:
            case Piece.BlackRook:
                List<Coordinate> movesToAddRook = new();

                foreach (Coordinate direction in Coordinate.OrthogonalDirections)
                    movesToAddRook.AddRange(MovePropagate(direction, pos));

                foreach (Coordinate elm in movesToAddRook)
                    moves.Add(new(pos, elm));

                break;


            case Piece.WhiteBishop:
            case Piece.BlackBishop:
                List<Coordinate> movesToAddBishop = new();
            
                foreach (Coordinate direction in Coordinate.DiagonalDirections)
                    movesToAddBishop.AddRange(MovePropagate(direction, pos));
            
                foreach (Coordinate elm in movesToAddBishop)
                    moves.Add(new(pos, elm));
            
                break;
            
            
            case Piece.WhiteQueen:
            case Piece.BlackQueen:
                List<Coordinate> movesToAddQueen = new();
            
                foreach (Coordinate direction in Coordinate.AllDirections)
                    movesToAddQueen.AddRange(MovePropagate(direction, pos));
            
                foreach (Coordinate elm in movesToAddQueen)
                    moves.Add(new(pos, elm));
            
                break;
            
            
            case Piece.WhiteKnight:
            case Piece.BlackKnight:
                List<Coordinate> movesToAddKnight = new(ReturnPossibleSpecialMoves(pos));
            
                foreach (Coordinate elm in movesToAddKnight)
                    moves.Add(new(pos, elm));
            
                break;
            
            
            case Piece.WhiteKing:
            case Piece.BlackKing:
                List<Coordinate> movesToAddKing = new(ReturnPossibleSpecialMoves(pos));
            
                foreach (Coordinate elm in movesToAddKing)
                    moves.Add(new(pos, elm));
            
               break;
        }

        return moves;
    }


    private static List<Coordinate> ReturnPossibleSpecialMoves(Coordinate pos)
    {
        List<Coordinate> movesToAdd = new();
        Coordinate[] allMoves;

        if (Pieces[pos.X, pos.Y].IsKing())
            allMoves = ReturnAllKingMoves(pos);
        else
            allMoves = ReturnAllKnightMoves(pos);

        foreach (Coordinate target in allMoves)
        {
            if (OnTheChessBoard(target))
            {
                if (MoveVerify(pos, target))
                {
                    movesToAdd.Add(target);
                }
            }  
        }

        return movesToAdd;
    }


    private static Coordinate[] ReturnAllKnightMoves(Coordinate position)
    {
        Coordinate[] positions = [
            position.Left().Left().Up(),
            position.Left().Left().Down(),
            position.Down().Down().Left(),
            position.Down().Down().Right(),
            position.Right().Right().Up(),
            position.Right().Right().Down(),
            position.Up().Up().Left(),
            position.Up().Up().Right()
        ];
        return positions;
    }


    public static Coordinate[] ReturnAllKingMoves(Coordinate position)
    {
        Coordinate[] positions = [
            position.Left(),
            position.Right(),
            position.Up(),
            position.Down(),
            position.Right().Up(),
            position.Left().Up(),
            position.Right().Down(),
            position.Left().Down()
        ];
        return positions;
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


    private static bool OnTheChessBoard(Coordinate position)
    {
        if (position.X >= 0 && position.X <= 7 && position.Y >= 0 && position.Y <= 7)
            return true;
        else
            return false;
    }


    private static List<Coordinate> MovePropagate(Coordinate movement, Coordinate position)
    {
        List<Coordinate> movesToAdd = new();
        Coordinate pos = position;

        while (true)
        {
            position += movement;

            if (!OnTheChessBoard(position))
                return movesToAdd;


            if (Pieces[position.X, position.Y] != Piece.Empty)
                // Se e' dello stesso colore.
                if (!IsOpponentColor(pos, position))
                    // Ritorna la lista.
                    return movesToAdd;
                // Altrimenti agguiungi il target e poi ritorna la lista.
                else
                {
                    movesToAdd.Add(position);
                    return movesToAdd;
                }
            else { }
                movesToAdd.Add(position);
        }
    }


    private static bool MoveVerify(Coordinate pos, Coordinate target)
    {
        if (OnTheChessBoard(target))
        {
            // Se posizione e' vuota.
            if (IntExtensions.IsEmpty(target))
                return true;

            // Se posizione occupata e' opposta.
            else if (IsOpponentColor(pos, target))
            {
                if (Pieces[target.X, target.Y].IsKing())
                    KingTakenError(pos, new(target.X, target.Y));
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }


    private static void KingTakenError(Coordinate takerPosition, Coordinate kingPosition)
    {
        Console.WriteLine($"\nThe King cannot be taken by {takerPosition} in {kingPosition}!");
    }


    private static bool IsOpponentColor(Coordinate pos, Coordinate target)
    {
        if (Pieces[pos.X, pos.Y].IsWhite() && Pieces[target.X, target.Y].IsBlack() ||
                Pieces[pos.X, pos.Y].IsBlack() && Pieces[target.X, target.Y].IsWhite())
            return true;
        else
            return false;
    }
}
