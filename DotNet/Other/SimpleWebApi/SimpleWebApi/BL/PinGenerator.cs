using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.BL
{
    public static class PinGenerator
    {
        public static string GeneratePin(uint value)
        {
            const int size = 32;
            const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            uint radix = 26;

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
    }
}
