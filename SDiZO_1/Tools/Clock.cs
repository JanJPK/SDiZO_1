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
        // Parametr sampleSize zawiera parametry danej operacji np. ilość dodawanych obiektów oraz sam typ operacji.
        // Jest on dopisany do pliku .txt jako pierwsza linijka oznaczająca start nowego pomiaru.
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
        public void Finish()
        {
            sw.Stop();
            // Wersja alternatywna - pomiar ticków, podzielenie przez częstotliwość a następnie pomnożenie
            // w celu uzyskania bardzo dokładnego pomiaru nanosekund.
            // Tak wielka dokładność okazała się zbędna.
            /*
            ticks = sw.ElapsedTicks;
            timeList.Add((ticks / Stopwatch.Frequency) * 1000000000);
            SaveProgress(((ticks / Stopwatch.Frequency)* 1000000000).ToString());
            */
            timeList.Add(sw.Elapsed.TotalMilliseconds);
            SaveProgress(sw.Elapsed.TotalMilliseconds.ToString());
        }

        // Stop
        // Zatrzymuje pomiar.
        public void Stop()
        {
            sw.Stop();
        }

        // Zapis do pliku.
        // Zapisuje czas wykonania każdego cyklu.
        private void SaveProgress(string text)
        {
            using (StreamWriter sw = new StreamWriter(@"./" + Filename + ".txt",true))
            {
                sw.WriteLine(text);
            }
        }

        // Zapis skrócony do loga.
        // Zapisuje uśredniony czas wszystkich cykli.
        public void SaveLog(string text)
        {
            using (StreamWriter sw = new StreamWriter(@"./Log.txt", true))
            {
                sw.WriteLine(Filename + "  " +text);
            }
        }

        // Zwraca średni czas wszystkich cykli.
        public string AverageTime()
        {
            double average;
            average = timeList.Sum();
            average = (average / timeList.Count);

            return average.ToString();
        }
    }
}
