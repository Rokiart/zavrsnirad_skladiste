using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste.KonzolnaAplikacija
{
    internal class Pomocno
    {
        public static int ucitajBrojRaspon(string poruka,string greska,
            int poc,int kraj)
        {
            int b;
            while (true)
            {

                Console.Write(poruka);
                try
                {
                        b = int.Parse(Console.ReadLine());
                        if(b>=poc && b <= kraj)
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
        internal static int ucitajCijeliBroj(string poruka,string greska)
        {
            int b;
            while (true)
            {
                Console.WriteLine(poruka);
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
            Console.WriteLine(poruka);
            return Console.ReadLine().Trim().ToLower().Equals("da")?true : false;
        }
           
        public static string UcitajString(string poruka)
        {
            string s;
            while (true)
            {
                Console.WriteLine(poruka);
                s = Console.ReadLine();
                if(s.Trim().Length == 0)
                {
                    Console.WriteLine("Obavezan unos");
                    continue;
                }
                return s;

            }

        }
        internal static DateTime UcitajDatum(string v1 , string v2)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(v1);
                    return DateTime.Parse(Console.ReadLine());

                }catch (Exception ex)
                {
                    Console.WriteLine(v2);
                }
            }
        }

    }
}
