using System;
using System.Collections.Generic;
using FastFuzzyStringMatcher;

namespace LoanPaymentCalculator {
    public class InputProcessor {
        private StringMatcher<int> fuzzyMatcher = new StringMatcher<int>(MatchingOption.RemoveSpacingAndLinebreaks);

        public InputProcessor() {
            fuzzyMatcher.Add("amount", 0);
            fuzzyMatcher.Add("interest", 1);
            fuzzyMatcher.Add("downpayment", 2);
            fuzzyMatcher.Add("term", 3);
        }

        public LoanDetails ProcessInput(string[] inputParamethers) {
            if(inputParamethers == null)
                throw new ArgumentNullException(nameof(inputParamethers));

            var inputValues = processInputParameters(inputParamethers);
            var LoanDetails = new LoanDetails();

            if (!decimal.TryParse(inputValues[0], out var amount))
                throw new ArgumentException($"Can not convert ammount value: {inputValues[0]} to Decimal");
            if(amount <= 0)
                throw new ArgumentException("Ammount should be positive");
            LoanDetails.Amount = amount;

            if (!decimal.TryParse(inputValues[1].TrimEnd('%'), out var interest))
                throw new ArgumentException($"Can not convert interest value: {inputValues[1]} to Decimal");
            if (interest < 0)
                throw new ArgumentException("Interest should be positive");
            if (interest >= 100)
                throw new ArgumentException("Interest should be less then 100%");
            LoanDetails.Interest = interest;

            if (!decimal.TryParse(inputValues[2], out var downpayment))
                throw new ArgumentException($"Can not convert downpayment value: {inputValues[2]} to Decimal");
            if(downpayment < 0)
                throw new ArgumentException("Downpayment should be positive value");
            if (downpayment > amount)
                throw new ArgumentException("Downpayment should be less than Amount");
            LoanDetails.Downpayment = downpayment;

            if (!int.TryParse(inputValues[3], out var term))
                throw new ArgumentException($"Can not convert term value: {inputValues[3]} to Integer");
            if(term <= 0)
                throw new ArgumentException("Term should be positive");
            LoanDetails.Term = term;

            return LoanDetails;
        }

        private string[] processInputParameters(string[] inputParamethers) {
            var inputValues = new string[4];


            if(inputParamethers.Length < 4){

                throw new ArgumentException("ammount, interest, downpayment and term should be provided");
            }

            if (inputParamethers.Length > 4) {
                throw new ArgumentException($"Expected input of ammount, interest, downpayment and term but provided {inputParamethers.Length} characterisitics");
            }

            foreach (var text in inputParamethers) {
                var characteristic = text.Split(':');
                if (characteristic.Length != 2)
                    throw new ArgumentException("Each line of input should have loan characteristic with its value separated by ':' character");
                var serachResult = fuzzyMatcher.Search(characteristic[0], 80f);
                if (serachResult.Count != 1)
                    throw new ArgumentException($"Can not parse loan characteristic {characteristic[0]}");
                inputValues[serachResult[0].AssociatedData] = characteristic[1].Trim();
            }
            return inputValues;
        }
    }
}
