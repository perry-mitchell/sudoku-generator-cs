using System;
using System.Collections.Generic;
using System.IO;

namespace SudokuGenerator {

	class Messenger {

		protected int maxPuzzles;
		//protected Dictionary<string, string> puzzles;
		protected string outputDir;
		protected int puzzleCount;

		public Messenger(int max) {
			maxPuzzles = max;
			puzzleCount = 0;
			//puzzles = new Dictionary<string, string>();
		}

		public int addPuzzle(string solution, string puzzle) {
			/*int count;
			lock(puzzles) {
				puzzles.Add(solution, puzzle);
				count = puzzles.Count;
			}
			return count;*/
			lock(this) {
				puzzleCount += 1;
				File.WriteAllText(outputDir + "/" + solution + ".sudoku", solution + "\n" + puzzle);
			}
			return puzzleCount;
		}

		public int getMaxPuzzles() {
			return maxPuzzles;
		}

		public void setOutputDirectory(string dir) {
			outputDir = dir;
		}

	}

}