using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames.Players
{
	public abstract class Player
	{
		public string Name { get; }
		public int NumberOfGamesWon { get; set; }

		public Player(string name)
		{
			Name = name;
		}

		public abstract string Play(Board board);

		internal virtual void ProcessEndGame(bool hasWon)
		{
			if (hasWon)
				NumberOfGamesWon++;
		}

		protected string ToPosition(int position)
		{
			switch (position)
			{
				case 0:
					return "a1";
				case 1: return "a2";
				case 2: return "a3";
				case 3: return "b1";
				case 4: return "b2";
				case 5: return "b3";
				case 6: return "c1";
				case 7: return "c2";
				case 8: return "c3";
			}

			throw new Exception($"Wrong position: {position}");
		}
	}
}
