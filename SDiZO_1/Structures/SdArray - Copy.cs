using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDiZO_1.Structures
{
    class SdArray
    {
        private int size;
        private int[] array;

        // Losowo: tworzy tablicę [amount] losowych liczb
        public SdArray(int amount)
        {
            this.size = amount;
        }

        // Odczyt: odczytuje tablicę z pliku [filename]
        public SdArray(string filename)
        {
            ReadFile(filename);
        }

        private void ReadFile(string filename)
        {
            // Wczytuje plik
            // Struktura pliku:
            //      pierwsza liczba określa wielkość struktury danych
            //      następne liczby oddzielone są losowymi białymi znakami
            try
            {
                using (StreamReader sr = new StreamReader(@".\" + filename))
                {
                    // Peek() podgląda znak ale go nie zjada
                    // Read() zjada znak wczytując go i przestawiając znacznik o jeden dalej
                    // Oba zwracają wartość ASCII do int
                    /*  
                     *  ASCII   symbol
                     *    48       0
                     *    57       9
                     *    45       -
                     */

                    int symbol;
                    if (sr.Peek() == 45)
                    {
                        // Błąd - tablica nie może być ujemnej wielkości
                    }
                    else
                    {
                        // Wczytuje pierwszą liczbę która zostanie wielkością tablicy
                        if (!sr.EndOfStream)
                        {
                            symbol = sr.Read();
                            string number = "";
                            // Sprawdza czy znak jest minusem
                            // Jeżeli jest to dodaje go do stringa
                            // Minus może być tylko jeden i tylko na początku liczby
                            if (symbol == 45)
                            {
                                number = number + (char) symbol;
                                symbol = sr.Read();
                            }
                            else
                            {
                                while (symbol > 47 && symbol < 58)
                                {
                                    number = number + (char)symbol;
                                    symbol = sr.Read();
                                }
                                if (number.Length > 0)
                                {
                                    this.size = Convert.ToInt32(number);
                                    this.array = new int[size];
                                }
                            }
                            
                        }
                    }

                    // Wczytuje pozostałe liczby
                    int index = 0;
                    
                    while (!sr.EndOfStream)
                    {
                        symbol = sr.Read();
                        string number = "";
                        // Sprawdza czy znak jest minusem
                        // Jeżeli jest to dodaje go do stringa
                        // Minus może być tylko jeden i tylko na początku liczby
                        if (symbol == 45)
                        {
                            number = number + (char)symbol;
                            symbol = sr.Read();
                        }
                        // Tworzy liczbę 
                        while (symbol > 47 && symbol < 58)
                        {
                            number = number + (char)symbol;
                            symbol = sr.Read();
                        }
                        // Dodaje liczbę
                        if ((number.Length > 1 && number[0] == 45 || number.Length > 0) && index < array.Length)
                        {
                            AddNumber(Convert.ToInt32(number), index);
                            index++;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void AddNumber(int number, int index)
        {
            array[index] = number;
        }

        public void ExtendAddNumber(int number, int index)
        {
            // Nie możemy wyjść poza rozmiar tablicy
            if (index < size)
            {
                Console.WriteLine("#### DODAJE ELEMENT ####\n");
                int[] newArray = new int[size + 1];

                // Wsadzamy element na miejsce [index]
                // [0 -> (index-1)] = stare elementy
                // [index] = nowy element
                // [(index+1) -> max] = stare elementy
                for (int i = 0; i < index; i++)
                {
                    newArray[i] = array[i];
                }

                newArray[index] = number;

                for (int i = index + 1; i < newArray.Length; i++)
                {
                    newArray[i] = array[i - 1];
                }

                // Przypisanie newArray do array
                array = newArray;
                size = array.Length;
            }
            else
            {
                Console.WriteLine("#### NIE MOZNA DODAC, NIEPOPRAWNY INDEX ####\n");
            }
        }

        public void DeleteNumber(int index)
        {
            if (index < array.Length)
            {
                Console.WriteLine("#### USUWAM ELEMENT " + array[index] + " Z MIEJSCA " + index + " ####\n");
                // [0 -> (index-1)] - i ---> i
                // [index -> max] - i ---> (i-1)
                int[] newArray = new int[size - 1];
                for (int i = 0; i < index; i++)
                {
                    newArray[i] = array[i];
                }
                for (int i = index; i < newArray.Length; i++)
                {
                    newArray[i] = array[i + 1];
                }
                // Przypisanie newArray do array
                array = newArray;
                size = array.Length;

            }
            else
            {
                Console.WriteLine("#### NIE MOZNA USUNAC, NIEPOPRAWNY INDEX ####\n");
            }

        }

        public void ListContents()
        {
            Console.WriteLine("#### WYPISYWANIE ####");
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine("["+i+"] = " + array[i]);
            }
            Console.WriteLine("#### KONIEC WYPISYWANIA ####\n");
            
        }
    }
}
