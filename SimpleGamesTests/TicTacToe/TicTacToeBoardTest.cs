using SimpleGames;
using SimpleGames.Players;
using SimpleGames.TicTacToe;
using System;
using Xunit;

namespace SimpleGamesTests
{
	public class TicTacToeBoardTest
	{
		[Theory]
		[MemberData(nameof(HasWinner_DataSource))]
		public void TTT_HasWinner_Expected(Board board, bool expected)
		{
			// prepare

			// execute
			var result = board.HasWinner();

			// verify
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData(1, true)]
		[InlineData(0, false)]
		public void TTT_IsFull_IsFull(int value, bool expected)
		{
			// prepare
			TicTacToeBoard board = new TicTacToeBoard();

			//full board as tie
			board["a1"] = 1;
			board["a2"] = -1;
			board["a3"] = value;
			board["b1"] = 1;
			board["b2"] = -1;
			board["b3"] = 1;
			board["c1"] = -1;
			board["c2"] = 1;
			board["c3"] = -1;

			// execute
			var result = board.IsFull();

			// verify
			Assert.Equal(expected, result);
		}

		public static TheoryData<Board, bool> HasWinner_DataSource()
		{
			var theoryData = new TheoryData<Board, bool>();
			TicTacToeBoard board;

			//horiz and vertical
			for (int i = -1; i <= 1; i += 2)
			{
				{
					board = new TicTacToeBoard();
					board["a1"] = i;
					board["a2"] = i;
					board["a3"] = i;
					theoryData.Add(board, true);
				}

				{
					board = new TicTacToeBoard();
					board["b1"] = i;
					board["b2"] = i;
					board["b3"] = i;
					theoryData.Add(board, true);
				}

				{
					board = new TicTacToeBoard();
					board["c1"] = i;
					board["c2"] = i;
					board["c3"] = i;
					theoryData.Add(board, true);
				}

				{
					board = new TicTacToeBoard();
					board["a1"] = i;
					board["b1"] = i;
					board["c1"] = i;
					theoryData.Add(board, true);
				}

				{
					board = new TicTacToeBoard();
					board["a2"] = i;
					board["b2"] = i;
					board["c2"] = i;
					theoryData.Add(board, true);
				}

				{
					board = new TicTacToeBoard();
					board["a3"] = i;
					board["b3"] = i;
					board["c3"] = i;
					theoryData.Add(board, true);
				}

				{
					board = new TicTacToeBoard();
					board["a1"] = i;
					board["b2"] = i;
					board["c3"] = i;
					theoryData.Add(board, true);
				}

				{
					board = new TicTacToeBoard();
					board["a3"] = i;
					board["b2"] = i;
					board["c1"] = i;
					theoryData.Add(board, true);
				}
			}

			for (int i = -1; i <= 1; i += 2)
			{
				{
					board = new TicTacToeBoard();
					board["a1"] = i;
					board["a2"] = -i;
					board["a3"] = i;
					theoryData.Add(board, false);
				}

				{
					board = new TicTacToeBoard();
					board["b1"] = i;
					board["b2"] = -i;
					board["b3"] = i;
					theoryData.Add(board, false);
				}

				{
					board = new TicTacToeBoard();
					board["c1"] = i;
					board["c2"] = -i;
					board["c3"] = i;
					theoryData.Add(board, false);
				}

				{
					board = new TicTacToeBoard();
					board["a1"] = i;
					board["b1"] = -i;
					board["c1"] = i;
					theoryData.Add(board, false);
				}

				{
					board = new TicTacToeBoard();
					board["a2"] = i;
					board["b2"] = -i;
					board["c2"] = i;
					theoryData.Add(board, false);
				}

				{
					board = new TicTacToeBoard();
					board["a3"] = i;
					board["b3"] = -i;
					board["c3"] = i;
					theoryData.Add(board, false);
				}

				{
					board = new TicTacToeBoard();
					board["a1"] = i;
					board["b2"] = -i;
					board["c3"] = i;
					theoryData.Add(board, false);
				}

				{
					board = new TicTacToeBoard();
					board["a3"] = i;
					board["b2"] = -i;
					board["c1"] = i;
					theoryData.Add(board, false);
				}
			}

			return theoryData;
		}
	}
}
