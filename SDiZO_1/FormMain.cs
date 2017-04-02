using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDiZO_1.Structures;
using SDiZO_1.Tools;

namespace SDiZO_1
{
    public partial class FormMain : Form
    {
        public string SavePath = @".\";
        public int[] numberArray;
        public int arraySize;
        private SdArray SdA;
        private Clock SdAClock;
        private SdList SdL;
        private Clock SdLClock;
        private SdHeap SdH;
        private Clock SdHClock;
        private int cycles;
        private Random rng = new Random();

        // Konstruktor.
        public FormMain()
        {

            InitializeComponent();
            radioButtonAddBeg.Checked = true;
            radioButtonDelBeg.Checked = true;
            radioButtonSearchIndex.Checked = true;

            textBoxActionMultiplier.Text = "1";
            textBoxAddAmount.Text = "1";
            textBoxAddFrom.Text = "0";
            textBoxAddTo.Text = "10";
            textBoxDelAmount.Text = "1";

            textBoxStatusArray.Text = "RDY";
            textBoxStatusList.Text = "RDY";
            textBoxStatusHeap.Text = "RDY";

            textBoxArrayFilename.Text = "Array";
            textBoxListFilename.Text = "List";
            textBoxHeapFilename.Text = "Heap";

            SdA = new SdArray();
            SdL = new SdList();
            SdH = new SdHeap();
            cycles = Convert.ToInt32(textBoxActionMultiplier.Text);
        }

        // Przyciski:
        // Dodawanie.
        private void buttonAddTarget_Click(object sender, EventArgs e)
        {
            AddSdArray();
            AddSdList();
            AddSdHeap();
        }
        // Usuwanie.
        private void buttonDel_Click(object sender, EventArgs e)
        {
            DelSdArray(Convert.ToInt32(textBoxDelAmount.Text));
            DelSdList(Convert.ToInt32(textBoxDelAmount.Text));
            DelSdHeap(Convert.ToInt32(textBoxDelAmount.Text));
        }
        // Ustawianie mnożnika operacji - każda operacja np. dodawanie zostanie powtórzona tyle razy ile wynosi parametr [cycles].
        private void buttonActionMultiplier_Click(object sender, EventArgs e)
        {
            cycles = Convert.ToInt32(textBoxActionMultiplier.Text);
        }
        // Retowanie struktur.
        private void buttonReset_Click(object sender, EventArgs e)
        {
            // Reset struktur do stanu pustego.
            SdA = new SdArray();
            SdL = new SdList();
            SdH = new SdHeap();
        }


        // Funkcje:
        // Dodawanie.
        private void AddSdArray()
        {
            // Tworzenie licznika czasu.
            SdAClock = new Clock(textBoxArrayFilename.Text, "ADDA X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text);
            textBoxStatusArray.Text = "WRK";

            // Dodawanie w losowe miejsce.
            if (radioButtonAddRng.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    numberArray = DataGenerator.NewArray(Convert.ToInt32(textBoxAddFrom.Text),
                        Convert.ToInt32(textBoxAddTo.Text), Convert.ToInt32(textBoxAddAmount.Text));
                    // Losowanie pozycji z zakresu [0, (obecny rozmiar tablicy)]
                    int position = rng.Next(0, SdA.Size-1);

                    // Start pomiaru.
                    SdAClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdA.AddNumberPlus(numberArray[i], position);
                    }
                    // Finish pomiaru i zapis.
                    SdAClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdA = new SdArray();
                    }
                }
            }

