using System;
using System.Linq;
using Newtonsoft.Json;

namespace LoanPaymentCalculator {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Enter loan details following by Enter key");
            var inputParamethers = ColsoleParamethersReader.Read().ToArray();
            var inputProcessor = new InputProcessor();
            var LoanDetails = inputProcessor.ProcessInput(inputParamethers);
            var loanCalculator = new PaymentCalculator();
            var payments = loanCalculator.Calculate(LoanDetails);
            var outputProcessor = new OutputProcessor();
            var result = outputProcessor.Process(payments);
            Console.WriteLine("Payment details:");
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
