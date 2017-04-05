using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace RainingSymbols
{
    public class Game
    {
        public int charChangeRate { get; protected set; }
        int lengthChangeRate;

        int targetFrameTimeMS;

        Stopwatch stopwatch;
        long timeForNextFrame;

        List<Column> columns;

        int screenWidth = 0;
        int screenHeight = 0;

        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            targetFrameTimeMS = 1000 / Utilities.InputInt("Target Speed (Reccommended: 15 to 60)");

            charChangeRate = Utilities.InputInt("Character Swap Rate (Reccommended: 1 to 3)");

            lengthChangeRate = Utilities.InputInt("Length Change Rate: (Reccommended: 20 to 100)");

            GraphicsQueue.ClearScreen();

            Console.CursorVisible = false;

            columns = new List<Column>();

            /*for (int x = 0; x < Console.WindowWidth; x++)
            {
                columns.Add(new Column(x, this));
            }*/

            stopwatch = new Stopwatch();
            stopwatch.Start();
            timeForNextFrame = 0;

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds < timeForNextFrame)
                {
                    Thread.Sleep(10);
                    continue;
                }

                timeForNextFrame = stopwatch.ElapsedMilliseconds + targetFrameTimeMS;

                if (screenWidth != Console.WindowWidth)
                {
                    GraphicsQueue.ClearScreen();
                    this.columns = new List<Column>();
                    screenWidth = Console.WindowWidth;
                }

                if (screenHeight != Console.WindowHeight)
                {
                    GraphicsQueue.ClearScreen();
                    this.columns = new List<Column>();
                    screenHeight = Console.WindowHeight;
                }

                if (Console.WindowWidth > this.columns.Count)
                {
                    columns.Add(new Column(this.columns.Count, this));
                }
                else if (Console.WindowWidth < this.columns.Count)
                {
                    columns.RemoveAt(this.columns.Count - 1);
                }

                foreach (Column column in columns)
                {
                    column.Update();
                }

                foreach (int i in Utilities.SelectRandomValuesInRange(0, columns.Count, lengthChangeRate))
                {
                    columns[i].Length += Utilities.random.Next(-1, 2);
                }

                GraphicsQueue.Draw();
            }
        }
    }
}