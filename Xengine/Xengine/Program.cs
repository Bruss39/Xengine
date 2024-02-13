// Set initial position.
Console.Write("Loading board... ");
FenDecoder.DecodeFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
Console.WriteLine("Done!");

Console.Write("Evaluating position... ");
int score = Evaluation.Evaluate();
Console.WriteLine("Done!");
Console.WriteLine(score);