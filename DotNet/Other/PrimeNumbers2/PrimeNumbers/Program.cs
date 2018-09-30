using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbers {
    public static class CollectionHelpers {
        public static IEnumerable<long> XRange(long start, long end, long step = 1) {
            for (long i = start; i <= end; i += step) {
                if (i <= 10 || i % 10 != 5)
                    yield return i;
            }
        }
    }

    class Program {

        /*
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
                }* 
        */

        public static List<long> CalculatePrimeNumbers(long n) {
            var primes = new List<long> { 2, 3, 5, 7 };
            for (long i = 11; i <= n; i += 2) {
                var isPrime = true;
                if (i % 3 == 0 || i % 5 == 0 || i % 7 == 0)
                    continue;
                var sqrt = (int)Math.Sqrt(i);
                for (var j = 3; j < primes.Count; j++) {
                    if (primes[j] >= sqrt)
                        break;
                    if (i % primes[j] == 0) {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) {
                    primes.Add(i);
                }
            }
            return primes;
        }



        public static IEnumerable<long> CalculatePrimeNumbersErtastofen1(bool[] sieve) {
            var sqrt = (long)Math.Sqrt(sieve.LongLength);
            for (long i = 2; i <= sqrt; i++) {
                if (!sieve[i]) {
                    yield return i;
                    for (long j = i * i; j < sieve.LongLength; j += i) {
                        sieve[j] = true;
                    }
                }
            }
            for (var prime = sqrt+1; prime < sieve.Length; prime++)
                if (!sieve[prime])
                    yield return prime;
        }

        public static IEnumerable<long> CalculatePrimeNumbersErtastofen2(bool[] sieve) {

            for (long i = 2; i < sieve.Length; i++) {
                if (!sieve[i]) {
                    yield return i;
                    for (long j = i * i; j < sieve.Length; j += i) {
                        sieve[j] = true;
                    }
                }
            }
        }

        public static IEnumerable<bool> CalculatePrimeNumbersErtastofen3(bool[] sieve) {
            var sqrt = (long)Math.Sqrt(sieve.Length);
            sieve[0] = true;
            sieve[1] = true;
            for (long i = 2; i <= sqrt; i++) {
                if (!sieve[i]) {
                    for (long j = i * i; j < sieve.Length; j += i) {
                        sieve[j] = true;
                    }
                }
            }
            return sieve.Where(p => p == false);
        }

        public static List<ulong> CalculatePrimeNumbersForInterval(ulong end) {
            ulong interval = end / 4; // (long)Math.Sqrt(end);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<ulong> primes = new List<ulong>((int)(end / (Math.Log(end) - 1.08366)));
            sw.Stop();
            Console.WriteLine("memory for CalculatePrimeNumbersForInterval alloction time {0}", sw.Elapsed);
            var sieve = new bool[interval];
            for (ulong i = 0; i < end; i += interval) {
                sw.Restart();
                CalculatePrimeNumbersForInterval(primes, i == 0 ? 2 : i, lowest(end, i + interval), sieve);
                sw.Stop();
                Console.WriteLine("CalculatePrimeNumbersForInterval inteval from  {0} to {1} time {2}", i, lowest(end, i + interval), sw.Elapsed);
            }
            return primes;
        }

        private static void CalculatePrimeNumbersForInterval(List<ulong> primes, ulong start, ulong end, bool[] sieve) {
            var len = end - start;
            if (primes.Count > 0) {
                for (ulong i = 0; i < len; i++)
                    sieve[i] = false;
                Parallel.ForEach(primes, (prime) => {
                    //foreach (var prime in primes) {
                    var multiplier = (start + prime - 1) / prime;
                    var sievePosition = prime * multiplier - start;
                    for (var j = sievePosition; j < len; j += prime) {
                        sieve[j] = true;
                    }
                });
            }
            for (ulong i = 0; i < len; i++) {
                if (!sieve[i]) {
                    var primeVal = i + start;
                    if(Math.Sqrt(end) > primeVal)
                    for (ulong j = primeVal * primeVal; j < end; j += primeVal) {
                        sieve[j - start] = true;
                    }
                    primes.Add(primeVal);
                }
            }
        }

        private static ulong lowest(ulong a, ulong b) {
            return a < b ? a : b;
        }

        static void Main(string[] args) {
            var len = 1000000000;
            Stopwatch sw = new Stopwatch();

            sw.Start();
            var sieve = new bool[len + 1];
            var primes = CalculatePrimeNumbersErtastofen1(sieve);
            long count = primes.Count();
            sw.Stop();
            Console.WriteLine("CalculatePrimeNumbersErtastofen1\tElapsed={0} Primes Found: {1}", sw.Elapsed, count);

            sw.Restart();
            var sieve2 = new bool[len + 1];
            var primes2 = CalculatePrimeNumbersErtastofen2(sieve2);
            count = primes2.Count();
            sw.Stop();
            Console.WriteLine("CalculatePrimeNumbersErtastofen2\tElapsed={0} Primes Found: {1}", sw.Elapsed, count);
            sw.Restart();

            var sieve3 = new bool[len + 1];
            var primes3 = CalculatePrimeNumbersErtastofen3(sieve3);
            count = primes3.Count();
            sw.Stop();
            Console.WriteLine("CalculatePrimeNumbersErtastofen3\tElapsed={0} Primes Found: {1}", sw.Elapsed, count);

            sw.Restart();
            var primes4 = CalculatePrimeNumbersForInterval((ulong)len);
            count = primes4.Count;
            sw.Stop();
            Console.WriteLine("CalculatePrimeNumbersForInterval\tElapsed={0} Primes Found: {1}", sw.Elapsed, count);

        }
    }
}
