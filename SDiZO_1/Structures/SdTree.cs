using System;
using System.IO;

namespace SDiZO_1.Structures
{
    public class SdTree
    {
        private SdTreeNode root;
        public int Size { get; set; }

        public SdTree()
        {
            root = null;
            Size = 0;
        }

        // Dodawanie nowego węzła.
        // Porównujemy dane z obecnym węzłem.
        // Jeżeli jest mniejszy, przechodzimy do lewego dziecka.
        // Jeżeli jest większy/równy, przechodzimy do prawego dziecka.
        // Gdy nie ma dzieci, dodajemy go tam.
        public void Add(int number)
        {
            if (Size == 0)
            {
                root = new SdTreeNode(number);
                Size++;
            }
            else
            {
                bool done = false;
                SdTreeNode comparisonNode = root;
                SdTreeNode newNode = new SdTreeNode(number);
                while (!done)
                {
                    if (newNode.Data >= comparisonNode.Data)
                    {
                        // Przechodzimy do prawego dziecka.
                        if (comparisonNode.Right == null)
                        {
                            // Nie ma dziecka - węzeł nim zostaje.
                            comparisonNode.Right = newNode;
                            done = true;
                        }
                        else
                        {
                            // Jest dziecko - będziemy musieli porównać z nim nowy węzeł.
                            comparisonNode = comparisonNode.Right;
                        }
                    }
                    else
                    {
                        // Przeczhodzimy do lewego dziecka.
                        if (comparisonNode.Left == null)
                        {
                            // Nie ma dziecka - węzeł nim zostaje.
                            comparisonNode.Left = newNode;
                            done = true;
                        }
                        else
                        {
                            // Jest dziecko - będziemy musieli porównać z nim nowy węzeł.
                            comparisonNode = comparisonNode.Left;
                        }
                    }
                }
                Size++;

            }
        }

        // Wyszukiwanie elementu o zadanej wartości.
        // Zwraca węzeł lub null jeżeli taki element nie został znaleziony.
        public SdTreeNode FindByValue(int value)
        {
            if (Size > 0)
            {
                SdTreeNode currentNode = root;
                while (currentNode != null)
                {
                    // Jeżeli wartość się zgadza, węzeł został znaleziony.
                    if (currentNode.Data == value)
                    {
                        return currentNode;
                    }
                    else
                    {
                        // Jeżeli nie, sprawdź czy jest mniejszy od wartości obecnego węzła.
                        // Jeżeli jest mniejszy, przejdź do lewego dziecka.
                        // Jeżeli jest niemniejszy, przejdź do prawego dziecka.
                        if (value < currentNode.Data)
                        {
                            currentNode = currentNode.Left;
                        }
                        else
                        {
                            currentNode = currentNode.Right;
                        }
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        // Usuwanie.
        // TODO
        public void Delete(int value)
        {
            SdTreeNode nodeToDelete = FindByValue(value);
        }

        // Wypisywanie zawartości do pliku.
        public void SaveData()
        {
            using (StreamWriter sw = new StreamWriter("./Drzw_Zawartość.txt"))
            {
                if (Size > 0)
                {
                    sw.WriteLine("Górna gałąź - lewa strona; Dolna - prawa strona");
                    HeapForm(root, " ", sw);
                }
                else
                {
                    sw.WriteLine("Drzewo jest puste.");
                }

            }

        }

        private void HeapForm(SdTreeNode node, String prefix, StreamWriter sw)
        {
            
            if (node == null)
            {
                sw.WriteLine(prefix + "-- [brak]");
            }
            else
            {
                sw.WriteLine(prefix + "-- [" + node.Data + "]");
                HeapForm(node.Left, prefix + "|  ", sw);
                HeapForm(node.Right, prefix + "|  ", sw);

            }
        }
    }

    public class SdTreeNode
    {
        public int Data { get; set; }
        
        public SdTreeNode Left { get; set; }

        public SdTreeNode Right { get; set; }

        // Konstruktory
        public SdTreeNode(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
        public SdTreeNode()
        {
            Left = null;
            Right = null;
        }
    }
}