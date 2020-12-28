using System;

namespace GameOfLife.Core
{
	/// <summary>
	///     Represents Game of Life state.
	/// </summary>
	public interface IGameState
	{
		/// <summary>
		///    Field size. Note that field is expected to be looped and not be bigger then 65535x65535.
		/// </summary>
		public UInt16 Size { get; }

		/// <summary>
		///    Returns whether cell with given coordinates is alive.
		/// </summary>
		public Boolean IsAlive(Int32 x, Int32 y);

		/// <summary>
		///    Advance one turn.
		/// </summary>
		public void Advance();

		/// <summary>
		///     Manually set value.
		/// </summary>
		void SetIsAlive(UInt16 x, UInt16 y, Boolean isAlive);
	}
}