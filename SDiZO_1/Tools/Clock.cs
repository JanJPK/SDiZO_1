using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDiZO_1.Tools
{ 

    class Clock
    {
        // Property i zmienne.
        public string Filename { get; set; }
        private Stopwatch sw;
        private List<double> timeList;
        private double ticks;
        
        // Konstruktor.
        public Clock(string filename, string sampleSize)
        {
            sw = new Stopwatch();
            Filename = filename;
            timeList = new List<double>();
            SaveProgress(sampleSize);
            SaveLog(sampleSize);
        }

        // Reset i start nowego pomiaru.
        public void Start()
        {
            sw.Reset();
            sw.Start();
        }

        // Stop i zapis do pliku.
        public void Stop()
        {
            sw.Stop();
            ticks = sw.ElapsedTicks;
            timeList.Add((ticks / Stopwatch.Frequency) * 1000000000);
            SaveProgress(((ticks / Stopwatch.Frequency)* 1000000000).ToString());
            /*
            timeList.Add(sw.Elapsed.TotalMilliseconds);
            SaveProgress(sw.Elapsed.TotalMilliseconds.ToString());
            */
        }

        // Zwraca obecny pomiar.
        public string LastRun()
        {
            ticks = sw.ElapsedTicks;
            return ((ticks / Stopwatch.Frequency) * 1000000000).ToString();
            //return AverageTime();
        }

        // Zapis do pliku.
        private void SaveProgress(string text)
        {
            using (StreamWriter sw = new StreamWriter(@"./" + Filename + ".txt",true))
            {
                sw.WriteLine(text);
            }
        }

        // Zapis skrócony do loga.
        public void SaveLog(string text)
        {
            using (StreamWriter sw = new StreamWriter(@"./Log.txt", true))
            {
                sw.WriteLine(Filename + "  " +text);
            }
        }

        // Średni czas.
        public string AverageTime()
        {
            double average = 0;
            average = timeList.Sum();
            average = (average / timeList.Count);

            return average.ToString();
        }
    }
}
