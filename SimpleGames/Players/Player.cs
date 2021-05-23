using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames.Players
{
	public abstract class Player
	{
		public string Name { get; }

		public Player(string name)
		{
			Name = name;
		}

		public abstract string Play();
	}
}
