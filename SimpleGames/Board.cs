using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames
{
	public abstract class Board
	{
		internal readonly int[,] m_Board;

		public int RowCount { get; set; }
		public int ColCount { get; set; }

		public Board(int row, int col)
		{
			m_Board = new int[row, col];
			RowCount = row;
			ColCount = col;

		}

		public int this[string position]
		{
			get
			{
				int r = position[0] - 97;
				int c = position[1] - 49;
				return m_Board[r, c];
			}
			set
			{
				int r = position[0] - 97;
				int c = position[1] - 49;
				m_Board[r, c] = value;
			}
		}

		public abstract bool HasWinner();

		public abstract void Render();

		public bool IsValid(string position)
		{
			int r = position[0] - 97;
			int c = position[1] - 49;
			return r < 3 && c < 3;
		}

		public bool IsFull()
		{
			var isFull = true;
			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColCount; j++)
				{
					isFull &= m_Board[i, j] != 0;
				}
			}

			return isFull;
		}

	}
}
