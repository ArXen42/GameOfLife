﻿using System;

namespace GameOfLife.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameCore();
            game.GameStart();
        }
    }
}