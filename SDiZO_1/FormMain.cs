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

        // Tablica z której dodawane będą liczby do struktur.
        public int[] numberArray;
        public int arraySize;

        // Zmienne struktur i ich zegarów.
        private SdArray SdArray;
        private Clock SdArrayClock;
        private SdList SdList;
        private Clock SdListClock;
        private SdHeap SdHeap;
        private Clock SdHeapClock;
        private SdTree SdTree;
        private Clock SdTreeClock;

        // Ilość powtórzeń.
        private int cycles;

        // Zmienne potrzebne przy dodawaniu.
        private int addFrom, addTo, addAmount;

        // Generator liczb losowych.
        private Random rng = new Random();
        // rng.Next(min,max) zwraca wartości <min, max).

        // Konstruktor.
        public FormMain()
        {

            InitializeComponent();
            
            // Ustawianie domyślnych wartości dla interfejsu graficznego.
            radioButtonAddBeg.Checked = true;
            radioButtonDelBeg.Checked = true;

            checkBoxEnableArray.Checked = true;
            checkBoxEnableList.Checked = true;
            checkBoxEnableHeap.Checked = true;
            checkBoxEnableTree.Checked = true;

            textBoxActionMultiplier.Text = "1";
            textBoxAddAmount.Text = "100";
            textBoxAddFrom.Text = "0";
            textBoxAddTo.Text = "10";
            textBoxDelAmount.Text = "10";

            textBoxStatusArray.Text = "RDY";
            textBoxStatusList.Text = "RDY";
            textBoxStatusHeap.Text = "RDY";
            textBoxStatusTree.Text = "RDY";

            // Te pola zawierają nazwy plików do których będą zapisywane pomiary.
            textBoxArrayFilename.Text = "Tabl_Czas";
            textBoxListFilename.Text = "List_Czas";
            textBoxHeapFilename.Text = "Kopc_Czas";
            textBoxTreeFilename.Text = "Drzw_Czas";

            SdArray = new SdArray();
            SdList = new SdList();
            SdHeap = new SdHeap();
            SdTree = new SdTree();
            cycles = 1;
        }

        // Przyciski:
        // Dodawanie.
        private void buttonAddTarget_Click(object sender, EventArgs e)
        {
            // Sprawdzanie czy wprowadzone liczby są poprawne.
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
            // Sprawdzenie czy struktura nie jest wyłączona.
            if (checkBoxEnableArray.Checked)
            {
                AddSdArray();
            }
            if (checkBoxEnableList.Checked)
            {
                AddSdList();
            }
            if (checkBoxEnableHeap.Checked)
            {
                AddSdHeap();
            }
            if (checkBoxEnableTree.Checked)
            {
                AddSdTree();
            }

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

            if (checkBoxEnableArray.Checked)
            {
                DeleteSdArray(delAmount);
            }
            if (checkBoxEnableList.Checked)
            {
                DeleteSdList(delAmount);
            }
            if (checkBoxEnableHeap.Checked)
            {
                DeleteSdHeap(delAmount);
            }
            if (checkBoxEnableTree.Checked)
            {
                DeleteSdTree(delAmount);
            }

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
            SdArray = new SdArray();
            SdList = new SdList();
            SdHeap = new SdHeap();
            SdTree = new SdTree();
        }
        // Wyszukiwanie po wartości.
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(textBoxDelAmount.Text, out value))
            {
                MessageBox.Show("Nieprawidłowa liczba", "Błąd",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkBoxEnableArray.Checked)
            {
                SearchSdArray(value);
            }
            if (checkBoxEnableList.Checked)
            {
                SearchSdList(value);
            }
            if (checkBoxEnableHeap.Checked)
            {
                SearchSdHeap(value);
            }
            if (checkBoxEnableTree.Checked)
            {
                SearchSdTree(value);
            }
        }

        // Wypisywanie zawartości do pliku i wyświetlanie.
        // Jeżeli struktura istnieje - zapisz jej zawartość do pliku a następnie wyświetl.
        private void buttonArraySaveData_Click(object sender, EventArgs e)
        {
            if (SdArray != null)
            {
                SdArray.SaveData();
                var displayForm = new DisplayForm("Tabl_Zawartość");
                displayForm.Show();
            }
        }
        private void buttonListSaveData_Click(object sender, EventArgs e)
        {
            if (SdList != null)
            {
                SdList.SaveData();
                var displayForm = new DisplayForm("List_Zawartość");
                displayForm.Show();
            }
        }
        private void buttonHeapSaveData_Click(object sender, EventArgs e)
        {
            if (SdHeap != null)
            {
                SdHeap.SaveData();
                var displayForm = new DisplayForm("Kopc_Zawartość");
                displayForm.Show();
            }
        }
        private void buttonTreeSaveData_Click(object sender, EventArgs e)
        {
            if (SdTree != null)
            {
                SdTree.SaveData();
                var displayForm = new DisplayForm("Drzw_Zawartość");
                displayForm.Show();
            }
        }

        // Funkcje:
        // Dodawanie.
        private void AddSdArray()
        {
            // Tworzenie licznika czasu.
            SdArrayClock = new Clock(textBoxArrayFilename.Text, "ADDA X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text);
            textBoxStatusArray.Text = "WRK";

            // Dodawanie w losowe miejsce.
            if (radioButtonAddRng.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    FillNumberArray();
                    // Losowanie pozycji z zakresu [0, (obecny rozmiar tablicy)]
                    int position = rng.Next(0, SdArray.Size);

                    // Start pomiaru.
                    SdArrayClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdArray.Add(numberArray[i], position);
                    }
                    // Finish pomiaru i zapis.
                    SdArrayClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdArray = new SdArray();
                    }
                }
            }

            // Dodawanie na początek.
            if (radioButtonAddBeg.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    FillNumberArray();
                    // Start pomiaru.
                    SdArrayClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdArray.AddBeg(numberArray[i]);
                    }
                    // Finish pomiaru i zapis.
                    SdArrayClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdArray = new SdArray();
                    }
                }
            }

            // Dodawanie na koniec.
            if (radioButtonAddEnd.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    FillNumberArray();
                    // Start pomiaru.
                    SdArrayClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdArray.AddEnd(numberArray[i]);
                    }
                    // Finish pomiaru.
                    SdArrayClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdArray = new SdArray();
                    }
                }
            }

            // Dodawanie w zadane miejsce.
            if (radioButtonAddTarget.Checked)
            {
                // TODO
            }

            SdArrayClock.SaveLog(SdArrayClock.AverageTime());
            textBoxTimeArray.Text = SdArrayClock.AverageTime();
            textBoxStatusArray.Text = "RDY";
        }
        private void AddSdList()
        {   
            // Tworzenie licznika czasu.
            SdListClock = new Clock(textBoxListFilename.Text, "ADDL X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text);
            textBoxStatusList.Text = "WRK";

            // Dodawanie w losowe miejsce.
            if (radioButtonAddRng.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    FillNumberArray();
                    // Losowanie pozycji z zakresu [0, (obecny rozmiar listy-1)]
                    int position = rng.Next(0, SdList.Size);
                    
                    // Start pomiaru.
                    SdListClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdList.Add(numberArray[i], position);
                    }
                    // Finish pomiaru.
                    SdListClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdList = new SdList();
                    }
                }
            }

            // Dodawanie na początek.
            if (radioButtonAddBeg.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    FillNumberArray();
                    // Start pomiaru.
                    SdListClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdList.AddBeg(numberArray[i]);
                    }
                    // Finish pomiaru.
                    SdListClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdList = new SdList();
                    }
                }
            }

            // Dodawanie na koniec.
            if (radioButtonAddEnd.Checked)
            {
                for (int j = 0; j < cycles; j++)
                {
                    // Tworzenie nowej tablicy.
                    FillNumberArray();
                    // Start pomiaru.
                    SdListClock.Start();
                    for (int i = 0; i < arraySize; i++)
                    {
                        SdList.AddEnd(numberArray[i]);
                    }
                    // Finish pomiaru.
                    SdListClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                    {
                        SdList = new SdList();
                    }
                }
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
                SdList.Add(addTargetValue, addTargetPosition);
            }

            SdListClock.SaveLog(SdListClock.AverageTime());
            textBoxTimeList.Text = SdListClock.AverageTime();
            textBoxStatusList.Text = "RDY";

        }
        private void AddSdHeap()
        {
            SdHeapClock = new Clock(textBoxHeapFilename.Text, ("ADDH X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text));
            textBoxStatusHeap.Text = "WRK";

                for (int j = 0; j < cycles; j++)
                {
                    // Standardowe dodawanie.
                    FillNumberArray();
                    // Start pomiaru.
                    SdHeapClock.Start();
                    for (var i = 0; i < arraySize; i++)
                        SdHeap.Add(numberArray[i]);
                    // Finish pomiaru.
                    SdHeapClock.Finish();

                    // Czyszczenie struktury.
                    if (checkBoxReset.Checked)
                        SdHeap = new SdHeap();
                }

            SdHeapClock.SaveLog(SdHeapClock.AverageTime());
            textBoxTimeHeap.Text = SdHeapClock.AverageTime();
            textBoxStatusHeap.Text = "RDY";

        }
        private void AddSdTree()
        {
            SdTreeClock = new Clock(textBoxTreeFilename.Text, ("ADDT X " + textBoxAddFrom.Text + " X " + textBoxAddTo.Text + " X " + textBoxAddAmount.Text));
            textBoxStatusTree.Text = "WRK";


            // Standardowe dodawanie.
            for (var j = 0; j < cycles; j++)
            {
                // Tworzenie nowej tablicy.
                FillNumberArray();
                // Start pomiaru.
                SdTreeClock.Start();
                for (var i = 0; i < arraySize; i++)
                    SdTree.Add(numberArray[i]);
                // Finish pomiaru.
                SdTreeClock.Finish();

                // Czyszczenie struktury.
                if (checkBoxReset.Checked)
                    SdTree = new SdTree();
            }

            SdTreeClock.SaveLog(SdTreeClock.AverageTime());
            textBoxTimeTree.Text = SdTreeClock.AverageTime();
            textBoxStatusTree.Text = "RDY";
        }

        // Usuwanie.
        private void DeleteSdArray(int deleteAmount)
        {
            SdArrayClock = new Clock(textBoxArrayFilename.Text, "DELA X " + textBoxDelAmount.Text);
            textBoxStatusArray.Text = "WRK";

            // Usuwanie z losowego miejsca.
            if (radioButtonDelRng.Checked)
            {
                while (SdArray.Size >= deleteAmount && deleteAmount > 0)
                {
                    int position = rng.Next(0, SdArray.Size);
                    SdArrayClock.Start();
                    SdArray.Delete(position);
                    SdArrayClock.Stop();
                    deleteAmount--;
                }

            }

            // Usuwanie z początku.
            if (radioButtonDelBeg.Checked)
            {
                while (SdArray.Size >= deleteAmount && deleteAmount > 0)
                {
                    SdArrayClock.Start();
                    SdArray.Delete(0);
                    deleteAmount--;
                }
            }

            // Usuwanie z końca.
            if (radioButtonDelEnd.Checked)
            {
                while (SdArray.Size >= deleteAmount && deleteAmount > 0)
                {
                    int position = SdArray.Size - 1;
                    SdArrayClock.Start();
                    SdArray.Delete(position);
                    deleteAmount--;
                }
            }

            SdArrayClock.Finish();
            SdArrayClock.SaveLog(SdArrayClock.AverageTime());
            textBoxTimeArray.Text = SdArrayClock.AverageTime();
            textBoxStatusArray.Text = "RDY";
        }
        private void DeleteSdList(int deleteAmount)
        {
            SdListClock = new Clock(textBoxListFilename.Text, "DELL X " + textBoxDelAmount.Text);
            textBoxStatusList.Text = "WRK";

            // Usuwanie z losowego miejsca.
            if (radioButtonDelRng.Checked)
            {
                while (SdList.Size >= deleteAmount && deleteAmount > 0)
                {
                    int position = rng.Next(0, SdList.Size);
                    SdListClock.Start();
                    SdList.Delete(position);
                    SdListClock.Stop();
                    deleteAmount--;
                }

            }

            // Usuwanie z początku.
            if (radioButtonDelBeg.Checked)
            {
                while (SdList.Size >= deleteAmount && deleteAmount > 0)
                {
                    SdListClock.Start();
                    SdList.Delete(1);
                    SdListClock.Stop();
                    deleteAmount--;
                }
            }

            // Usuwanie z końca.
            if (radioButtonDelEnd.Checked)
            {
                while (SdList.Size >= deleteAmount && deleteAmount > 0)
                {
                    int position = SdList.Size;
                    SdListClock.Start();
                    SdList.Delete(position);
                    SdListClock.Stop();
                    deleteAmount--;
                }
            }

            SdListClock.Finish();
            SdListClock.SaveLog(SdListClock.AverageTime());
            textBoxTimeList.Text = SdListClock.AverageTime();
            textBoxStatusList.Text = "RDY";
        }
        private void DeleteSdHeap(int deleteAmount)
        {
            SdHeapClock = new Clock(textBoxHeapFilename.Text, "DELH X " + textBoxDelAmount.Text);
            textBoxStatusHeap.Text = "WRK";

            while (SdHeap.Size >= deleteAmount && deleteAmount > 0)
            {
                SdHeapClock.Start();
                SdHeap.Delete();
                SdHeapClock.Stop();
                deleteAmount--;
            }

            SdHeapClock.Finish();
            SdHeapClock.SaveLog(SdHeapClock.AverageTime());
            textBoxTimeHeap.Text = SdHeapClock.AverageTime();
            textBoxStatusHeap.Text = "RDY";
        }
        private void DeleteSdTree(int deleteAmount)
        {
            // TODO
        }

        // TODO
        // Wyszukiwanie.
        private void SearchSdArray(int value)
        {
            SdArrayClock = new Clock(textBoxArrayFilename.Text, "FNDA X " + textBoxSearchValue.Text);
            textBoxStatusArray.Text = "WRK";

            int resultIndex;
            SdArrayClock.Start();
            resultIndex = SdArray.FindByValue(value);
            SdArrayClock.Finish();

            if (resultIndex == -1)
            {
                textBoxSearchResultArray.Text = "N";
            }
            else
            {
                textBoxSearchResultArray.Text = "T";
            }
            
            textBoxTimeArray.Text = SdArrayClock.AverageTime();
            SdArrayClock.SaveLog(SdArrayClock.AverageTime());
            textBoxStatusArray.Text = "RDY";
        }
        private void SearchSdList(int value)
        {
            SdListClock = new Clock(textBoxListFilename.Text, "FNDL X " + textBoxSearchValue.Text);
            textBoxStatusList.Text = "WRK";

            SdListNode resultNode;
            SdListClock.Start();
            resultNode = SdList.FindByValue(value);
            SdListClock.Finish();

            if (resultNode.Data == value)
            {
                textBoxSearchResultList.Text = "Y";
            }
            else
            {
                textBoxSearchResultList.Text = "N";
            }

            textBoxTimeList.Text = SdListClock.AverageTime();
            SdListClock.SaveLog(SdListClock.AverageTime());
            textBoxStatusList.Text = "RDY";
        }
        private void SearchSdHeap(int value)
        {
            SdHeapClock = new Clock(textBoxHeapFilename.Text, "FNDH X " + textBoxSearchValue.Text);
            textBoxStatusHeap.Text = "WRK";

            int resultIndex;
            SdHeapClock.Start();
            resultIndex = SdHeap.FindByValue(value);
            SdHeapClock.Finish();

            if (resultIndex == -1)
            {
                textBoxSearchResultHeap.Text = "N";
            }
            else
            {
                textBoxSearchResultHeap.Text = "T";
            }

            textBoxTimeHeap.Text = SdHeapClock.AverageTime();
            SdHeapClock.SaveLog(SdHeapClock.AverageTime());
            textBoxStatusHeap.Text = "RDY";
        }
        private void SearchSdTree(int value)
        {
            SdTreeClock = new Clock(textBoxTreeFilename.Text, "FNDT X " + textBoxSearchValue.Text);
            textBoxStatusArray.Text = "WRK";
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
                // TODO
                //Console.WriteLine(e);
                throw;
            }
        }
        
        // Tworzenie tablicy z liczbami.
        private void FillNumberArray()
        {
            if (checkBoxAddFromFile.Checked)
            {
                // Jeżeli zaznaczony jest checkBox "wczytywanie pliku"
                // Wczytaj dane z pliku do tablicy.
                ReadFile("Input");
                // Ustaw cykle na 1, nie ma potrzeby wczytywać kilka razy tego samego.
                cycles = 1;
                textBoxActionMultiplier.Text = "1";
            }
            else
            {
                // Stwórz tablicę losowych liczb.
                numberArray = DataGenerator.NewArray(addFrom, addTo, addAmount);
            }
            // Dopasuj wielkość.
            arraySize = numberArray.Length;
        }
    }
}
