using SimpleGames.Players;
using SimpleGames.TicTacToe;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames
{
	public abstract class BoardGame
	{
		public Player Player1 { get; }
		public Player Player2 { get; }
		public Board Board { get; protected set; }
		public bool IsPlayer1Turn { get; private set; }
		public bool GameIsOver { get; protected set; }
		public Player Winner { get; protected set; }

		public BoardGame(Player player1, Player player2, bool player1Turn)
		{
			Player1 = player1;
			Player2 = player2;
			IsPlayer1Turn = player1Turn;
		}

		public void Start()
		{
			Board.Render();

			while (!GameIsOver)
			{
				string position;
				if (IsPlayer1Turn)
				{
					do
					{
						position = Player1.Play(Board);
					} while (!IsValid(position));
				}
				else
				{
					do
					{
						position = Player2.Play(Board);
					} while (!IsValid(position));
				}

				Play(position);
				Board.Render();

				IsPlayer1Turn = !IsPlayer1Turn;
			}

			ProcessEndGame(Winner);
		}

		private void ProcessEndGame(Player player)
		{
			Player1.ProcessEndGame(player == Player1);
			Player2.ProcessEndGame(player == Player2);
			if (Mode.RenderOn)
			{
				if (player != null)
				{
					Console.WriteLine($"!!! {player.Name} has won !!!");
				}
				else
				{
					Console.WriteLine("!! This is a tie !!");
				}
			}
		}

		protected abstract void Play(string position);
		protected abstract bool IsValid(string position);

	}
}
