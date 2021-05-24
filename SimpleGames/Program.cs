using SimpleGames.Players;
using SimpleGames.TicTacToe;
using System;

namespace SimpleGames
{
	class Program
	{
		static void Main(string[] args)
		{
			Mode.RenderOn = false;
			var loic = new ConsolePlayer("loic");
			var p1 = new IaPlayer("robot1") { FirstPlayer = true };
			var p2 = new IaPlayer("robot2") { FirstPlayer = false };
			p2.IA = p1.IA;
			var numberOfGames = 0;
			string entree = "y";
			do
			{
				numberOfGames++;
				if (Mode.RenderOn)
				{
					Console.Clear();
					Console.WriteLine($"Game N°{numberOfGames}");
				}
				var ttt = new TicTacToeGame(p1, p2);
				ttt.Start();

				//Console.WriteLine("Play again?(y/n)");
				//entree = Console.ReadLine();
				p1.FirstPlayer = !p1.FirstPlayer;
				p2.FirstPlayer = !p2.FirstPlayer;
			} while (entree == "y" && numberOfGames < 5000);

			Mode.RenderOn = true;
			var iaPlayer = p1.NumberOfGamesWon > p2.NumberOfGamesWon ? p1 : p2;

			var ttt2 = new TicTacToeGame(loic, iaPlayer);
			ttt2.Start();

			Console.Clear();
			Console.WriteLine("Bye !!");
		}
	}
}
