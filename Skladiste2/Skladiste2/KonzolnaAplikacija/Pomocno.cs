using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste2.KonzolnaAplikacija
{
    internal class Pomocno
    {
        public static bool dev;
        public static int ucitajBrojRaspona(string poruka, string greska,
            int poc, int kraj)
        {
            int b;
            while (true)
            {
                Console.Write(poruka);
                try
                {
                    b = int.Parse(Console.ReadLine());
                    if (b >= poc && b<= kraj)
                    {
                        return b;
                    }
                    Console.WriteLine(greska);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(greska);
                }
            }
        }
        internal static int ucitajCijeliBroj(string poruka, string greska)
        {
            int b;
            while (true)
            {
                Console.Write(poruka);
                try
                {
                    b = int.Parse(Console.ReadLine());
                    if (b > 0)
                    {
                        return b;
                    }
                    Console.WriteLine(greska);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(greska);
                }
            }
        }

        internal static decimal ucitajDecimalniBroj(string poruka, string greska)
        {
            decimal b;
            while (true)
            {
                Console.Write(poruka);
                try
                {
                    b = decimal.Parse(Console.ReadLine());
                    if (b > 0)
                    {
                        return b;
                    }
                    Console.WriteLine(greska);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(greska);
                }
            }
        }

        internal static bool ucitajBool(string poruka)
        {
            Console.Write(poruka);
            return Console.ReadLine().Trim().ToLower().Equals("da") ? true : false;
        }

        internal static string UcitajString(string poruka, string greska)
        {
            string s = "";
            while (true)
            {
                Console.Write(poruka);
                s = Console.ReadLine();
                if (s != null && s.Trim().Length > 0)
                {
                    return s;
                }
                Console.WriteLine(greska);
            }
        }

        internal static DateTime ucitajDatum(string v1, string v2)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(v1);
                    return DateTime.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(v2);
                }
            }
        }

        public bool ProvjeriOIB(string oib)
        {
            if (oib.Length != 11)
            {
                Console.WriteLine("**********************************");
                Console.WriteLine("OIB mora imati 11 znamenki.");
                return false;
            }

            long b;
            if (!long.TryParse(oib, out b))
            {
                Console.WriteLine("**********************************");

                Console.WriteLine("OIB mora sadržavati samo brojeve.");
                return false;
            }

            int a = 10;
            for (int i = 0; i < 10; i++)
            {
                a = a + int.Parse(oib.Substring(i, 1));
                a = a % 10;
                if (a == 0)
                {
                    a = 10;
                }
                a *= 2;
                a = a % 11;
            }
        }
}
