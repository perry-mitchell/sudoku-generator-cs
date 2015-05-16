using System;

namespace SudokuGenerator {

	abstract class BoardPoker {

		protected Board puzzleBoard;

		public BoardPoker(ref Board pBoard) {
			puzzleBoard = pBoard;
		}

		public virtual void process() {
			
		}

	}

}