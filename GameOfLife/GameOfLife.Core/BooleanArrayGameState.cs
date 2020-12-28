using System;

namespace GameOfLife.Core
{
	/// <summary>
	///     Naive implementation of game field based on two dimensional boolean array.
	/// </summary>
	public class BooleanArrayGameState : IGameState
	{
		private Boolean[,] _currentState;
		private Boolean[,] _nextState;

		public BooleanArrayGameState()
		{
			_currentState = new Boolean[Size, Size];
			_nextState    = new Boolean[Size, Size];
		}

		public UInt16 Size => 64;

		/// <inheritdoc />
		/// <remarks>
		///    Note: this implementation first casts value to UInt16 (potentially causing overflow), then also takes remainder from division on <see cref="Size"/>.
		///    This should ensure proper looping.
		/// </remarks>
		public Boolean IsAlive(Int32 x, Int32 y) => _currentState[(UInt16) x % Size, (UInt16) y % Size];

		public void Advance()
		{
			for (UInt16 x = 0; x < Size; x++)
			for (UInt16 y = 0; y < Size; y++)
			{
				Int32 neighboursCount = CountNeighbours(x, y);
				if (neighboursCount < 2 || neighboursCount > 3)
				{
					_nextState[x, y] = false;
				}
				else if (neighboursCount == 3)
				{
					_nextState[x, y] = true;
				}
				else
				{
					_nextState[x, y] = _currentState[x, y];
				}
			}

			var old = _currentState;
			_currentState = _nextState;
			_nextState    = old;
		}

		public void SetIsAlive(UInt16 x, UInt16 y, Boolean isAlive)
		{
			_currentState[x, y] = isAlive;
		}

		private Int32 CountNeighbours(UInt16 x, UInt16 y) =>
			Convert.ToInt32(IsAlive(x, y + 1))
			+ Convert.ToInt32(IsAlive(x  + 1, y + 1))
			+ Convert.ToInt32(IsAlive(x  + 1, y))
			+ Convert.ToInt32(IsAlive(x  + 1, y - 1))
			+ Convert.ToInt32(IsAlive(x,      y - 1))
			+ Convert.ToInt32(IsAlive(x         - 1, y - 1))
			+ Convert.ToInt32(IsAlive(x         - 1, y))
			+ Convert.ToInt32(IsAlive(x         - 1, y + 1));
	}
}