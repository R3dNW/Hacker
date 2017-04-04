using System;
using System.Collections.Generic;

namespace RainingSymbols
{
    public class Column
    {
        int _Length;

        public Game game;

        public int Length
        {
            get
            {
                return this._Length;
            }

            set
            {
                while (value > this.chars.Count)
                {
                    this.chars.Add(Utilities.PrintableChars[Utilities.random.Next(0, Utilities.PrintableChars.Length)]);
                }

                int lengthDiff = value - this._Length;

                if (lengthDiff > 0)
                {
                    for (int i = this._Length; i < value; i++)
                    {
                        GraphicsQueue.Push(new Position(xPos, i), this.chars[i]);
                    }
                }
                else if (lengthDiff < 0)
                {
                    for (int i = this._Length; i > value; i--)
                    {
                        GraphicsQueue.Push(new Position(xPos, i), ' ');
                    }
                }

                this._Length = value >= Console.WindowHeight
                    ? Console.WindowHeight - 2
                    : value <= 0
                        ? 0
                        : value;
            }
        }

        int xPos;

        List<char> chars;

        public Column(int xPos, Game game)
        {
            this.game = game;

            this.xPos = xPos;
            this.chars = new List<char>();

            for (int y = 0; y < Console.WindowHeight; y++)
            {
                this.chars.Add(Utilities.PrintableChars[Utilities.random.Next(0, Utilities.PrintableChars.Length)]);
            }

            this.Length = Utilities.random.Next(0, Console.WindowHeight);

            for (int y = 0; y < this.Length; y++)
            {
                GraphicsQueue.Push(new Position(xPos, y), this.chars[y]);
            }
        }

        public void Update()
        {
            /*if (Console.WindowHeight > this.chars.Count)
            {
                this.chars.Add(Utilities.PrintableChars[Utilities.random.Next(0, Utilities.PrintableChars.Length)]);
            }
            else if (Console.WindowHeight < this.chars.Count)
            {
                this.chars.RemoveAt(this.chars.Count - 1);
            }*/

            foreach (int y in Utilities.SelectRandomValuesInRange(0, this.chars.Count, game.charChangeRate))
            {
                this.chars[y] = Utilities.PrintableChars[Utilities.random.Next(0, Utilities.PrintableChars.Length)];

                if (y < this.Length)
                {
                    GraphicsQueue.Push(new Position(xPos, y), this.chars[y]);
                }
            }
        }
    }
}
