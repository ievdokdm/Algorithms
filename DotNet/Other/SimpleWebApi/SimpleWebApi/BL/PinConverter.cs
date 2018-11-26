using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.BL
{
    public static class PinConverter
    {
        public static string IntToPin(int value)
        {
            const int size = 32;
            const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int radix = 26;

            if (value < 0)
                throw new ArgumentException("value should be positive", "value");

            if (value == 0)
                return Alphabet[0].ToString();

            int index = size - 1;
            char[] charArray = new char[size];

            while (value != 0)
            {
                int remainder = (int)(value % radix);
                charArray[index--] = Alphabet[remainder];
                value = value / radix;
            }

            string result = new String(charArray, index + 1, size - index - 1);

            return result;
        }

        public static int PinToInt(string pin)
        {
            const string Digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int radix = 26;

            if (String.IsNullOrEmpty(pin))
                return 0;

            // Make sure pin is in upper case
            pin = pin.ToUpperInvariant();

            int result = 0;
            int multiplier = 1;
            for (int i = pin.Length - 1; i >= 0; i--)
            {
                char c = pin[i];

                int digit = Digits.IndexOf(c);
                if (digit == -1)
                    throw new ArgumentException("Invalid character in the pin", "pin");

                result += digit * multiplier;
                multiplier *= radix;
            }

            return result;
        }
    }
}
