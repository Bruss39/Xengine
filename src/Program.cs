﻿using Utilities;

// Set initial position.
Console.Write("Loading board... ");
Fen.Decode("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
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


// string rights = " w KQkq - 0 1";
