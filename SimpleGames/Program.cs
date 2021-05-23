using SimpleGames.Players;
using SimpleGames.TicTacToe;
using System;

namespace SimpleGames
{
	class Program
	{
		static void Main(string[] args)
		{
			var p1 = new ConsolePlayer("loic");
			var p2 = new ConsolePlayer("mathias");

			string entree;
			do
			{
				Console.Clear();
				var ttt = new TicTacToeGame(p1, p2);
				ttt.Start();

				Console.WriteLine("Play again?(y/n)");
				entree = Console.ReadLine();
			} while (entree == "y" );
			Console.Clear();
			Console.WriteLine("Bye !!");
		}
	}
}
