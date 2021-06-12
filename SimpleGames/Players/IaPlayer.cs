using MachineLearning;
using SimpleGames.Exceptions;
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
		private int NOutput;
		private int NeuronsPerLayers;
		private int NLayers;
		private readonly List<double[]> Inputs;
		private readonly List<int> GuessPosition;
		public bool FirstPlayer { get; set; }

		private List<int> NumberOfWrongPositions { get; set; }
		public bool TrainingMode { get; set; }

		private int HasToLearn;
		public IaPlayer(string name, bool trainingMode = true) : base(name)
		{
			NInput = 9;
			NOutput = 9;
			NeuronsPerLayers = 36;
			NLayers = 2;

			IA = new DeepLearning(NInput, NeuronsPerLayers, NOutput, NLayers);
			Inputs = new List<double[]>();
			GuessPosition = new List<int>();
			NumberOfWrongPositions = new List<int>();
			TrainingMode = trainingMode;
		}

		public override string Play(Board board)
		{
			// [0, 1, 0.5, 0.5, 0.5, 1, ..., 1]
			var input = board.m_Board.Cast<int>().Select(p => p / 2.0 + 0.5).ToArray();
			Inputs.Add(input);
			Random random = new Random(Guid.NewGuid().GetHashCode());
			double[] guess;

			if (!TrainingMode)
			{
				guess = IA.Predict(input);
				return SelectPositionFromGuesses(guess, board);
			}
			//exploitation
			if (random.Next(10) < 8)
			{
				return Exploitation(board, input, out guess);
			}
			//exploration
			var n = random.Next(input.Count(i => i == 0.5));
			int ii = 0;
			int position = 0;
			foreach (var item in input)
			{
				if (ii == n)
				{
					GuessPosition.Add(position);
					return ToPosition(position);
				}
				if (item == 0.5)
				{
					ii++;
				}
				position++;
			}

			throw new GameLogicException("The IA Couldnot find a position to play !!");
		}

		private string Exploitation(Board board, double[] input, out double[] guess)
		{
			guess = IA.Predict(input);
			var expected = guess.ToArray();
			bool learnRules;

			//do
			//{
				learnRules = false;
				for (int i = 0; i < NInput; i++)
				{
					if (input[i] != 0.5)
					{
						expected[i] = 0;
						if (guess[i] > 0.1)
						{
							learnRules = true;
						}
					}
				}

				if (learnRules)
				{
					HasToLearn++;
					IA.Train(input, expected);
					guess = IA.Predict(input);
				}

			//} while (learnRules);
			return SelectPositionFromGuesses(guess, board);
		}

		internal override void ProcessEndGame(bool hasWon)
		{
			base.ProcessEndGame(hasWon);
			if (hasWon && TrainingMode)
			{
				int i = 0;
				foreach (var input in Inputs)
				{
					for (int j = 0; j <= i*2; j++)
					{
						var expected = input.Select(e => 0.0).ToArray();
						expected[GuessPosition[i]] = 1;
						IA.Train(input, expected);
					}
					i++;
				}
			}
			//else
			//{
			//	for (int j = 0; j < 20; j++)
			//	{
			//		int i = 0;
			//		foreach (var input in Inputs)
			//		{
			//			var expected = new double[input.Length];
			//			expected[GuessPosition[i++]] = 0;
			//			IA.Train(input, expected);
			//		}
			//	}
			//}

			GuessPosition.Clear();
			Inputs.Clear();
			if (NumberOfGamesWon % 1000 == 0)
			{
				NumberOfWrongPositions.Add(HasToLearn);
			}
			HasToLearn = 0;
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

	}
}
