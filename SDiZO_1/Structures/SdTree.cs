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
        public void Insert(int number)
        {
            if (Size == 0)
            {
                root = new SdTreeNode(number);
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