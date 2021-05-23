using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames
{
	public abstract class Board
	{
		internal readonly int[,] m_Board;

		public Board(int row, int col)
		{
			m_Board = new int[row, col];

			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					m_Board[i, j] = 0;
				}
			}
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
	}
}
