using System;
using System.Threading.Tasks;
using System.Windows;
using GameOfLife.Core;

namespace GameOfLife.UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Task.Run(
				async () =>
				{
					var       gameState = new BooleanArrayGameState();
					using var renderer  = new GameStateRenderer();

					gameState.SetIsAlive(4, 4, true);
					gameState.SetIsAlive(5, 4, true);
					gameState.SetIsAlive(6, 4, true);
					gameState.SetIsAlive(6, 5, true);
					gameState.SetIsAlive(5, 6, true);

					while (true) // TODO: play, pause, fps control, etc, etc
					{
						var bitmapSource = renderer.Render(gameState);
						Dispatcher.Invoke(() => RenderImage.Source = bitmapSource);

						gameState.Advance();
						await Task.Delay(TimeSpan.FromMilliseconds(16));
					}

					// ReSharper disable once FunctionNeverReturns
				}
			);
		}
	}
}