using Utilities;

// Set initial position.
Console.Write("Loading board... ");
Fen.Decode("8/2K5/8/4k3/8/8/8/8 b - - 0 1");
Console.WriteLine("Done!");
Console.WriteLine(Fen.Encode());

Console.Write("Evaluating position... ");
int score = Evaluation.Evaluate();
Console.WriteLine("Done!");
Console.WriteLine(score);

Console.Write("Generating moves... ");
List<Move> moves = Board.GenerateAllMoves();
Console.WriteLine("Done!");
foreach (Move move in moves) Console.WriteLine(move);


// BUG IN FenTest!!
// 8/2K/8/4k/8/8/8/8   rather than   8/2K5/8/4k3/8/8/8/8 b - - 0 1