            // Dodawanie na początek.
            if (radioButtonAddBeg.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    numberArray = DataGenerator.NewArray(Convert.ToInt32(textBoxAddFrom.Text),
                        Convert.ToInt32(textBoxAddTo.Text), Convert.ToInt32(textBoxAddAmount.Text));
                    // Start pomiaru.
                    SdAClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdA.AddNumberBeg(numberArray[i]);
                    }
                    // Finish pomiaru i zapis.
                    SdAClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdA = new SdArray();
                    }
                }
            }

            // Dodawanie na koniec.
            if (radioButtonAddEnd.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    numberArray = DataGenerator.NewArray(Convert.ToInt32(textBoxAddFrom.Text),
                        Convert.ToInt32(textBoxAddTo.Text), Convert.ToInt32(textBoxAddAmount.Text));
                    // Start pomiaru.
                    SdAClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdA.AddNumberEnd(numberArray[i]);
                    }
                    // Finish pomiaru.
                    SdAClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdA = new SdArray();
                    }
                }
            }

            SdAClock.SaveLog(SdAClock.AverageTime());
            textBoxTimeArray.Text = SdAClock.AverageTime();
            textBoxStatusArray.Text = "RDY";
        }
        private void AddSdList()
        {   
            // Tworzenie licznika czasu.
            SdLClock = new Clock(textBoxListFilename.Text, "ADDL X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text);
            textBoxStatusList.Text = "WRK";

            // Dodawanie w losowe miejsce.
            if (radioButtonAddRng.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    numberArray = DataGenerator.NewArray(Convert.ToInt32(textBoxAddFrom.Text),
                        Convert.ToInt32(textBoxAddTo.Text), Convert.ToInt32(textBoxAddAmount.Text));
                    // Losowanie pozycji z zakresu [0, (obecny rozmiar listy-1)]
                    int position = rng.Next(0, SdL.Size);
                    
                    // Start pomiaru.
                    SdLClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdL.AddSdNodePlus(numberArray[i], position);
                    }
                    // Finish pomiaru.
                    SdLClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdL = new SdList();
                    }
                }
            }

            // Dodawanie na początek.
            if (radioButtonAddBeg.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    numberArray = DataGenerator.NewArray(Convert.ToInt32(textBoxAddFrom.Text),
                        Convert.ToInt32(textBoxAddTo.Text), Convert.ToInt32(textBoxAddAmount.Text));

                    // Start pomiaru.
                    SdLClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdL.AddSdNodeBeg(numberArray[i]);
                    }
                    // Finish pomiaru.
                    SdLClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdL = new SdList();
                    }
                }
            }

            // Dodawanie na koniec.
            if (radioButtonAddEnd.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    numberArray = DataGenerator.NewArray(Convert.ToInt32(textBoxAddFrom.Text),
                        Convert.ToInt32(textBoxAddTo.Text), Convert.ToInt32(textBoxAddAmount.Text));

                    // Start pomiaru.
                    SdLClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdL.AddSdNodeEnd(numberArray[i]);
                    }
                    // Finish pomiaru.
                    SdLClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdL = new SdList();
                    }
                }
            }

            SdLClock.SaveLog(SdLClock.AverageTime());
            textBoxTimeList.Text = SdLClock.AverageTime();
            textBoxStatusList.Text = "RDY";

        }
        private void AddSdHeap()
        {
            SdHClock = new Clock(textBoxHeapFilename.Text, ("ADDH X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text));
            textBoxStatusHeap.Text = "WRK";

            for (int j = 0; j < cycles; j++)
            {
                numberArray = DataGenerator.NewArray(Convert.ToInt32(textBoxAddFrom.Text),
                    Convert.ToInt32(textBoxAddTo.Text), Convert.ToInt32(textBoxAddAmount.Text));

                // Start pomiaru.
                SdHClock.Start();
                for (int i = 0; i < arraySize; i++)
                {
                    SdH.Insert(numberArray[i]);
                }
                // Finish pomiaru.
                SdHClock.Finish();

                // Czyszczenie struktury.
                if (checkBoxReset.Checked)
                {
                    SdH = new SdHeap();
                }
            }

            SdHClock.SaveLog(SdHClock.AverageTime());
            textBoxTimeHeap.Text = SdHClock.AverageTime();
            textBoxStatusHeap.Text = "RDY";

        }
        // Usuwanie.
        private void DelSdArray(int deleteAmount)
        {
            SdAClock = new Clock(textBoxArrayFilename.Text, "DELA X " + textBoxDelAmount.Text);
            textBoxStatusArray.Text = "WRK";

            // Usuwanie z losowego miejsca.
            if (radioButtonDelRng.Checked)
            {
                while (SdA.Size >= deleteAmount)
                {
                    int position = rng.Next(0, SdA.Size-1);
                    SdAClock.Start();
                    SdA.DeleteNumber(position);
                    SdAClock.Stop();
                    deleteAmount--;
                }

            }

            // Usuwanie z początku.
            if (radioButtonDelBeg.Checked)
            {
                while (SdA.Size >= deleteAmount)
                {
                    SdAClock.Start();
                    SdA.DeleteNumber(0);
                }
            }

            // Usuwanie z końca.
            if (radioButtonDelEnd.Checked)
            {
                while (SdA.Size >= deleteAmount)
                {
                    int position = SdA.Size - 1;
                    SdAClock.Start();
                    SdA.DeleteNumber(position);
                }
            }

            SdAClock.Finish();
            SdAClock.SaveLog(SdAClock.AverageTime());
            textBoxTimeArray.Text = SdAClock.AverageTime();
            textBoxStatusArray.Text = "RDY";
        }
        private void DelSdList(int deleteAmount)
        {
            
        }
        private void DelSdHeap(int deleteAmount)
        {
            
        }

        // Wczytywanie pliku.
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
                        // Tworzy liczbę. 
                        while (symbol > 47 && symbol < 58)
                        {
                            number = number + (char)symbol;
                            symbol = sr.Read();
                        }
                        // Dodaje liczbę.
                        // Jeżeli string wczytanych znaków jest większy od 1 i ma z przodu minusa lub jest większy od 0 - jest to liczba.
                        if ((number.Length > 1 && number[0] == 45) || number.Length > 0)
                        {
                            // Jeżeli tablica nie została utworzona, stwórz ją o wielkości pierwszej liczby.
                            if (numberArray == null)
                            {
                                // Jeżeli pierwsza liczba jest ujemna - utwórz tablicę o wielkości jej wartości bezwzględnej.
                                if (number[0] == 45)
                                {
                                    numberArray = new int[Convert.ToInt32(number) * -1];

                                }
                                else
                                {
                                    numberArray = new int[Convert.ToInt32(number)];
                                }

                            }
                            else
                            {
                                if (index < numberArray.Length)
                                {
                                    numberArray[index] = Convert.ToInt32(number);
                                    index++;
                                }
                            }
                        }
                    }
                    arraySize = numberArray.Length;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void ListContents()
        {
            Console.WriteLine("#### PROGRAM WCZYTAL NASTEPUJACE LICZBY ####");
            for (int i = 0; i < numberArray.Length; i++)
            {
                Console.WriteLine("[" + i + "] = " + numberArray[i]);
            }
            Console.WriteLine("#### KONIEC WYPISYWANIA ####\n");

        }


    }
}
