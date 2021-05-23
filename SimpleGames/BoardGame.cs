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

		public BoardGame(Player player1, Player player2)
		{
			Player1 = player1;
			Player2 = player2;
			IsPlayer1Turn = true;
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
						position = Player1.Play();
					} while (!IsValid(position));
				}
				else
				{
					do
					{
						position = Player2.Play();
					} while (!IsValid(position));
				}

				Play(position);
				Board.Render();

				IsPlayer1Turn = !IsPlayer1Turn;
			}

			if (Winner != null)
			{
				RenderPlayerWon(Player1);
			}
			else
			{
				Console.WriteLine("!! This is a tie !!");
			}
		}

		private void RenderPlayerWon(Player player)
		{
			Console.WriteLine($"!!! {player.Name} has won !!!");
		}

		protected abstract void Play(string position);
		protected abstract bool IsValid(string position);

	}
}
