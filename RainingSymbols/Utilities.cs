using System;
using System.Collections.Generic;
using System.Linq;

namespace RainingSymbols
{
    public static class Utilities
    {
        public static char[] PrintableChars { get; private set; }

        public static Random random;

        static Utilities()
        {
            random = new Random();

            List<char> printableCharsList = new List<char>();

            for (int i = char.MinValue; i < 256; i++)
            {
                char c = Convert.ToChar(i);
                if (!char.IsControl(c))
                {
                    printableCharsList.Add(c);
                }
            }

            PrintableChars = printableCharsList.ToArray();
        }

        public static IEnumerable<int> SelectRandomValuesInRange(int min, int max, int maxNumValues)
        {
            List<int> values = new List<int>();

            for (int i = 0; i < maxNumValues; i++)
            {
                values.Add(random.Next(min, max));
            }

            return values;
        }

        public static int InputInt(string prompt)
        {
            int value;

            while (true)
            {
                Console.WriteLine(prompt);

                if (int.TryParse(Console.ReadLine().Trim('\n'), out value))
                {
                    break;
                }

                Console.WriteLine("Invalid Input (Expected Integer)\n");
            }

            return value;
        }
    }
}