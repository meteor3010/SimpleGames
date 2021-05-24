using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames.TicTacToe
{
	public class TicTacToeBoard : Board
	{
		public TicTacToeBoard() : base(3, 3)
		{

		}

		public override bool HasWinner()
		{
			bool hasWinner;
			// horizonthal and verticals check
			for (uint i = 0; i < 3; i++)
			{
				hasWinner = (m_Board[i, 0] == m_Board[i, 1] && m_Board[i, 0] == m_Board[i, 2] && m_Board[i, 0] != 0)
					|| (m_Board[0, i] == m_Board[1, i] && m_Board[0, i] == m_Board[2, i] && m_Board[0, i] != 0);
				if (hasWinner)
					return true;
			}
			//diagonals
			for (int i = -1; i <= 1; i += 2)
			{
				hasWinner = m_Board[1 + i, 0] == m_Board[1, 1] && m_Board[1 - i, 2] == m_Board[1, 1] && m_Board[1, 1] != 0;
				if (hasWinner)
					return true;
			}

			return false;
		}

		public override void Render()
		{
			if (!Mode.RenderOn)
			{
				return;
			}

			Console.Clear();
			for (int i = 0; i < 3; i++)
			{
				if (i == 0)
				{
					Console.WriteLine("  1 2 3");
				}
				for (int j = 0; j < 3; j++)
				{
					if (j == 0)
					{
						if (i == 0)
							Console.Write("a ");
						if (i == 1)
							Console.Write("b ");
						if (i == 2)
							Console.Write("c ");
					}
					var value = m_Board[i, j];
					string mark;
					if (value == 1)
						mark = "X ";
					else if (value == -1)
						mark = "O ";
					else
						mark = ". ";
					Console.Write(mark);
				}
				Console.WriteLine();
			}
		}
	}
}
