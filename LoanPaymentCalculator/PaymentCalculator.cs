using System;
namespace LoanPaymentCalculator {
    public class PaymentCalculator {
        public PaymentDetails Calculate(LoanDetails loan) {
            if (loan == null)
                throw new ArgumentNullException(nameof(loan));

            if (loan.Amount <= 0)
                throw new ArgumentException("Ammount should be positive");

            if (loan.Interest < 0)
                throw new ArgumentException("Interest should be positive");
            if (loan.Interest >= 100)
                throw new ArgumentException("Interest should be less then 100%");

            if (loan.Downpayment < 0)
                throw new ArgumentException("Downpayment should be positive value");
            if (loan.Downpayment >= loan.Amount)
                throw new ArgumentException("Downpayment should be less than Amount");

            if (loan.Term <= 0)
                throw new ArgumentException("Term should be positive");


            var payments = new PaymentDetails();

            var loanAmount = loan.Amount - loan.Downpayment;
            var monthsTerm = loan.Term * 12;
            decimal monthly;

            if (loan.Interest == 0) {
                monthly = loanAmount / monthsTerm;
            } else {
                var rate = loan.Interest / 12 / 100;
                var factor = rate / (1 - (decimal)Math.Pow((double)(1 + rate), -monthsTerm));
                monthly = loanAmount * factor;
            }

            payments.MonthlyPayment = decimal.Round(monthly, 2);
            payments.TotalPayment = decimal.Round(monthly * monthsTerm, 2);
            payments.TotalInterest = payments.TotalPayment - loanAmount;
            return payments;
        }
    }
}
