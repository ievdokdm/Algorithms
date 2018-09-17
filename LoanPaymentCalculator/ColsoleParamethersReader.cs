using System;
using System.Collections.Generic;

namespace LoanPaymentCalculator {
    public class ColsoleParamethersReader {
        public static IEnumerable<string> Read() {
            var inputCommandLines = new List<string>();

            while (true) {
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;
                yield return line;
            }
        }
    }
}
