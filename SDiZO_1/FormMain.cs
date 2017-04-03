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
        private SdTree SdT;
        private Clock SdTClock;
        private int cycles;
        private Random rng = new Random();
        // rng.Next(min,max) zwraca wartości <min, max).

        // Konstruktor.
        public FormMain()
        {

            InitializeComponent();
            radioButtonAddBeg.Checked = true;
            radioButtonDelBeg.Checked = true;

            textBoxActionMultiplier.Text = "1";
            textBoxAddAmount.Text = "100";
            textBoxAddFrom.Text = "0";
            textBoxAddTo.Text = "10";
            textBoxDelAmount.Text = "10";

            textBoxStatusArray.Text = "RDY";
            textBoxStatusList.Text = "RDY";
            textBoxStatusHeap.Text = "RDY";
            textBoxStatusTree.Text = "RDY";

            textBoxArrayFilename.Text = "Tabl_Czas";
            textBoxListFilename.Text = "List_Czas";
            textBoxHeapFilename.Text = "Kopc_Czas";
            textBoxTreeFilename.Text = "Drzw_Czas";

            SdA = new SdArray();
            SdL = new SdList();
            SdH = new SdHeap();
            SdT = new SdTree();
            cycles = 1;
        }

        // Przyciski:
        // Dodawanie.
        private void buttonAddTarget_Click(object sender, EventArgs e)
        {
            // Sprawdzanie czy wprowadzone liczby są poprawne.
            int addFrom, addTo, addAmount;
            if (!int.TryParse(textBoxAddFrom.Text, out addFrom))
            {
                MessageBox.Show("Nieprawidłowa liczba", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxAddTo.Text, out addTo))
            {
                MessageBox.Show("Nieprawidłowa liczba", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBoxAddAmount.Text, out addAmount))
            {
                MessageBox.Show("Nieprawidłowa liczba", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (addAmount < 1)
            {
                MessageBox.Show("Nieprawidłowa ilość", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Wywoływanie funkcji każdej struktury.
            AddSdArray(addFrom, addTo, addAmount);
            AddSdList(addFrom, addTo, addAmount);
            AddSdHeap(addFrom, addTo, addAmount);
            AddSdTree(addFrom, addTo, addAmount);

            // Przywracanie tablicy do null.
            numberArray = null;

            MessageBox.Show("Dodawanie zakończone", "Informacja",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Usuwanie.
        private void buttonDel_Click(object sender, EventArgs e)
        {
            // Sprawdzanie czy wprowadzone liczby są poprawne.
            int delAmount;
            if (!int.TryParse(textBoxDelAmount.Text, out delAmount))
            {
                MessageBox.Show("Nieprawidłowa liczba", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (delAmount < 1)
            {
                MessageBox.Show("Nieprawidłowa ilość", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DeleteSdArray(delAmount);
            DeleteSdList(delAmount);
            DeleteSdHeap(delAmount);

            MessageBox.Show("Usuwanie zakończone", "Informacja",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Ustawianie mnożnika operacji - każda operacja np. dodawanie zostanie powtórzona tyle razy ile wynosi parametr [cycles].
        private void buttonActionMultiplier_Click(object sender, EventArgs e)
        {
            // Sprawdzanie czy wprowadzone liczby są poprawne.
            int cyclesAmount;
            if (!int.TryParse(textBoxActionMultiplier.Text, out cyclesAmount))
            {
                MessageBox.Show("Nieprawidłowa liczba", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cyclesAmount < 1)
            {
                MessageBox.Show("Nieprawidłowa ilość", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cycles = cyclesAmount;
            MessageBox.Show("Modyfikator poprawnie zmieniony", "Informacja",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Resetowanie struktur.
        private void buttonReset_Click(object sender, EventArgs e)
        {
            // Reset struktur do stanu pustego.
            MessageBox.Show("Struktury wyzerowane", "Informacja",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            SdA = new SdArray();
            SdL = new SdList();
            SdH = new SdHeap();
            SdT = new SdTree();
        }

        // Wypisywanie zawartości do pliku i wyświetlanie.
        // Jeżeli struktura istnieje - zapisz jej zawartość do pliku a następnie wyświetl.
        private void buttonArraySaveData_Click(object sender, EventArgs e)
        {
            if (SdA != null)
            {
                SdA.SaveData();
                var displayForm = new DisplayForm("Tabl_Zawartość");
                displayForm.Show();
            }
        }
        private void buttonListSaveData_Click(object sender, EventArgs e)
        {
            if (SdL != null)
            {
                SdL.SaveData();
                var displayForm = new DisplayForm("List_Zawartość");
                displayForm.Show();
            }
        }
        private void buttonHeapSaveData_Click(object sender, EventArgs e)
        {
            if (SdH != null)
            {
                SdH.SaveData();
                var displayForm = new DisplayForm("Kopc_Zawartość");
                displayForm.Show();
            }
        }
        private void buttonTreeSaveData_Click(object sender, EventArgs e)
        {
            if (SdT != null)
            {
                SdT.SaveData();
                var displayForm = new DisplayForm("Drzw_Zawartość");
                displayForm.Show();
            }
        }

        // Funkcje:
        // Dodawanie.
        private void AddSdArray(int addFrom, int addTo, int addAmount)
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
                    numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
                    arraySize = numberArray.Length;
                    // Losowanie pozycji z zakresu [0, (obecny rozmiar tablicy)]
                    int position = rng.Next(0, SdA.Size);

                    // Start pomiaru.
                    SdAClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdA.Add(numberArray[i], position);
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
                    numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
                    arraySize = numberArray.Length;
                    // Start pomiaru.
                    SdAClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdA.AddBeg(numberArray[i]);
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
                    numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
                    arraySize = numberArray.Length;
                    // Start pomiaru.
                    SdAClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdA.AddEnd(numberArray[i]);
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

            // Dodawanie z pliku.
            if (radioButtonAddFromFile.Checked)
            {
                ReadFile("Input");
                // Start pomiaru.
                SdAClock.Start();
                for (var i = 0; i < arraySize; i++)
                {
                    SdA.AddEnd(numberArray[i]);
                }
                // Finish pomiaru i zapis.
                SdAClock.Finish();

            }

            // Dodawanie w zadane miejsce.
            if (radioButtonAddTarget.Checked)
            {

            }

            SdAClock.SaveLog(SdAClock.AverageTime());
            textBoxTimeArray.Text = SdAClock.AverageTime();
            textBoxStatusArray.Text = "RDY";
        }
        private void AddSdList(int addFrom, int addTo, int addAmount)
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
                    numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
                    arraySize = numberArray.Length;
                    // Losowanie pozycji z zakresu [0, (obecny rozmiar listy-1)]
                    int position = rng.Next(0, SdL.Size);
                    
                    // Start pomiaru.
                    SdLClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdL.Add(numberArray[i], position);
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
                    numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
                    arraySize = numberArray.Length;
                    // Start pomiaru.
                    SdLClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdL.AddBeg(numberArray[i]);
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
                    numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
                    arraySize = numberArray.Length;
                    // Start pomiaru.
                    SdLClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdL.AddEnd(numberArray[i]);
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

            // Dodawanie z pliku.
            if (radioButtonAddFromFile.Checked)
            {
                ReadFile("Input");
                // Start pomiaru.
                SdLClock.Start();
                for (var i = 0; i < arraySize; i++)
                {
                    SdL.AddEnd(numberArray[i]);
                }
                // Finish pomiaru i zapis.
                SdHClock.Finish();
            }

            // Dodawanie w zadane miejsce.
            if (radioButtonAddTarget.Checked)
            {
                int addTargetValue, addTargetPosition;
                if (!int.TryParse(textBoxAddTargetValue.Text, out addTargetValue))
                {
                    MessageBox.Show("Nieprawidłowa liczba", "Błąd",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(textBoxAddTargetPosition.Text, out addTargetPosition))
                {
                    MessageBox.Show("Nieprawidłowa liczba", "Błąd",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SdL.Add(addTargetValue, addTargetPosition);
            }

            SdLClock.SaveLog(SdLClock.AverageTime());
            textBoxTimeList.Text = SdLClock.AverageTime();
            textBoxStatusList.Text = "RDY";

        }
        private void AddSdHeap(int addFrom, int addTo, int addAmount)
        {
            SdHClock = new Clock(textBoxHeapFilename.Text, ("ADDH X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text));
            textBoxStatusHeap.Text = "WRK";

            if (radioButtonAddFromFile.Checked)
            {
                // Dodawanie z pliku.
                // Start pomiaru.
                ReadFile("Input");
                SdHClock.Start();
                for (int i = 0; i < arraySize; i++)
                {
                    SdH.Add(numberArray[i]);
                }
                // Finish pomiaru.
                SdHClock.Finish();
            }
            else
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Standardowe dodawanie.
                    numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
                    arraySize = numberArray.Length;
                    // Start pomiaru.
                    SdHClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdH.Add(numberArray[i]);
                    }
                    // Finish pomiaru.
                    SdHClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdH = new SdHeap();
                    }
                }
            }

            SdHClock.SaveLog(SdHClock.AverageTime());
            textBoxTimeHeap.Text = SdHClock.AverageTime();
            textBoxStatusHeap.Text = "RDY";

        }
        private void AddSdTree(int addFrom, int addTo, int addAmount)
        {
            SdTClock = new Clock(textBoxTreeFilename.Text, ("ADDT X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text));
            textBoxStatusTree.Text = "WRK";

            if (radioButtonAddFromFile.Checked)
            {

            }
            else
            {
                if (radioButtonAddTarget.Checked)
                {

                }
                else
                {
                    // Standardowe dodawanie.
                    for (int j = 0; j < cycles; j++)
                    {
                        numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
                        arraySize = numberArray.Length;
                        // Start pomiaru.
                        SdTClock.Start();
                        for (int i = 0; i < arraySize; i++)
                        {
                            SdT.Add(numberArray[i]);
                        }
                        // Finish pomiaru.
                        SdTClock.Finish();

                        // Czyszczenie struktury.
                        if (checkBoxReset.Checked)
                        {
                            SdT = new SdTree();
                        }
                    }
                }
            }
            

            SdTClock.SaveLog(SdTClock.AverageTime());
            textBoxTimeTree.Text = SdTClock.AverageTime();
            textBoxStatusTree.Text = "RDY";
        }

        // Usuwanie.
        private void DeleteSdArray(int deleteAmount)
        {
            SdAClock = new Clock(textBoxArrayFilename.Text, "DELA X " + textBoxDelAmount.Text);
            textBoxStatusArray.Text = "WRK";

            // Usuwanie z losowego miejsca.
            if (radioButtonDelRng.Checked)
            {
                while (SdA.Size >= deleteAmount && deleteAmount > 0)
                {
                    int position = rng.Next(0, SdA.Size);
                    SdAClock.Start();
                    SdA.Delete(position);
                    SdAClock.Stop();
                    deleteAmount--;
                }

            }

            // Usuwanie z początku.
            if (radioButtonDelBeg.Checked)
            {
                while (SdA.Size >= deleteAmount && deleteAmount > 0)
                {
                    SdAClock.Start();
                    SdA.Delete(0);
                    deleteAmount--;
                }
            }

            // Usuwanie z końca.
            if (radioButtonDelEnd.Checked)
            {
                while (SdA.Size >= deleteAmount && deleteAmount > 0)
                {
                    int position = SdA.Size - 1;
                    SdAClock.Start();
                    SdA.Delete(position);
                    deleteAmount--;
                }
            }

            SdAClock.Finish();
            SdAClock.SaveLog(SdAClock.AverageTime());
            textBoxTimeArray.Text = SdAClock.AverageTime();
            textBoxStatusArray.Text = "RDY";
        }
        private void DeleteSdList(int deleteAmount)
        {
            SdAClock = new Clock(textBoxListFilename.Text, "DELA X " + textBoxDelAmount.Text);
            textBoxStatusList.Text = "WRK";

            // Usuwanie z losowego miejsca.
            if (radioButtonDelRng.Checked)
            {
                while (SdL.Size >= deleteAmount && deleteAmount > 0)
                {
                    int position = rng.Next(0, SdL.Size);
                    SdLClock.Start();
                    SdL.Delete(position);
                    SdLClock.Stop();
                    deleteAmount--;
                }

            }

            // Usuwanie z początku.
            if (radioButtonDelBeg.Checked)
            {
                while (SdL.Size >= deleteAmount && deleteAmount > 0)
                {
                    SdLClock.Start();
                    SdL.Delete(1);
                    SdLClock.Stop();
                    deleteAmount--;
                }
            }

            // Usuwanie z końca.
            if (radioButtonDelEnd.Checked)
            {
                while (SdL.Size >= deleteAmount && deleteAmount > 0)
                {
                    int position = SdL.Size;
                    SdLClock.Start();
                    SdL.Delete(position);
                    SdLClock.Stop();
                    deleteAmount--;
                }
            }

            SdLClock.Finish();
            SdLClock.SaveLog(SdLClock.AverageTime());
            textBoxTimeList.Text = SdLClock.AverageTime();
            textBoxStatusList.Text = "RDY";
        }
        private void DeleteSdHeap(int deleteAmount)
        {
            SdHClock = new Clock(textBoxHeapFilename.Text, "DELA X " + textBoxDelAmount.Text);
            textBoxStatusHeap.Text = "WRK";

            while (SdH.Size >= deleteAmount && deleteAmount > 0)
            {
                SdHClock.Start();
                SdH.Delete();
                SdHClock.Stop();
                deleteAmount--;
            }

            SdHClock.Finish();
            SdHClock.SaveLog(SdHClock.AverageTime());
            textBoxTimeHeap.Text = SdHClock.AverageTime();
            textBoxStatusHeap.Text = "RDY";
        }
        private void DeleteSdTree(int deleteAmount)
        {
            
        }

        // Wyszukiwanie.
        private void SearchSdArray()
        {
            
        }
        private void SearchSdList()
        {
            
        }
        private void SearchSdHeap()
        {
            
        }
        private void SearchSdTree()
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
                using (StreamReader sr = new StreamReader(@".\" + filename+".txt"))
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
        
    }
}
