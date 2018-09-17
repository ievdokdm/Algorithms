using System;
using Xunit;
using LoanPaymentCalculator;
using System.Collections.Generic;

namespace LoanPaymentCalculatorTests {
    public class OutputProcessorTest {

        [Fact]
        public void VerifyArgumentIsNullScenario() {
            //Arrange
            var outputProcessor = new OutputProcessor();

            //Act
            Action result = () => outputProcessor.Process(null);

            //Assert
            Assert.Throws<ArgumentNullException>(result);
        }

        [Fact]
        public void VerifyArgumentPositivelScenario() {
            //Arrange
            var outputProcessor = new OutputProcessor();
            var payments = new PaymentDetails {
                MonthlyPayment = 454.23m,
                TotalInterest = 83523.23m,
                TotalPayment = 163523.23m
            };

            //Act
            var result = outputProcessor.Process(payments);

            //Assert
            Assert.Equal("{"+Environment.NewLine+
                         "  \"monthly payment\": 454.23," + Environment.NewLine + 
                         "  \"total interest\": 83523.23," + Environment.NewLine + 
                         "  \"total payment\": 163523.23" + Environment.NewLine +
                         "}", result);
        }
    }
}
