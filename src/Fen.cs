namespace Utilities
{
    public class Fen
    {
        public static readonly Dictionary<char, int> PieceByChar = new()
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
        public static void Decode(string fen)
        {
            Coordinate pos = new(0, 0);
            int section = 0;

            foreach (char letter in fen)
            {
                if (section > 0) break;


                if (letter == '/') pos.Y++;

                else
                {
                    if (letter == ' ') section++;

                    else if (letter == '-') { }

                    else if (char.IsDigit(letter))
                    {
                        pos.X += (int)char.GetNumericValue(letter) - 1;
                    }

                    else
                    {
                        int piece = PieceByChar[letter];

                        SetCurrentPiece(piece);
                    }

                    pos.X++;

                    if (pos.X >= 8) pos.X = 0;
                }
            }


            void SetCurrentPiece(int piece)
                => Board.Pieces[pos.X, pos.Y] = piece;
        }

        public static string Encode()
        {
            string fen = "";
            int emptyPosNum = 0;

            // Per ogni riga => Y.
            for (int y = 0; y < 8; y++)
            {
                // Per ogni posizione in riga => X.
                for (int x = 0; x < 8; x++)
                {
                    // Se posizione vuota, aggiungi +1 a emptyPosNum.
                    if (Board.Pieces[x, y] == Piece.Empty)
                    {
                        emptyPosNum++;
                        if (emptyPosNum >= 8)
                        {
                            emptyPosNum = addAtEmptyPosNum(emptyPosNum);
                        }
                    }

                    else
                    {
                        // Se posizione occupata dopo successione di una o piu' posizioni vuote, aggiungi emptyPosNum a fen.
                        if (emptyPosNum > 0)
                        {
                            emptyPosNum = addAtEmptyPosNum(emptyPosNum);
                        }

                        // Aggiungi pezzo a fen.
                        int toSearch = Board.Pieces[x, y];
                        fen += PieceByChar.FirstOrDefault(x => x.Value == toSearch).Key;
                    }
                }
                emptyPosNum = 0;
                if (y < 7)
                    fen += "/";
            }


            int addAtEmptyPosNum(int n)
            {
                fen += n.ToString();
                n = 0;
                return n;
            }

            return fen;
        }
    }
}