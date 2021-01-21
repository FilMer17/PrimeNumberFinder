using System;
using System.Collections.Generic;

namespace Help
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach (var item in SlowSieve(10))
            //{
            //    Console.WriteLine(item);
            //}
            //Console.ReadLine();
        }


        public static List<int> SlowSieve(int max)
        {
            bool[] is_prime = new bool[max + 1];
            List<int> primes = new List<int>();

            for (int i = 2; i <= max; i++)
                is_prime[i] = true;

            for (int i = 2; i <= max; i++)
            {
                if (is_prime[i])
                {
                    for (int j = i * 2; j <= max; j += i)
                        is_prime[j] = false;
                }
            }

            for (int i = 2; i <= max; i++)
            {
                if (is_prime[i]) 
                    primes.Add(i);
            }

            return primes;
        }
    }
}
