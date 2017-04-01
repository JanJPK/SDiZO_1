using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDiZO_1.Structures;
using System.Windows.Forms;

namespace SDiZO_1
{
    public class Program
    {
        public static string SavePath = @".\";
        public static int[] initialArray;
        public static int arraySize;
        private static SdArray SdA;
        private static SdList SdL;
        private static SdHeap SdH;

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

        }
        
    
    }
}
