using System;
namespace LoanPaymentCalculator {
    public class LoanDetails {
        public decimal Amount { get; set; }
        public decimal Interest { get; set; }
        public decimal Downpayment { get; set; }
        public int Term { get; set; }
    }
}
