// Set initial position.
Console.Write("Loading board... ");
FenDecoder.DecodeFen("8/5K2/2q5/3B4/2K5/5k2/8/8 w - - 0 1");
Console.WriteLine("Done!");

Console.Write("Evaluating position... ");
int score = Evaluation.Evaluate();
Console.WriteLine("Done!");
Console.WriteLine(score);

Console.Write("Generating moves... ");
List<Move> moves = Board.GenerateAllMoves();
Console.WriteLine("Done!");
foreach (Move move in moves) Console.WriteLine(move);