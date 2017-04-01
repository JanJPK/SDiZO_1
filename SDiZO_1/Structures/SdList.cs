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
        private SdNode head = new SdNode();
        private SdNode tail = new SdNode();

        // Konstruktor.
        public SdList()
        {
            tail.Previous = head;
            head.Next = tail;
        }

        // Dodawanie węzła w wybrane miejsce.
        public void AddSdNodePlus(int number, int position)
        {
            if (position <= Size && position >= 0)
            {
                
                //Console.WriteLine("#### DODAWANIE " + number + " NA MIEJSCE " + position + " ####");
                SdNode newNode = new SdNode(number);
                // Szukamy węzła na pozycji [position] - zostanie on przesunięty w kierunku ogona i zastąpiony przez [newNode].
                SdNode movedNode = FindNode(position);
                // Łączenie nowego węzła z poprzednikiem
                (movedNode.Previous).Next = newNode;
                newNode.Previous = movedNode.Previous;

                // Łączenie węzła z węzłem przesuniętym
                newNode.Next = movedNode;
                movedNode.Previous = newNode;

                Size++;
            }
            else
            {
                //Console.WriteLine("#### NIE MOZNA DODAC, NIEPOPRAWNY INDEX ####\n");
            }
            
        }

        // Dodawanie węzła na koniec.
        public void AddSdNodeEnd(int number)
        {
            
            SdNode newNode = new SdNode(number);
            //Console.WriteLine("#### DODAWANIE " + number + " NA KONIEC LISTY ####\n");

            // Łączenie z poprzednim węzłem
            (tail.Previous).Next = newNode;
            newNode.Previous = (tail.Previous);

            // Łączenie z ogonem
            tail.Previous = newNode;
            newNode.Next = tail;

            Size++;
        }

        // Dodawanie węzła na początek.
        public void AddSdNodeBeg(int number)
        {
            SdNode newNode = new SdNode(number);
            //Console.WriteLine("#### DODAWANIE " + number + " NA POCZATEK LISTY ####\n");

            // Łączenie z następnym węzłem
            (head.Next).Previous = newNode;
            newNode.Next = head.Next;

            // Łączenie z głową
            head.Next = newNode;
            newNode.Previous = head;

            Size++;
        }
        
        // Usuwanie węzła z wybranego miejsca.
        public void RemoveSdNode(int position)
        {
            if (position <= Size)
            {

                //Console.WriteLine("#### USUWANIE Z POZYCJI "+ position + " ####");
                // Szukamy węzła na pozycji [position] - zostanie on usunięty.
                SdNode targetNode = FindNode(position);
                // Łączenie węzła poprzedniego i następnego dla [targetNode] ze sobą.
                (targetNode.Previous).Next = targetNode.Next;
                (targetNode.Next).Previous = targetNode.Previous;
                Size--;
            }
            else
            {
                //Console.WriteLine("#### NIE MOZNA DODAC, NIEPOPRAWNY INDEX ####\n");
            }
        }

        // Funkcja znajdująca węzeł na wybranej pozycji.
        // Jeżeli [cel] - [połowa rozmiaru] > 0, to szybciej tam dojdziemy od ogona.
        // W innym przypadku startujemy od głowy.
        public SdNode FindNode(int position)
        {
            SdNode targetNode;
            if (position - (Size / 2) > 0)
            {
                // Start od ogona
                // Console.WriteLine("### START OD OGONA\n");
                targetNode = tail.Previous;
                for (int i = 0; i < (Size - position); i++)
                {
                    targetNode = targetNode.Previous;
                }
            }
            else
            {
                // Start od głowy
                // Console.WriteLine("### START OD GLOWY\n");
                targetNode = head.Next;
                for (int i = 0; i < (position - 1); i++)
                {
                    targetNode = targetNode.Next;
                }
            }

            return targetNode;
        }

        // Wypisywanie zawartości listy.
        public void ListContents()
        {
            //Console.WriteLine("#### WYPISYWANIE ZAWARTOSCI LISTY ####");
            //Console.WriteLine("#### ROZMIAR = " + Size);
            //Console.WriteLine("## GLOWA");
            SdNode currentNode = head.Next;
            while (currentNode.Next != null)
            {
                //Console.WriteLine("# "+currentNode.Number);
                currentNode = currentNode.Next;
            }
            //Console.WriteLine("## OGON");
            //Console.WriteLine("### KONIEC WYPISYWANIA\n");
            
        }
    }

    // Węzeł listy.
    class SdNode
    {
        // Przechowywane dane
        public int Number { get; set; }

        // Następny węzeł
        public SdNode Next { get; set; }

        // Poprzedni węzeł
        public SdNode Previous { get; set; }

        // Konstruktory
        public SdNode(int number)
        {
            Number = number;
        }
        public SdNode()
        {
            
        }

    }
}
