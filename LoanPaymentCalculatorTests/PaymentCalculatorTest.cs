using System;
using Xunit;
using LoanPaymentCalculator;

namespace LoanPaymentCalculatorTests {

    public class PaymentCalculatorTest {
        public static TheoryData<LoanDetails, PaymentDetails> positiveTestCases => new TheoryData<LoanDetails, PaymentDetails> {
            {
                new LoanDetails{Amount=100000m, Interest = 5.5m, Downpayment = 20000m, Term = 30}, 
                new PaymentDetails{MonthlyPayment = 454.23m, TotalInterest = 83523.23m, TotalPayment = 163523.23m}
            },
            {
                new LoanDetails{Amount=100000m, Interest = 0m, Downpayment = 20000m, Term = 30},
                new PaymentDetails{MonthlyPayment = 222.22m, TotalInterest = 0m, TotalPayment = 80000m}
            }
        };

        public static TheoryData<LoanDetails> negativeTestCases => new TheoryData<LoanDetails> {
            {new LoanDetails{Amount=-100000m, Interest = 5.5m, Downpayment = 20000m, Term = 30}},
            {new LoanDetails{Amount=100000m, Interest = -5.5m, Downpayment = 20000m, Term = 30}},
            {new LoanDetails{Amount=100000m, Interest = 5.5m, Downpayment = -20000m, Term = 30}},
            {new LoanDetails{Amount=100000m, Interest = 5.5m, Downpayment = 20000m, Term = -30}},
            {new LoanDetails{Amount=100000m, Interest = 5.5m, Downpayment = 100000m, Term = 30}},
            {new LoanDetails{Amount=100000m, Interest = 5.5m, Downpayment = 200000m, Term = 30}},
            {new LoanDetails{Amount=100000m, Interest = 105.5m, Downpayment = 20000m, Term = 30}}

        };

        [Theory]
        [MemberData(nameof(positiveTestCases))]
        public void VerifyPositiveScenarious(LoanDetails input, PaymentDetails output) {
            //Arrange
            var calculator = new PaymentCalculator();

            //Act
            var result = calculator.Calculate(input);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(output.MonthlyPayment, result.MonthlyPayment);
            Assert.Equal(output.TotalInterest, result.TotalInterest);
            Assert.Equal(output.TotalPayment, result.TotalPayment);
        }

        [Theory]
        [MemberData(nameof(negativeTestCases))]
        public void VerifyNegativeScenarious(LoanDetails input) {
            //Arrange
            var calculator = new PaymentCalculator();

            //Act
            Action result = () => calculator.Calculate(input);

            //Assert
            Assert.Throws<ArgumentException>(result);
        }

        [Fact]
        public void VerifyArgumentIsNullScenario() {
            //Arrange
            var calculator = new PaymentCalculator();

            //Act
            Action result = () => calculator.Calculate(null);

            //Assert
            Assert.Throws<ArgumentNullException>(result);
        }
    }
}


