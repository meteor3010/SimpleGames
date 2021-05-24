using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleGames.Exceptions
{
	public class GameLogicException : Exception
	{
		public GameLogicException(string message) : base(message)
		{

		}
	}
}
