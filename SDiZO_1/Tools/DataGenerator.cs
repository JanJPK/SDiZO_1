using System;
using System.IO;

namespace SDiZO_1.Tools
{
    public static class DataGenerator
    {
        private static Random rng = new Random();
        private static int[] array;

        // Generuje tablice z [amount] liczb o wartościach od [lowerLimit] do [upperLimit].
        public static int[] NewArray(int lowerLimit, int upperLimit, int amount)
        {
            array = new int[amount];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rng.Next(lowerLimit, upperLimit);
            }
            return array;
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