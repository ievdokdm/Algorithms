namespace CollectorsUniverse
{
    public class Challenge2
    {
        public static string FirstNonRepeatingLetter(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var charArr = new int[256];
            var chars = str.ToUpper().ToCharArray();
            foreach (var ch in chars)
            {
                charArr[ch] += 1;
            }
            foreach (var ch in str.ToCharArray())
            {
                var chUp = char.ToUpper(ch);
                if (charArr[chUp] == 1)
                {
                    return ch.ToString();
                }
            }
            return string.Empty;
        }
    }
}
