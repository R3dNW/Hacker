using System;
using System.Collections.Generic;

namespace RainingSymbols
{
    public struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object o)
        {
            if (o is Position)
            {
                return false;
            }

            Position b = (Position)o;

            return this.x == b.x && this.y == b.y;
        }

        public override int GetHashCode()
        {
            return (this.x * 1000000) + this.y;
        }

        public static bool operator ==(Position a, Position b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Position a, Position b)
        {
            return a.x != b.x || a.y != b.y;
        }
    }

    public static class GraphicsQueue
    {
        static List<Position> posQueue;
        static List<char> charQueue;

        static int screenWidth;
        static int screenHeight;

        static GraphicsQueue()
        {
            posQueue = new List<Position>();
            charQueue = new List<char>();

            screenWidth = Console.WindowWidth;
            screenHeight = Console.WindowHeight;
        }

        public static void Push(Position pos, char symbol)
        {
            if (posQueue.Contains(pos))
            {
                charQueue[posQueue.IndexOf(pos)] = symbol;
            }
            else
            {
                posQueue.Add(pos);
                charQueue.Add(symbol);
            }
        }

        public static void Draw()
        {
            if (screenWidth != Console.WindowWidth)
            {
                Console.Clear();
            }

            if (screenHeight != Console.WindowHeight)
            {
                Console.Clear();
            }

            if (posQueue.Count != charQueue.Count)
            {
                posQueue = new List<Position>();
                charQueue = new List<char>();

                return;
            }

            for (int i = 0; i < posQueue.Count; i++)
            {
                int indexToCheck = 0;

                if (posQueue[indexToCheck].x < Console.WindowWidth && posQueue[indexToCheck].y < Console.WindowHeight)
                {
                    Console.SetCursorPosition(posQueue[indexToCheck].x, posQueue[indexToCheck].y);
                    Console.Write(charQueue[indexToCheck]);
                }

                posQueue.RemoveAt(indexToCheck);
                charQueue.RemoveAt(indexToCheck);
            }

            screenWidth = Console.WindowWidth;
            screenHeight = Console.WindowHeight;
        }
    }
}