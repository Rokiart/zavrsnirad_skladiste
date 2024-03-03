using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digitron
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Jednostavan kalkulator:");
                Console.WriteLine("1 - Sabiranje");
                Console.WriteLine("2 - Oduzimanje");
                Console.WriteLine("3 - Množenje");
                Console.WriteLine("4 - Deljenje");
                Console.WriteLine("0 - Izlaz");

                Console.Write("Izaberite operaciju (0-4): ");
                string izbor = Console.ReadLine();

                if (izbor == "0")
                {
                    Console.WriteLine("Izlaz iz kalkulatora.");
                    break;
                }

                if (izbor != "1" && izbor != "2" && izbor != "3" && izbor != "4")
                {
                    Console.WriteLine("Nevažeći izbor. Molimo izaberite od 0 do 4.");
                    continue;
                }

                Console.Write("Unesite prvi broj: ");
                double broj1;
                if (!double.TryParse(Console.ReadLine(), out broj1))
                {
                    Console.WriteLine("Nevažeći unos. Molimo unesite broj.");
                    continue;
                }

                Console.Write("Unesite drugi broj: ");
                double broj2;
                if (!double.TryParse(Console.ReadLine(), out broj2))
                {
                    Console.WriteLine("Nevažeći unos. Molimo unesite broj.");
                    continue;
                }

                double rezultat = 0;

                switch (izbor)
                {
                    case "1":
                        rezultat = broj1 + broj2;
                        break;
                    case "2":
                        rezultat = broj1 - broj2;
                        break;
                    case "3":
                        rezultat = broj1 * broj2;
                        break;
                    case "4":
                        if (broj2 != 0)
                        {
                            rezultat = broj1 / broj2;
                        }
                        else
                        {
                            Console.WriteLine("Nemoguće deljenje sa nulom.");
                            continue;
                        }
                        break;
                    default:
                        break;
                }

                Console.WriteLine($"Rezultat: {rezultat}");
            }
        }
    }
}
