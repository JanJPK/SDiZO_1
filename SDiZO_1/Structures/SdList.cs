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
        public void AddNode(int number, int position)
        {
            if (position <= Size && position >= 0)
            {
                
                SdListNode newListNode = new SdListNode(number);
                // Szukamy węzła na pozycji [position] - zostanie on przesunięty w kierunku ogona i zastąpiony przez [newListNode].
                SdListNode movedListNode = FindNode(position);
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
        public void AddNodeEnd(int data)
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
        public void AddNodeBeg(int number)
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
        public void RemoveNode(int position)
        {
            if (position <= Size)
            {
                
                // Szukamy węzła na pozycji [position] - zostanie on usunięty.
                SdListNode targetListNode = FindNode(position);
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
        public SdListNode FindNode(int position)
        {
            SdListNode targetListNode = null;
            if (position - (Size / 2) > 0)
            {
                // Start od ogona
                targetListNode = tail.Previous;
                for (int i = 0; i < (Size - position); i++)
                {
                    targetListNode = targetListNode.Previous;
                }
            }
            else
            {
                // Start od głowy
                targetListNode = head.Next;
                for (int i = 0; i < (position - 1); i++)
                {
                    targetListNode = targetListNode.Next;
                }
            }

            return targetListNode;
        }

        // Wypisywanie zawartości listy.
        public void ListContents()
        {

            SdListNode currentListNode = head.Next;
            while (currentListNode.Next != null)
            {
                currentListNode = currentListNode.Next;
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
