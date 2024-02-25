public struct Piece
{
    public const int Empty = 0;

    public const int WhiteKing   = 1;
    public const int WhiteQueen  = 2;  // Ok.
    public const int WhiteBishop = 3;  // Ok.
    public const int WhiteKnight = 4;  // Ok.
    public const int WhiteRook   = 5;  // Ok.
    public const int WhitePawn   = 6;  // Ok.

    public const int BlackKing   = 7;
    public const int BlackQueen  = 8;  // Ok.
    public const int BlackBishop = 9;  // Ok.
    public const int BlackKnight = 10; // Ok.
    public const int BlackRook   = 11; // Ok.
    public const int BlackPawn   = 12;
}

public enum Color
{
    White,
    Black
}
