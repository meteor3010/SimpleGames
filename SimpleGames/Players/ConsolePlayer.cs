using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames.Players
{
	public class ConsolePlayer : Player
	{
		public ConsolePlayer(string name): base(name)
		{

		}

		public override string Play()
		{
			string entree;
			while (true)
			{
				Console.WriteLine($"[{Name}]'s turn:");
				entree = Console.ReadLine();
				if(entree.Length == 2);
					return entree;
			}
		}
	}
}
