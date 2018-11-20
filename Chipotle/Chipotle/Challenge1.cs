using System.Text;

namespace Chipotle
{
    public class Challenge1
    {
        public static string compressedString(string message)
        {
            if (string.IsNullOrEmpty(message))
                return message;

            char consecutive = char.MinValue;
            int count = 1;
            var chars = message.ToCharArray();
            var builder = new StringBuilder();
            foreach (var current in chars)
            {
                if (consecutive == current)
                {
                    count++;
                }
                else
                {
                    if (count > 1)
                    {
                        builder.Append(count.ToString());
                        count = 1;
                    }
                    builder.Append(current);
                    consecutive = current;
                }
            }
            if (count > 1)
                builder.Append(count.ToString());

            return builder.ToString();
        }
    }
}
