using System;

namespace CollectorsUniverse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Challenge1.FizzBuzz(15));
            Console.WriteLine(Challenge2.FirstNonRepeatingLetter("abbra"));
            Console.WriteLine(Challenge3.CountChange(10, new int[] { 5, 2, 3 }));
        }
    }
}
