using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TicketmasterSystems.Encryption;

namespace BCryptTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int iterations = 100;
            var passwords = new List<string>();

            for (int i = 0 ; i < iterations ; ++i)
            {
                passwords.Add("passWORD!" + i);
            }

            var stopwatch = new Stopwatch();
            Console.WriteLine("Starting...");
            stopwatch.Start();

            for (int i = 0 ; i < iterations ; ++i)
            {
                string hashedPassword = BCrypt.HashPassword(passwords[i], BCrypt.GenerateSalt(4));
                if (i < 1)
                {
                    Console.WriteLine("{0}\t{1}", passwords[i], hashedPassword);
                }
            }

            stopwatch.Stop();

            Console.WriteLine("Elapsed = {0}ms", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Time per Encrypt = {0}ms", stopwatch.ElapsedMilliseconds / iterations);
            Console.ReadLine();
        }
    }
}
