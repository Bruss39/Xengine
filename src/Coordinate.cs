/// <summary>
/// Struttura usata per rappresentare la posizione di un quadrato sulla scacchiera, una direzione o un movimento.
/// </summary>
/// <remarks>
/// Le coordinate vanno dall'angolo in alto a sinistra a quello in basso a destra, perciò aggiungere alla y risulta in uno spostamento verso il basso.
/// </remarks>
public struct Coordinate
{
    public static string[] FileLetter = ["a", "b", "c", "d", "e", "f", "g", "h"];


    public static readonly Coordinate LeftDirection = new(-1, 0);

    public static readonly Coordinate RightDirection = new(1, 0);

    public static readonly Coordinate DownDirection = new(0, 1);

    public static readonly Coordinate UpDirection = new(0, -1);


    public static readonly Coordinate[] OrthogonalDirections = [LeftDirection, RightDirection, DownDirection, UpDirection];
    
    public static readonly Coordinate[] DiagonalDirections = [
        LeftDirection   + UpDirection, 
        RightDirection  + UpDirection, 
        LeftDirection   + DownDirection, 
        RightDirection  + DownDirection 
    ];

    public static readonly Coordinate[] AllDirections = [.. OrthogonalDirections, .. DiagonalDirections];


    public int X;
    public int Y;

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }


    public Coordinate Left(int length = 1) => new(X - length, Y);

    public Coordinate Right(int length = 1) => new(X + length, Y);

    public Coordinate Down(int length = 1) => new(X, Y + length);

    public Coordinate Up(int length = 1) => new(X, Y - length);


    public override string ToString() => $"{FileLetter[X]}{8 - Y}";

    public static Coordinate operator +(Coordinate left, Coordinate right) => new(left.X + right.X, left.Y + right.Y);

    public static Coordinate operator -(Coordinate left, Coordinate right) => new(left.X - right.X, left.Y - right.Y);
}
