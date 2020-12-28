using System;
using System.Threading;
using GameOfLife.Core;

namespace GameOfLife.ConsoleUI
{
	internal static class Program
	{
		private const Int32 RenderSize = 16;

		private static void Render(IGameState gameState)
		{
			Console.Clear();

			for (Int32 y = 0; y < RenderSize; y++)
			{
				for (Int32 x = 0; x < RenderSize; x++)
				{
					Console.Write(gameState.IsAlive(x, y) ? "x" : ".");
				}

				Console.WriteLine();
			}
		}

		private static void Main(String[] args)
		{
			var state = new BooleanArrayGameState();
			state.SetIsAlive(4, 4, true);
			state.SetIsAlive(5, 4, true);
			state.SetIsAlive(6, 4, true);
			state.SetIsAlive(6, 5, true);
			state.SetIsAlive(5, 6, true);
			for (Int32 i = 0; i < 100; i++)
			{
				Render(state);
				state.Advance();
				Thread.Sleep(200);
			}
		}
	}
}