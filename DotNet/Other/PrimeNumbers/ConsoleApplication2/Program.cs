using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace ConsoleApplication2
{
    public static class MyExtensions
    {
        public static bool AnyWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> checkCondition, Func<TSource, bool> whileCondition)
        {
            foreach (TSource source1 in source)
            {
                if (checkCondition(source1))
                    return true;
                if (!whileCondition(source1))
                    break;
            }
            return false;
        }
    }
    class Solution
    {
        static List<int> PrimeNumbersConcurent(int n)
        {
            return Enumerable.Range(2, n - 1)
                .AsParallel()
                .WithDegreeOfParallelism(Environment.ProcessorCount)
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .WithMergeOptions(ParallelMergeOptions.NotBuffered) // remove order dependancy
                .Where(x => Enumerable.Range(2, x - 2)
                    .All(y => x % y != 0))
                .ToList();
        }


        //static bool Check

        static List<int> PrimeNumbers1(int n)
        {
            List<int> primes = new List<int>();
            //primes.Add(2);
            foreach (var num in Enumerable.Range(2, n - 1))
            {
                var stop = false;
                foreach (var prime in primes)
                {
                    if (prime > Math.Sqrt(num) + 1)
                    {
                        primes.Add(num);
                        stop = true;
                        break;
                    }
                    if (num % prime == 0)
                    {
                        stop = true;
                        break;
                    }
                }
                if (!stop)
                {
                    primes.Add(num);
                }
            }

            return primes;
        }

        static IEnumerable<int> XRange(int start, int stop, int step = 1)
        {
            for (var i = start; i <= stop; i += step)
            {
                yield return i;
            }
        }

        static List<int> PrimeNumbers2(int n)
        {
            var primes = new List<int> { 2 };
            //var 
            foreach (var num in XRange(3, n, 2))
            {
                if (num > 10 && num % 10 == 5)
                    continue;
                foreach (var prime in primes)
                {
                    if (prime > Math.Sqrt(num))
                    {
                        primes.Add(num);
                        break;
                    }
                    if (num % prime == 0)
                    {
                        break;
                    }
                }
            }
            return primes;
        }

        static HashSet<int> PrimeNumbers3(int n)
        {
            var primes = new HashSet<int> { 2 };
            foreach (var num in XRange(3, n, 2)
                .Where(num => num <= 10 || num % 10 != 5))
            {
                if (!primes.AnyWhile(p => num % p == 0, l => l < Math.Sqrt(num) + 1))
                {
                    primes.Add(num);
                }
            }
            return primes;
        }

        public static HashSet<int> PrimesNumbersEratosfen(int n) {
            var primes = new HashSet<int>();
            var numbers = Enumerable.Range(1, n).ToArray();
            var len = numbers.Length;
            for (var i = 1; i < len; i++) {
                var num = numbers[i];
                if (num == 0)
                    continue;
                primes.Add(num);
                for (var j = i; j < len; j += num) {
                    numbers[j] = 0;
                }
            }
            return primes;
        }

        /*private static Dictionary<int, bool> DictRange(int start, int count)
        {
            for (int i = 0; i < count; ++i)
                yield return start + i;
        }*/
        public static HashSet<int> PrimesNumbersEratosfen2(int n) {
            var primes = new HashSet<int>();
            
            var numbers = Enumerable.Range(2, n - 2).ToDictionary(k=>k,v=>true);
            for (var i = 2; i < n; i++) {
                
                if (!numbers[i])
                    continue;
                primes.Add(i);
                for (var j = i; j <= n; j += i)
                {
                    numbers[j] = false;
                }
            }
            return primes;
        }
        static void Main(string[] args)
        {
            int n = 5;
            Console.WriteLine(n);
            Stopwatch sw = new Stopwatch();

            sw.Start();
            /*var primes1 = PrimeNumbers1(n);
            sw.Stop();
            Console.WriteLine("PrimeNumbersCount1\t\tElapsed={0} Primes Found: {1}", sw.Elapsed, primes1.Count);
            sw.Restart();*/
            /*var primes2 = PrimeNumbers2(n);
            sw.Stop();
            Console.WriteLine("PrimeNumbersCount2\t\tElapsed={0} Primes Found: {1}", sw.Elapsed, primes2.Count);*/
            /*sw.Restart();
            var primes3 = PrimeNumbers3(n);
            sw.Stop();*/
            //Console.WriteLine("PrimeNumbersCount3 Elapsed={0} Primes Found: {1}", sw.Elapsed, primes3.Count);
            /*sw.Restart();*/
            var primesEratosfen = PrimesNumbersEratosfen(n);
            sw.Stop();
            Console.WriteLine("PrimeNumbersCountEratosfen\tElapsed={0} Primes Found: {1}", sw.Elapsed, primesEratosfen.Count);
            /*sw.Restart();
            var primesEratosfen2 = PrimesNumbersEratosfen2(n);
            sw.Stop();
            Console.WriteLine("PrimeNumbersCountEratosfen2\tElapsed={0} Primes Found: {1}", sw.Elapsed, primesEratosfen2.Count);*/
            Console.ReadLine();
        }


    }
}