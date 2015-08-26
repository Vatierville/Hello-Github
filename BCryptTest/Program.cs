using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TicketmasterSystems.Encryption;

namespace BCryptTest
{
    // TMPrime_3 merge conflict
    class Program
    {
        // Git changes
        static void Main(string[] args)
        {
            const int iterations = 1000;
            var passwords = new List<string>();

            for (int i = 0 ; i < iterations ; ++i)
            {
                passwords.Add("zhadum66T!" + i);
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0 ; i < iterations ; ++i)
            {
                string hashedPassword = BCrypt.HashPassword(passwords[i], BCrypt.GenerateSalt());
                Console.WriteLine("{0}\t{1}", passwords[i], hashedPassword);
            }

            stopwatch.Stop();

            Console.WriteLine("Elapsed = {0}", stopwatch.Elapsed);
            Console.ReadLine();
        }
    }
}
