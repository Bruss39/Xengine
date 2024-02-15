public struct Coordinate
{
    public static string[] FileLetter = { "a", "b", "c", "d", "e", "f", "g", "h"};

    public int X;
    public int Y;

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }


    public override string ToString() => $"{FileLetter[X]}{8 - Y}";
}
