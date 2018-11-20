using System.Text;

namespace CollectorsUniverse
{
    public class Challenge1
    {
        public static string FizzBuzz(int n)
        {
            if (n < 1)
                return "Invalid";

            var fizzStr = "Fizz";
            var buzzStr = "Buzz";
            var fizzbuzzStr = "FizzBuzz";
            var fizCounter = 1;
            var buzzCounter = 1;
            var builder = new StringBuilder();
            for (var i = 1; i <= n; i++)
            {
                if (fizCounter == 3 && buzzCounter == 5)
                {
                    builder.AppendLine(fizzbuzzStr);
                    fizCounter = 0;
                    buzzCounter = 0;
                }
                else if (fizCounter == 3)
                {
                    builder.AppendLine(fizzStr);
                    fizCounter = 0;
                }
                else if (buzzCounter == 5)
                {
                    builder.AppendLine(buzzStr);
                    buzzCounter = 0;
                } else
                {
                    builder.AppendLine(i.ToString());
                }
                fizCounter++;
                buzzCounter++;
            }

            return builder.ToString(0, builder.Length-1);
        }
    }
}
