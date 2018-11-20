namespace CollectorsUniverse
{
    public class Challenge3
    {
        public static int CountChange(int money, int[] coins)
        {
            return CountChange(money, coins, 0);
        }

        private static int CountChange(int money, int[] coins, int currCoinIdx)
        {
            if (currCoinIdx == coins.Length)
                return 0;
            var currCoin = coins[currCoinIdx];
            var r = 0;
            for (var sum = 0; sum <= money; sum += currCoin)
            {
                if (sum < money)
                {
                    r += CountChange(money - sum, coins, currCoinIdx + 1);
                }
                else if (sum == money)
                {
                    r++;
                }
            }
            return r;
        }
    }
}
