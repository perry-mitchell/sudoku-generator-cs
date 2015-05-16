using System;

namespace SudokuGenerator {

	class BoardGenerator {

		protected const int MAX_EMPTY_SEARCH_ATTEMPTS = 40;

		protected Board solutionBoard;
		protected Board puzzleBoard;

		protected static Random rnd = new Random();

		public BoardGenerator() {

		}

		public void generatePuzzleBoard(string generator = "mirror") {
			bool solvable = false;
			while (!solvable) {
				// STEP 1: generate holes
				Board testBoard = new Board();
				testBoard.copyBoard(solutionBoard);
				if (generator == "mirror") {
					MirrorBoardPoker mbp = new MirrorBoardPoker(ref testBoard);
					mbp.process();
				} else {
					Console.WriteLine("Bad generator: " + generator);
					System.Environment.Exit(1);
				}
				// STEP 2: test if solvable with 1 solution
				BoardSolver bs = new BoardSolver(testBoard);
				int numSolutions = bs.countSolutions();
				if (numSolutions == 1) {
					//Console.WriteLine("Found 1 solution for board!");
					solvable = true;
				}
				// STEP 3: check
				if (solvable) {
					puzzleBoard = testBoard;
				}
			}
		}

		public void generateSolutionBoard() {
			solutionBoard = new Board();
			while (!trySolutionGeneration()) {
				solutionBoard.whipeBoard();
			}
		}

		public Board getPuzzleBoard() {
			return puzzleBoard;
		}

		public Board getSolutionBoard() {
			return solutionBoard;
		}

		protected bool trySolutionGeneration() {
			//Random rnd = new Random();
			for (int num = 1; num <= 9; num += 1) {
				for (int xq = 0; xq < 3; xq += 1) {
					for (int yq = 0; yq < 3; yq += 1) {
						int tries = 0;
						bool foundEmpty = false;
						int absX = 0;
						int absY = 0;
						while (!foundEmpty) {
							int rx, ry;
							lock(rnd) {
								rx = rnd.Next(0, 3);
								ry = rnd.Next(0, 3);
							}
							absX = (xq * 3) + rx;
							absY = (yq * 3) + ry;
							if (solutionBoard.canPlaceAtSubGrid(xq, yq, rx, ry, num)) {
								if (solutionBoard.canBePlacedAtPosition(absX, absY, num)) {
									foundEmpty = true;
								}
							}
							tries += 1;
							if (!foundEmpty && (tries >= MAX_EMPTY_SEARCH_ATTEMPTS)) {
								return false;
							}
						}
						// set number
						solutionBoard.setNumber(absX, absY, num);
					}
				}
			}
			return true;
		}

	}

}