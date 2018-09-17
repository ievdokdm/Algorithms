namespace LeetCode {
    public class AToI {
        public int MyAtoi(string str) {
            if (string.IsNullOrEmpty(str)) return 0;
            str = str.Trim(' ');
            if (str.Length == 0) return 0;
            bool isNegative = str[0] == '-' ? true : false;
            int i = (str[0] == '-' || str[0] == '+') ? 1 : 0;
            int result = 0;
            while (i < str.Length && char.IsDigit(str[i])) {
                int digit = str[i] - '0';
                if (result > int.MaxValue / 10 || (result == int.MaxValue / 10 && digit > 7))
                    return isNegative == true ? int.MinValue : int.MaxValue;
                result = result * 10 + digit;
                ++i;
            }
            return isNegative == true ? -1 * result : result;
        }
    }
}
