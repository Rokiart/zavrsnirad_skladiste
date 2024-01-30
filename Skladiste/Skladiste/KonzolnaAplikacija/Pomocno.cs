using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skladiste.KonzolnaAplikacija
{
    internal class Pomocno
    {
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
    }
}
