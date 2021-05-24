using SimpleGames.Players;
using SimpleGames.TicTacToe;
using System;
using System.IO;

namespace SimpleGames
{
	class Program
	{
		static bool P1turn = true;
		static long NumberOfGames = 0;
		static void Main(string[] args)
		{
			TrainIA();
		}

		private static void TrainIA()
		{
			Mode.RenderOn = false;
			var loic = new ConsolePlayer("loic");
			var p1 = new IaPlayer("robot1") { FirstPlayer = true };
			var p2 = new IaPlayer("robot2") { FirstPlayer = false };
			p2.IA = p1.IA;
			var path = @"C:\Users\Boudart Loic\source\repos\SimpleGames\SimpleGames\TicTacToe\Brains\brain.json";
			if (File.Exists(path))
			{
				p1.IA.Load(path);
			}

			string entree = "y";
			do
			{
				NumberOfGames++;
				if (Mode.RenderOn)
				{
					Console.Clear();
					Console.WriteLine($"Game N°{NumberOfGames}");
				}
				var ttt = new TicTacToeGame(p1, p2, P1turn);
				ttt.Start();

				//Console.WriteLine("Play again?(y/n)");
				//entree = Console.ReadLine();
				p1.FirstPlayer = !p1.FirstPlayer;
				p2.FirstPlayer = !p2.FirstPlayer;
				if (NumberOfGames % 5000 == 0)
				{
					p1.IA.Save(path);
					var buPath = path + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".json";
					//backup
					File.Copy(path, buPath, true);
				}
				P1turn = !P1turn;
			} while (entree == "y");

			Mode.RenderOn = true;
			do
			{

				var iaPlayer = p1.NumberOfGamesWon > p2.NumberOfGamesWon ? p1 : p2;

				var ttt2 = new TicTacToeGame(loic, iaPlayer, P1turn);
				ttt2.Start();

				//Console.WriteLine("Play again?(y/n)");
				//entree = Console.ReadLine();
				P1turn = !P1turn;
			} while (entree == "y");
			Console.Clear();
			Console.WriteLine("Bye !!");
		}

		private static void PlayRandomPlayer()
		{
			var r1 = new RandomPlayer();
			var r2 = new RandomPlayer();
			Mode.RenderOn = true;
			while (true)
			{
				P1turn = !P1turn;
				var ttt = new TicTacToeGame(r1, r2, true);
				ttt.Start();
				Console.ReadLine();
			}
		}

		private static void IaVsRandom()
		{
			{

				Mode.RenderOn = false;
				var p1 = new IaPlayer("robot1", false) { FirstPlayer = true };
				var path = @"C:\Users\Boudart Loic\source\repos\SimpleGames\SimpleGames\TicTacToe\Brains\brain.json";
				if (File.Exists(path))
				{
					p1.IA.Load(path);
				}
				var p2 = new IaPlayer("robot1", false) { FirstPlayer = false };
				for (int i = 0; i < 100000; i++)
				{
					P1turn = !P1turn;
					var ttt = new TicTacToeGame(p1, p2, P1turn);
					ttt.Start();
					NumberOfGames++;
				}

				Console.WriteLine("IA trained :  " + 100 * p1.NumberOfGamesWon * 1.0 / NumberOfGames + "%");
				Console.WriteLine("IA untrained : " + 100 * p2.NumberOfGamesWon * 1.0 / NumberOfGames + "%");
			}

			Console.ReadLine();
		}
	}
}
