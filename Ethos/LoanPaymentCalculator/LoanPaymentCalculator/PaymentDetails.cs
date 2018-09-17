using System;
using Newtonsoft.Json;

namespace LoanPaymentCalculator {
    public class PaymentDetails {
        [JsonProperty(PropertyName = "monthly payment")]
        public decimal MonthlyPayment { get; set; }
        [JsonProperty(PropertyName = "total interest")]
        public decimal TotalInterest { get; set; }
        [JsonProperty(PropertyName = "total payment")]
        public decimal TotalPayment { get; set; }
    }
}
