using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buecherregal
{
    class Program
    {
        static string[] dateiinhalt;
        static cBuch[] bücherarray;
        static void Main(string[] args)
        {
            bool beenden = false;
            do
            {
                int Menue = Hauptmenue();

                if (Menue == 1)
                {
                    dateiinhalt =  Einlesen();
                }
                
                if (Menue == 2)
                {
                    Auswertung();
                }

                if (Menue == 3)
                {
                    Ausgabe();
                }

                if (Menue == 4)
                {
                    beenden = true;
                }

                if (Menue < 1 || Menue > 4)
                {
                    Console.WriteLine("Bitte geben Sie nur 1, 2, 3 oder 4 ein!");
                }
            } while (!beenden);
            
        }

        public static int Hauptmenue()
        {
            Console.WriteLine("Bitte waehlen Sie einen Menuepunkt aus:");
            Console.WriteLine("1: Daten einlesen\n2: Daten auswerten\n3: Ergebnis ausgeben\n4: Beenden");
            int Auswahl = Convert.ToInt32(Console.ReadLine());
            return Auswahl;
        }

        public static string[] Einlesen()
        {
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\Finn\Documents\buecherregal");
            FileInfo[] dateien = di.GetFiles("*.txt");
            for (int i = 0; i < dateien.Length; i++)
            {
                int zeilennummer = i+1;
                string zeile = zeilennummer.ToString() + ". " + dateien[i].Name;
                Console.WriteLine(zeile);
            }
            Console.WriteLine("BItte waehlen Sie eine Datei aus!");
            int Auswahl = Convert.ToInt32(Console.ReadLine()) - 1;
            string[] DateiAuswahl = File.ReadAllLines(dateien[Auswahl].FullName);
            Console.WriteLine("Daten eingelesen!");
            return DateiAuswahl;
        }

        public static void Auswertung()
        {
            int stueckbuecher = dateiinhalt.Length - 2;
            bücherarray = new cBuch[stueckbuecher];
            for (int i = 2; i < dateiinhalt.Length; i++)
            {
                bücherarray[i - 2] = new cBuch(Convert.ToInt32(dateiinhalt[i]));
            }
            bücherarray = HoehensortierungObj(bücherarray);
            RegalEinsortierung(bücherarray);
        }

        private static cBuch[] HoehensortierungObj(cBuch[] bücherarray)
        {
            int zaehler = bücherarray.Length;
            int temp = bücherarray[0].Höhe;

            for (int i = 0; i < zaehler; i++)
            {
                for (int j = i + 1; j < zaehler; j++)
                {
                    if (bücherarray[i].Höhe > bücherarray[j].Höhe)
                    {
                        temp = bücherarray[i].Höhe;

                        bücherarray[i].Höhe = bücherarray[j].Höhe;

                        bücherarray[j].Höhe = temp;
                    }
                }
            }

            return bücherarray;
        }

        public static void Ausgabe()
        {
            Console.WriteLine("Büchersortierung:");
            Console.WriteLine("Firugen: {0}",dateiinhalt[0]);
            Console.WriteLine("Buch|Höhe|Fach");
            for (int i = 0; i < bücherarray.Length; i++)
            {
                int nummer = i + 1;
                if (nummer < 10)
                {
                    Console.WriteLine("{0}   |{1} |{2} ", nummer, bücherarray[i].Höhe, bücherarray[i].Zone);
                }
                if (nummer > 9 && nummer < 100)
                {
                    Console.WriteLine("{0}  |{1} |{2} ", nummer, bücherarray[i].Höhe, bücherarray[i].Zone);
                }
                if (nummer > 99 && nummer < 1000)
                {
                    Console.WriteLine("{0} |{1} |{2} ", nummer, bücherarray[i].Höhe, bücherarray[i].Zone);
                }
            }
        }

        public static void RegalEinsortierung(cBuch[] array)
        {
            int aktuellezone = 1;
            int Zonenanfangshoehe = array[0].Höhe;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Höhe<=Zonenanfangshoehe+30)
                {
                    array[i].Zone = aktuellezone;
                }
                else
                {
                    aktuellezone++;
                    Zonenanfangshoehe = array[i].Höhe;
                    array[i].Zone = aktuellezone;

                }
            }
            if (aktuellezone > Convert.ToInt32(dateiinhalt[0]) + 1)
            {
                Console.WriteLine("Bei der Anzahl der Figuren kann nicht in jedem Fach ein maximal höhenunterschied von 3cm gewährleistet werden!");
            }
        }
    }
}
