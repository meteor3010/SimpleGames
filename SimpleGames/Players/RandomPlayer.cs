using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames.Players
{
	public class RandomPlayer : Player
	{

		public RandomPlayer():base("random")
		{

		}

		public override string Play(Board board)
		{
			return ToPosition(new Random().Next(9));
		}
	}
}
