public class FenDecoder
{
    public static readonly Dictionary<char, int> PieceChar = new()
    {
        ['R'] = Piece.WhiteRook,
        ['N'] = Piece.WhiteKnight,
        ['B'] = Piece.WhiteBishop,
        ['Q'] = Piece.WhiteQueen,
        ['K'] = Piece.WhiteKing,
        ['P'] = Piece.WhitePawn,
        ['r'] = Piece.BlackRook,
        ['n'] = Piece.BlackKnight,
        ['b'] = Piece.BlackBishop,
        ['q'] = Piece.BlackQueen,
        ['k'] = Piece.BlackKing,
        ['p'] = Piece.BlackPawn,
    };

    // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
    public static void DecodeFen(string fen)
    {
        (int X, int Y) pos = (0, 0);
        int section = 0;

        foreach (char letter in fen)
        {
            pos.X++;

            if (letter == ' ')
            {
                section++;
            }

            else if(letter == '/')
            {

            }

            else if (letter == '-')
            {

            }

            else if (char.IsDigit(letter))
            {

            }

            else
            {
                int piece = PieceChar[letter];
            }
        }
    }
}
