using System;

namespace SudokuGenerator {

	class Board {

		protected BoardBox[,] board;

		public Board() {
			board = new BoardBox[3, 3];
			whipeBoard();
		}

		public bool canBePlacedAtPosition(int x, int y, int num) {
			int current = getNumber(x, y);
			//Console.WriteLine("Number at " + x + "," + y + " => " + current + " ... " + num);
			if (current > 0) {
				return false;
			}
			// row
			for (int tx = 0; tx < 9; tx += 1) {
				if (tx == x) {
					continue;
				}
				int thisNum = getNumber(tx, y);
				if (thisNum == num) {
					return false;
				}
			}
			// col
			for (int ty = 0; ty < 9; ty += 1) {
				if (ty == y) {
					continue;
				}
				int thisNum = getNumber(x, ty);
				if (thisNum == num) {
					return false;
				}
			}
			return true;
		}

		public bool canPlaceAtSubGrid(int subX, int subY, int relX, int relY, int num) {
			BoardBox subGrid = board[subX, subY];
			int checkNum = subGrid.getNumber(relX, relY);
			if (checkNum == 0) {
				bool numExists = false;
				for (int x = 0; x < 3; x += 1) {
					for (int y = 0; y < 3; y += 1) {
						if ((x == relX) && (y == relY)) {
							continue;
						}
						checkNum = subGrid.getNumber(x, y);
						if (checkNum == num) {
							numExists = true;
							break;
						}
					}
					if (numExists) {
						break;
					}
				}
				if (!numExists) {
					// winner!
					return true;
				}
			}
			return false;
		}

		public void copyBoard(Board master) {
			for (int y = 0; y < 9; y += 1) {
				for (int x = 0; x < 9; x += 1) {
					setNumber(x, y, master.getNumber(x, y));
				}
			}
		}

		public static Board fromString(string boardString) {
			Board board = new Board();
			int chr = 0;
			for (int y = 0; y < 9; y += 1) {
				for (int x = 0; x < 9; x += 1) {
					int num = Convert.ToInt32("" + boardString[chr]);
					chr += 1;
					board.setNumber(x, y, num);
				}
			}
			return board;
		}

		public int getNumber(int x, int y) {
			int row = (int)Math.Floor((double)y / 3);
			int col = (int)Math.Floor((double)x / 3);
			BoardBox bb = board[col, row];
			int minorX = x - (col * 3);
			int minorY = y - (row * 3);
			return bb.getNumber(minorX, minorY);
		}

		public void setNumber(int x, int y, int num) {
			int row = (int)Math.Floor((double)y / 3);
			int col = (int)Math.Floor((double)x / 3);
			BoardBox bb = board[col, row];
			int minorX = x - (col * 3);
			int minorY = y - (row * 3);
			bb.setNumber(minorX, minorY, num);
		}

		public string toString(bool asGrid = false) {
			string output = "";
			for (int y = 0; y < 9; y += 1) {
				for (int x = 0; x < 9; x += 1) {
					output += "" + getNumber(x, y);
				}
				if (asGrid) {
					output += "\n";
				}
			}
			return output;
		}

		public void whipeBoard() {
			for (int x = 0; x < 3; x += 1) {
				for (int y = 0; y < 3; y += 1) {
					board[x, y] = new BoardBox();
				}
			}
		}

	}

}