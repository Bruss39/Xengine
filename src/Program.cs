using Utilities;

// Set initial position.
Console.Write("Loading board... ");
Fen.Decode("4K3/8/4N3/8/3qrq2/8/4p3/4K3 w - - 1 1");
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
