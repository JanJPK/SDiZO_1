using System;
using System.IO;

namespace SDiZO_1.Structures
{
    class SdArray
    {
        // Property i zmienne.
        public int Size { get; set; }
        private int[] array;
        
        // Konstruktor.
        public SdArray()
        {
            Size = 0;
        }

        // Dodawanie elementu na koniec.
        public void AddNumberEnd(int number)
        {
            // Utworzenie większej tablicy, przekopiowanie zawartości.
            Size++;
            int[] newArray = new int[Size];
            for (int i = 0; i < (Size - 1); i++)
            {
                newArray[i] = array[i];
            }
            // Dodawanie nowej liczby na sam koniec.
            newArray[Size - 1] = number;
            // Przypisanie zmiennym nowej tablicy i jej wielkości.
            array = newArray;
            Size = newArray.Length;
        }

        // Dodawanie elementu na koniec.
        public void AddNumberBeg(int number)
        {
            // Utworzenie większej tablicy, przekopiowanie zawartości.
            Size++;
            int[] newArray = new int[Size];
            for (int i = 0; i < (Size - 1); i++)
            {
                newArray[i] = array[i];
            }
            // Dodawanie nowej liczby na sam koniec.
            newArray[0] = number;
            // Przypisanie zmiennym nowej tablicy i jej wielkości.
            array = newArray;
            Size = newArray.Length;
        }

        // Dodawanie elementu w wybrane miejsce.
        public void AddNumberPlus(int number, int index)
        {
            // Nie możemy wyjść poza rozmiar tablicy
            if (index < Size && index >= 0)
            {
                //Console.WriteLine("#### DODAJE ELEMENT ####\n");
                int[] newArray = new int[Size + 1];

                // Wsadzamy element na miejsce [index]
                // [0 -> (index-1)] = stare elementy
                for (int i = 0; i < index; i++)
                {
                    newArray[i] = array[i];
                }

                // [index] = nowy element
                newArray[index] = number;

                // [(index+1) -> max] = stare elementy
                for (int i = index + 1; i < newArray.Length; i++)
                {
                    newArray[i] = array[i - 1];
                }

                // Przypisanie zmiennym nowej tablicy i jej wielkości.
                array = newArray;
                Size = array.Length;
            }
            else
            {
                //Console.WriteLine("#### NIE MOZNA DODAC, NIEPOPRAWNY INDEX ####\n");
            }
        }

        // Usuwanie elementu z wybranego miejsca.
        public void DeleteNumber(int index)
        {
            if (index < Size && index > 0)
            {
                Console.WriteLine("#### USUWAM ELEMENT " + array[index] + " Z MIEJSCA " + index + " ####\n");
                // [0 -> (index-1)] - i ---> i
                // [index -> max] - i ---> (i-1)
                int[] newArray = new int[Size - 1];
                for (int i = 0; i < index; i++)
                {
                    newArray[i] = array[i];
                }
                for (int i = index; i < newArray.Length; i++)
                {
                    newArray[i] = array[i + 1];
                }
                // Przypisanie zmiennym nowej tablicy i jej wielkości.
                array = newArray;
                Size = array.Length;

            }
            else
            {
                Console.WriteLine("#### NIE MOZNA USUNAC, NIEPOPRAWNY INDEX ####\n");
            }

        }

        // Wypisywanie zawartości.
        public void ListContents()
        {
            //Console.WriteLine("#### WYPISYWANIE ZAWARTOSCI TABLICY ####");
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("["+i+"] = " + array[i]);
            }
            //Console.WriteLine("#### KONIEC WYPISYWANIA ####\n");
            
        }
    }
}
