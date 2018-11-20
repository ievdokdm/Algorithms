namespace Chipotle
{
    public class Challenge2
    {
        public static int maxDifference(int[] a)
        {
            var maxDiff = -1;
            if (a is null || a.Length < 2)
                return maxDiff;
            var minValue = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] <= minValue)
                {
                    minValue = a[i];
                }
                else if (a[i] - minValue > maxDiff)
                {
                    maxDiff = a[i] - minValue;
                }
            }
            return maxDiff;
        }
    }
}
