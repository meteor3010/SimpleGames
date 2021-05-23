using SimpleGames.Players;
using SimpleGames.TicTacToe;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames.TicTacToe
{
	public class TicTacToeGame : BoardGame
	{
		public TicTacToeGame(Player player1, Player player2): base(player1, player2)
		{
			Board = new TicTacToeBoard();
		}

		protected override bool IsValid(string position)
		{
			return Board.IsValid(position) && Board[position] == 0;
		}

		protected override void Play(string position)
		{
			if (IsPlayer1Turn)
			{
				Board[position] = 1;

				if(Board.HasWinner())
				{
					GameIsOver = true;
					Winner = Player1;
				}
			}
			else
			{
				Board[position] = -1;

				if (Board.HasWinner())
				{
					GameIsOver = false;
					Winner = Player2;
				}
			}

			if (Board.IsFull())
			{
				GameIsOver = true;
			}
		}
	}
}
