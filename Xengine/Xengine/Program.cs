// Set initial position.
Console.Write("Loading board... ");
FenDecoder.DecodeFen("8/3p4/8/2kR1Q2/8/8/3b4/8 w - - 1 1");
Console.WriteLine("Done!");

Console.Write("Evaluating position... ");
int score = Evaluation.Evaluate();
Console.WriteLine("Done!");
Console.WriteLine(score);

Console.Write("Generating moves... ");
List<Move> moves = Board.GenerateAllMoves();
Console.WriteLine("Done!");
foreach (Move move in moves) Console.WriteLine(move);