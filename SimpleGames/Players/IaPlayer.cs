using MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGames.Players
{
	public class IaPlayer : Player
	{
		private int NInput;
		public DeepLearning IA { get; set; }
		private int NOutput = 9;
		private int NeuronsPerLayers = 18;
		private int NLayers = 2;
		private List<double[]> Inputs;
		private List<int> GuessPosition;
		public int NumberOfGamesWon { get; set; }
		public bool FirstPlayer { get; set; }

		public IaPlayer(string name) : base(name)
		{
			NInput = 9;
			NOutput = 9;
			NeuronsPerLayers = 18;
			NLayers = 2;

			IA = new DeepLearning(NInput, NeuronsPerLayers, NOutput, NLayers);
			Inputs = new List<double[]>();
			GuessPosition = new List<int>();
		}

		public override string Play(Board board)
		{
			// [0, 1, 0.5, 0.5, 0.5, 1, ..., 1]
			var input = board.m_Board.Cast<int>().Select(p => p / 2.0 + 0.5).ToArray();
			Inputs.Add(input);
			var guess = IA.Predict(input);
			var expected = guess.ToArray();
			var learnRules = false;
			
			for (int i = 0; i < NInput; i++)
			{
				if (input[i] != 0.5 && guess[i] != 0)
				{
					learnRules = true;
					expected[i] = 0;
				}
			}
			
			if(learnRules)
				IA.Train(input, expected);

			return SelectPositionFromGuesses(guess, board);
		}


		internal override void ProcessEndGame(bool hasWon)
		{
			if (hasWon)
			{
				NumberOfGamesWon++;
				int i = 0;
				foreach (var input in Inputs)
				{
					var expected = input.ToArray();
					expected[GuessPosition[i++]] = 1;
					IA.Train(input, expected);
				}
			}
		}

		private string SelectPositionFromGuesses(double[] guess, Board board)
		{
			var maxGuess = 0d;
			var position = 0;
			for (int i = 0; i < board.RowCount; i++)
			{
				for (int j = 0; j < board.ColCount; j++)
				{
					if (board.m_Board[i, j] == 0 && guess[board.RowCount * i + j] > maxGuess)
					{
						maxGuess = guess[board.RowCount * i + j];
						position = board.RowCount * i + j;
					}
				}
			}

			GuessPosition.Add(position);
			return ToPosition(position);
		}

		private string ToPosition(int position)
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
