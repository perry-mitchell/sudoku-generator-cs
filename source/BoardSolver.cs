using System;
using System.Collections.Generic;

namespace SudokuGenerator {

	class BoardSolver {

		protected Board gameBoard;
		protected int solutions;
		protected List<Tuple<int, int>> emptyCells;

		public BoardSolver(Board partialBoard) {
			gameBoard = partialBoard;
		}

		public bool canBeSolved() {
			return (countSolutions(true) > 0);
		}

		public int countSolutions(bool stopAtOne = false) {
			solutions = 0;

			int index = 0;
			emptyCells = new List<Tuple<int, int>>();
			// find the empty cells
			for (int y = 0; y < 9; y += 1) {
				for (int x = 0; x < 9; x += 1) {
					int num = gameBoard.getNumber(x, y);
					if (num == 0) {
						Tuple<int, int> empty = new Tuple<int, int>(x, y);
						emptyCells.Add(empty);
					}
				}
			}

			if (emptyCells.Count <= 0) {
				return solutions;
			}

			tryPoint(index);

			return solutions;
		}

		private void tryPoint(int index) {
			Tuple<int, int> point = emptyCells[index];
			int x = point.Item1;
			int y = point.Item2;
			for (int num = 1; num <= 9; num += 1) {
				if (gameBoard.canBePlacedAtPosition(x, y, num)) {
					int subX = (int)Math.Floor((double)x / 3);
					int subY = (int)Math.Floor((double)y / 3);
					int relX = x - (subX * 3);
					int relY = y - (subY * 3);
					if (gameBoard.canPlaceAtSubGrid(subX, subY, relX, relY, num)) {
						//Console.WriteLine("OK placement!");
						gameBoard.setNumber(x, y, num);
						if (index == (emptyCells.Count - 1)) {
							// last cell
							solutions += 1;
							//Console.WriteLine("Solution! " + index);
							//Console.WriteLine(gameBoard.toString(false));
						} else {
							// go next
							tryPoint(index + 1);
						}
						gameBoard.setNumber(x, y, 0);
					}
				}
			}
			//Console.WriteLine("Leaving point " + x + "," + y);
			// reset
			gameBoard.setNumber(x, y, 0);
		}

	}

}