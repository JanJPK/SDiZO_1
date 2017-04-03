using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDiZO_1.Structures
{
    class SdList
    {
        // Property i zmienne.
        public int Size { get; set; }
        private SdListNode head = new SdListNode();
        private SdListNode tail = new SdListNode();

        // Konstruktor.
        public SdList()
        {
            tail.Previous = head;
            head.Next = tail;
        }

        // Dodawanie węzła w wybrane miejsce.
        public void Add(int number, int index)
        {
            if (index <= Size && index >= 0)
            {
                
                SdListNode newListNode = new SdListNode(number);
                // Szukamy węzła na pozycji [index] - zostanie on przesunięty w kierunku ogona i zastąpiony przez [newListNode].
                SdListNode movedListNode = FindByIndex(index);
                // Łączenie nowego węzła z poprzednikiem
                (movedListNode.Previous).Next = newListNode;
                newListNode.Previous = movedListNode.Previous;

                // Łączenie węzła z węzłem przesuniętym
                newListNode.Next = movedListNode;
                movedListNode.Previous = newListNode;

                Size++;
            }
            else
            {

            }
            
        }

        // Dodawanie węzła na koniec.
        public void AddEnd(int data)
        {
            
            SdListNode newListNode = new SdListNode(data);

            // Łączenie z poprzednim węzłem
            (tail.Previous).Next = newListNode;
            newListNode.Previous = (tail.Previous);

            // Łączenie z ogonem
            tail.Previous = newListNode;
            newListNode.Next = tail;

            Size++;
        }

        // Dodawanie węzła na początek.
        public void AddBeg(int number)
        {
            SdListNode newListNode = new SdListNode(number);

            // Łączenie z następnym węzłem
            (head.Next).Previous = newListNode;
            newListNode.Next = head.Next;

            // Łączenie z głową
            head.Next = newListNode;
            newListNode.Previous = head;

            Size++;
        }
        
        // Usuwanie węzła z wybranego miejsca.
        public void Delete(int index)
        {
            if (index <= Size)
            {
                
                // Szukamy węzła na pozycji [index] - zostanie on usunięty.
                SdListNode targetListNode = FindByIndex(index);
                // Łączenie węzła poprzedniego i następnego dla [targetListNode] ze sobą.
                (targetListNode.Previous).Next = targetListNode.Next;
                (targetListNode.Next).Previous = targetListNode.Previous;
                Size--;
            }
            else
            {

            }
        }

        // Funkcja znajdująca węzeł na wybranej pozycji.
        // Jeżeli [cel] - [połowa rozmiaru] > 0, to szybciej tam dojdziemy od ogona.
        // W innym przypadku startujemy od głowy.
        public SdListNode FindByIndex(int index)
        {
            SdListNode targetListNode;
            if (index - (Size / 2) > 0)
            {
                // Start od ogona
                targetListNode = tail.Previous;
                for (int i = 0; i < (Size - index); i++)
                {
                    targetListNode = targetListNode.Previous;
                }
            }
            else
            {
                // Start od głowy
                targetListNode = head.Next;
                for (int i = 0; i < (index - 1); i++)
                {
                    targetListNode = targetListNode.Next;
                }
            }

            return targetListNode;
        }

        // Wyszukiwanie elementu o zadanej wartości.
        // Zwraca indeks elementu lub null jeżeli taki element nie został znaleziony.
        public SdListNode FindByValue(int value)
        {
            SdListNode currentNode = head;
            for (int i = 0; i < Size; i++)
            {
                currentNode = currentNode.Next;
                if (currentNode.Data == value)
                {
                    return currentNode;
                }
            }

            return null;
        }

        // Wypisywanie zawartości listy.
        public void SaveData()
        {
            using (StreamWriter sw = new StreamWriter("./List_Zawartość.txt"))
            {
                if (Size > 0)
                {
                    sw.WriteLine("HEAD");
                    SdListNode currentListNode = head.Next;
                    while (currentListNode.Next != null)
                    {
                        sw.WriteLine(currentListNode.Data);
                        currentListNode = currentListNode.Next;
                    }
                    sw.WriteLine("TAIL");
                }
                else
                {
                    sw.WriteLine("Lista jest pusta.");
                }
                
            }

        }

    }

    // Węzeł listy.
    class SdListNode
    {
        // Przechowywane dane
        public int Data { get; set; }

        // Następny węzeł
        public SdListNode Next { get; set; }

        // Poprzedni węzeł
        public SdListNode Previous { get; set; }

        // Konstruktory
        public SdListNode(int data)
        {
            Data = data;
        }
        public SdListNode()
        {
            
        }

    }
}
