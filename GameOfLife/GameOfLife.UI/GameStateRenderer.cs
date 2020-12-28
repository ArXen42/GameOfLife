using System;
using System.Windows.Media.Imaging;
using GameOfLife.Core;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace GameOfLife.UI
{
	public sealed class GameStateRenderer : IDisposable
	{
		public const Int32 CellSize   = 16;
		public const Int32 CellsCount = 64;
		public const Int32 RenderSize = CellSize * CellsCount;

		private readonly Mat<Byte> _img;

		public GameStateRenderer()
		{
			_img = new Mat<Byte>(RenderSize, RenderSize);
		}

		public BitmapSource Render(IGameState gameState)
		{
			for (Int32 x = 0; x < CellsCount; x++)
			for (Int32 y = 0; y < CellsCount; y++)
			{
				Boolean isAlive   = gameState.IsAlive(x, y);
				Byte    cellColor = (Byte) (isAlive ? 0 : 255);

				var roi     = _img.SubMat(new Rect(new Point(x * CellSize, y * CellSize), new Size(CellSize, CellSize)));
				var indexer = roi.GetIndexer();

				for (Int32 row = 0; row < CellSize; row++)
				for (Int32 col = 0; col < CellSize; col++)
				{
					Boolean isBorder = row    < 2
					                   || row > CellSize - 2
					                   || col < 2
					                   || col > CellSize - 2;

					if (isBorder)
					{
						indexer[row, col] = 128;
					}
					else
					{
						indexer[row, col] = cellColor;
					}
				}
			}

			var bitmapSource = _img.ToBitmapSource();
			bitmapSource.Freeze();
			return bitmapSource;
		}

		public void Dispose()
		{
			_img?.Dispose();
		}
	}
}