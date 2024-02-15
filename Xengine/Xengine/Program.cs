// Set initial position.
Console.Write("Loading board... ");
FenDecoder.DecodeFen("rnbqkbnr/1ppppppp/8/p7/1P6/8/P1PPPPPP/RNBQKBNR b KQkq - 0 2");
Console.WriteLine("Done!");

Console.Write("Evaluating position... ");
int score = Evaluation.Evaluate();
Console.WriteLine("Done!");
Console.WriteLine(score);

Console.Write("Generating moves... ");
List<Move> moves = Board.GenerateAllMoves();
Console.WriteLine("Done!");
foreach (Move move in moves) Console.WriteLine(move);