using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDiZO_1.Structures
{
    class SdHeap
    {
        /*
         * Niech n będzie liczbą węzłów w drzewie, a k numerem węzła.
         * numer lewego syna = 2k + 1
         * numer prawego syna = 2k + 2
         * numer ojca = [(k - 1) / 2], dla k > 0
         * lewy syn istnieje, jeśli 2k + 1 < n
         * prawy syn istnieje, jeśli 2k + 2 < n
         * węzeł k jest liściem, jeśli 2k + 2 > n
         */

        // Property i zmienne.
        public int Size { get; set; }
        private int[] array;
        
        // Konstruktor.
        public SdHeap()
        {
            Size = 0;
        }

        // Dodawanie elementu.
        public void Add(int number)
        {
            // Powiększanie tablicy.
            Size++;
            int[] newArray = new int[Size];
            for (int i = 0; i < (Size - 1); i++)
            {
                newArray[i] = array[i];
            }
            newArray[Size - 1] = number;
            array = newArray;

            // Zamienianie miejsc gdy jest to potrzebne.
            int index = (Size - 1);
            while (index > 0 && array[((index - 1) / 2)] < number)
            {
                int swap = array[((index - 1) / 2)];
                array[((index - 1) / 2)] = number;
                array[index] = swap;
                index = ((index - 1) / 2);
            }
        }

        // Usuwanie korzenia.
        public void Delete()
        {
            /*
            * numer lewego syna = 2k + 1
            * numer prawego syna = 2k + 2
            */

            // Zamiana miejscami ostatniego elementu z pierwszym.
            array[0] = array[Size - 1];
            Size--;
            // Przeniesienie zawartości tablicy.
            int [] newArray = new int[Size];
            for (int i = 0; i < (Size); i++)
            {
                newArray[i] = array[i];
            }
            array = newArray;
            // Sortowanie w celu zachowania warunku kopca.
            int index = 0;
            int lChildIndex = 2 * index + 1;
            int rChildIndex = 2 * index + 2;
            bool done = false;
            int swap;
            while (!done)
            {
                if (rChildIndex < Size && lChildIndex < Size)
                {
                    if (array[rChildIndex] > array[index] || array[lChildIndex] > array[index])
                    {
                        if (array[rChildIndex] > array[lChildIndex])
                        {
                            swap = array[index];
                            array[index] = array[rChildIndex];
                            array[rChildIndex] = swap;
                            index = rChildIndex;
                        }
                        else
                        {
                            swap = array[index];
                            array[index] = array[lChildIndex];
                            array[lChildIndex] = swap;
                            index = lChildIndex;
                        }
                        
                    }
                    else
                    {
                        done = true;
                    }
                    
                }
                else
                {
                    if (lChildIndex < Size)
                    {
                        if (array[lChildIndex] > array[index])
                        {
                            swap = array[index];
                            array[index] = array[lChildIndex];
                            array[lChildIndex] = swap;
                            index = lChildIndex;
                        }
                        else
                        {
                            done = true;
                        }
                    }
                    else
                    {
                        done = true;
                    }
                    
                }

                lChildIndex = 2 * index + 1;
                rChildIndex = 2 * index + 2;
            }
        }

        // Wyszukiwanie elementu o zadanej wartości.
        // Zwraca indeks elementu lub -1 jeżeli taki element nie został znaleziony.
        public int FindByValue(int value)
        {
            int index = 0;
            while (index < Size)
            {
                if (array[index] == value)
                {
                    return index;
                }
                else
                {
                    index++;
                }
            }
            return -1;
        }

        // Wypisywanie zawartości do pliku.
        public void SaveData()
        {
            using (StreamWriter sw = new StreamWriter("./Kopc_Zawartość.txt"))
            {
                if (Size > 0)
                {
                    sw.WriteLine("Zawartość w formie tablicy:");
                    sw.Write("[");
                    for (int i = 0; i < array.Length; i++)
                    {
                        sw.Write(array[i] + " ");
                    }
                    sw.Write("]\n");

                    sw.WriteLine("Zawartość w formie drzewa:");
                    sw.WriteLine("Górna gałąź - lewa strona; Dolna - prawa strona");
                    HeapForm(0, " ", sw);
                }
                else
                {
                    sw.WriteLine("Kopiec jest pusty.");
                }

            }

        }


        private void HeapForm(int index, String prefix, StreamWriter sw)
        {
            if (index > array.Length)
            {
                sw.WriteLine(prefix + "-- [brak]");
            }
            else
            {
                sw.WriteLine(prefix + "-- [" + array[index]+"]");
                if ((index * 2 + 1) < array.Length)
                {
                    HeapForm((index * 2 + 1), prefix + "|  ", sw);
                }

                if ((index * 2 + 2) < array.Length)
                {
                    HeapForm((index * 2 + 2), prefix + "|  ", sw);
                }

            }
        }
    }
}
