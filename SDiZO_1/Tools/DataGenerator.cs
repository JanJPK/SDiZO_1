using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SDiZO_1.Tools
{
    public static class DataGenerator
    {
        private static Random rng = new Random();
        private static List<int> list;

        // Generuje tablice z [amount] liczb o wartościach od [lowerLimit] do [upperLimit].
        public static int[] NewArray(int lowerLimit, int upperLimit, int amount)
        {
            list = new List<int>();
            for (int i = 0; i < amount; i++)
            {
                list.Add(rng.Next(lowerLimit, upperLimit + 1));
            }
            return list.ToArray();
        }

        // Generuje tablicę z [amount] liczb bez powtórzeń.
        // Tworzy listę zawierającą wszystkie liczby z zakresu, "miesza" nimi a następnie wybiera pierwsze [amount].
        public static int[] NewArrayUnique(int lowerLimit, int upperLimit, int amount)
        {
            list = new List<int>();
            for (int i = lowerLimit; i < upperLimit; i++)
            {
                list.Add(i);
            }
            int[] array = list.OrderBy(x => rng.Next()).ToArray();
            return array.Take(amount).ToArray();
        }

        // Generuje plik "Input.txt" z [amount] liczb o wartościach od [lowerLimit] do [upperLimit].
        public static void NewInput(int lowerLimit, int upperLimit, int amount)
        {
            using (StreamWriter sw = new StreamWriter(@".\" + "Input.txt"))
            {
                sw.WriteLine(amount);
                for (int i = 0; i < amount; i++)
                {
                    sw.WriteLine(rng.Next(lowerLimit, upperLimit));
                }
            }
        }

    }
}