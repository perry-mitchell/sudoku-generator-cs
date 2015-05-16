using System;
using System.Threading;
using System.Collections.Generic;

namespace SudokuGenerator {

	class Program {

		public static void Main(string[] args) {
			/*int targetCount = 1;
			for (int current = 0; current < targetCount; current += 1) {
				BoardGenerator gen = new BoardGenerator();
				gen.generateSolutionBoard();
				Console.WriteLine(gen.getSolutionBoard().toString());
				gen.generatePuzzleBoard();
				Console.WriteLine("");
				Console.WriteLine(gen.getPuzzleBoard().toString(false));
			}*/

			if (args.Length < 3) {
				Console.WriteLine("Expected: <workers> <puzzles> <output>");
			}


			int numWorkers = Convert.ToInt32(args[0]);
			int numPuzzles = Convert.ToInt32(args[1]);
			string outputDir = args[2];

			//List<SudokuWorker> workers = new List<SudokuWorker>();
			List<Thread> threads = new List<Thread>();

			Messenger messenger = new Messenger(numPuzzles);
			messenger.setOutputDirectory(outputDir);

			for (int w = 0; w < numWorkers; w += 1) {
				SudokuWorker sw = new SudokuWorker(ref messenger);
				Thread swt = new Thread(new ThreadStart(sw.work));
				swt.Start();
				threads.Add(swt);
				Console.WriteLine("Thread started.");
				//workers.Add(sw);
			}

			foreach (Thread t in threads) {
				t.Join();
				Console.WriteLine("Thread terminated.");
			}
			Console.WriteLine("Done.");

		}

	}

}