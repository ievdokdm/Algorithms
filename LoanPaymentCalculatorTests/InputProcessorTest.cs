using System;
using Xunit;
using LoanPaymentCalculator;
using System.Collections.Generic;

namespace LoanPaymentCalculatorTests {
    //[Collection("LoanPaymentCalculator")]
    public class InputProcessorTest {

        public static TheoryData<string[]> positiveTestCases => new TheoryData<string[]> {
            {
                new string[] {"amount: 100000", "interest: 5.5%", "downpayment: 20000", "term: 30"}
            },
            {
                new string[] {"amount: 100000", "interest: 5.5", "downpayment: 20000", "term: 30"}
            },
            {
                new string[] {"Amount: 100000", "inteZest: 5.5%", "downpaIment: 20000", "term: 30"}
            },
            {
                new string[] {"Amount: 100000", "inteZest: 5.5", "downpaIment: 20000", "term: 30"}
            },
            {
                new string[] {"AmounT: 100000", "inteZest: 5.5%", "downPaYment: 20000", "term: 30"}
            },
            {
                new string[] {" Amount : 100000", "interest:  5.5%", "downpayment: 20000", "term: 30"}
            },
            {
                new string[] {"Amount: 100000", "interest: 5.5%", "Down Payment: 20000", "term: 30"}
            },
            {
                new string[] {"interest: 5.5%", "downpayment: 20000", "term: 30", "amount: 100000"}
            },
            {
                new string[] {"downpayment: 20000", "term: 30", "amount: 100000", "interest: 5.5%"}
            },
            {
                new string[] {"term: 30", "amount: 100000", "interest: 5.5%", "downpayment: 20000" }
            }
        };

        public static TheoryData<string[]> negativeTestCases => new TheoryData<string[]> {
            {
                new string[] {"amount: 100000", "interest: 5.5%", "downpayment: 20000"}
            },
            {
                new string[] {"amount: 100000", "interest: 5.5%", "downpayment: 20000", "term: 30", "something: 123"}
            },
            {
                new string[] {"amount 100000", "interest: 5.5%", "downpayment: 20000", "term: 30"}
            },
            {
                new string[] {"OmAunD: 100000", "interest: 5.5%", "downpayment: 20000", "term: 30"}
            },
            {
                new string[] {"amount : 100000:", "interest:  5.5%", "downpayment: 20000", "term: 30"}
            },
            {
                new string[] {"amount: A00000", "interest: 5.5%", "downpayment: 20000", "term: 30"}
            },
            {
                new string[] {"amount: 100000", "interest: D.5%", "downpayment: 20000", "term: 30"}
            },
            {
                new string[] {"amount: 100000", "interest: 5.5%", "downpayment: E0000", "term: 30"}
            },
            {
                new string[] {"amount: 100000", "interest: 5.5%", "downpayment: 20000", "term: F"}
            }
        };

        [Theory]
        [MemberData(nameof(positiveTestCases))]
        public void VerifyPositiveScenarious(string[] input) {
            //Arrange
            var processor = new InputProcessor();

            //Act
            var result = processor.ProcessInput(input);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(100000m, result.Amount);
            Assert.Equal(20000m, result.Downpayment);
            Assert.Equal(5.5m, result.Interest);
            Assert.Equal(30, result.Term);
        }

        [Theory]
        [MemberData(nameof(negativeTestCases))]
        public void VerifyNegativeScenarious(string[] input) {
            //Arrange
            var processor = new InputProcessor();

            //Act
            Action result = () => processor.ProcessInput(input);

            //Assert
            Assert.Throws<ArgumentException>(result);
        }

        [Fact]
        public void VerifyArgumentIsNullScenario() {
            //Arrange
            var processor = new InputProcessor();

            //Act
            Action result = () => processor.ProcessInput(null);

            //Assert
            Assert.Throws<ArgumentNullException>(result);
        }
    }
}

