using NUnit.Framework;
using CalcLibrary;
using System;

namespace CalculatorTests
{
    /// <summary>
    /// Unit Test Class for Calculator
    /// Demonstrates TestFixture, SetUp, TearDown, Test, TestCase, and Ignore attributes
    /// </summary>
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        /// <summary>
        /// SetUp method - runs before each test
        /// Used for initialization activities
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Initialize calculator instance for each test
            // This ensures each test starts with a fresh, clean state
            _calculator = new Calculator();
            
            // You can add more initialization here if needed
            // For example: logging, database setup, etc.
            Console.WriteLine("Test setup completed - Calculator instance created");
        }

        /// <summary>
        /// TearDown method - runs after each test
        /// Used for cleanup activities
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Cleanup activities after each test
            // For example: dispose resources, cleanup files, etc.
            _calculator = null;
            Console.WriteLine("Test cleanup completed - Calculator instance disposed");
        }

        #region Addition Tests

        /// <summary>
        /// Basic addition test without parameters
        /// </summary>
        [Test]
        public void Add_ShouldReturnCorrectSum_WhenAddingTwoPositiveNumbers()
        {
            // Arrange
            int a = 5;
            int b = 3;
            int expected = 8;

            // Act
            int actual = _calculator.Add(a, b);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Parameterized test cases for addition
        /// Demonstrates the benefit of TestCase attribute for multiple test scenarios
        /// </summary>
        [TestCase(1, 2, 3, Description = "Adding two positive numbers")]
        [TestCase(-1, -2, -3, Description = "Adding two negative numbers")]
        [TestCase(10, -5, 5, Description = "Adding positive and negative numbers")]
        [TestCase(0, 5, 5, Description = "Adding zero and positive number")]
        [TestCase(0, 0, 0, Description = "Adding two zeros")]
        [TestCase(100, 200, 300, Description = "Adding larger numbers")]
        [TestCase(int.MaxValue - 1, 1, int.MaxValue, Description = "Adding near maximum integer")]
        public void Add_ShouldReturnCorrectSum_WithVariousInputs(int a, int b, int expected)
        {
            // Act
            int actual = _calculator.Add(a, b);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), 
                $"Expected {a} + {b} = {expected}, but got {actual}");
        }

        #endregion

        #region Subtraction Tests

        [TestCase(5, 3, 2, Description = "Subtracting smaller from larger")]
        [TestCase(3, 5, -2, Description = "Subtracting larger from smaller")]
        [TestCase(10, 10, 0, Description = "Subtracting equal numbers")]
        [TestCase(0, 5, -5, Description = "Subtracting from zero")]
        [TestCase(5, 0, 5, Description = "Subtracting zero")]
        public void Subtract_ShouldReturnCorrectDifference_WithVariousInputs(int a, int b, int expected)
        {
            // Act
            int actual = _calculator.Subtract(a, b);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        #endregion

        #region Multiplication Tests

        [TestCase(2, 3, 6, Description = "Multiplying two positive numbers")]
        [TestCase(-2, 3, -6, Description = "Multiplying negative and positive")]
        [TestCase(-2, -3, 6, Description = "Multiplying two negative numbers")]
        [TestCase(0, 5, 0, Description = "Multiplying by zero")]
        [TestCase(1, 5, 5, Description = "Multiplying by one")]
        public void Multiply_ShouldReturnCorrectProduct_WithVariousInputs(int a, int b, int expected)
        {
            // Act
            int actual = _calculator.Multiply(a, b);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        #endregion

        #region Division Tests

        [TestCase(6, 2, 3.0, Description = "Dividing evenly")]
        [TestCase(7, 2, 3.5, Description = "Dividing with remainder")]
        [TestCase(-6, 2, -3.0, Description = "Dividing negative by positive")]
        [TestCase(0, 5, 0.0, Description = "Dividing zero")]
        public void Divide_ShouldReturnCorrectQuotient_WithVariousInputs(int a, int b, double expected)
        {
            // Act
            double actual = _calculator.Divide(a, b);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
        }

        [Test]
        public void Divide_ShouldThrowDivideByZeroException_WhenDividingByZero()
        {
            // Arrange
            int a = 10;
            int b = 0;

            // Act & Assert
            Assert.That(() => _calculator.Divide(a, b), 
                Throws.TypeOf<DivideByZeroException>()
                .With.Message.EqualTo("Cannot divide by zero"));
        }

        #endregion

        #region Percentage Tests

        [TestCase(100, 10, 10.0, Description = "10% of 100")]
        [TestCase(200, 25, 50.0, Description = "25% of 200")]
        [TestCase(150, 33.33, 49.995, Description = "33.33% of 150")]
        public void CalculatePercentage_ShouldReturnCorrectPercentage_WithVariousInputs(
            double value, double percentage, double expected)
        {
            // Act
            double actual = _calculator.CalculatePercentage(value, percentage);

            // Assert
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
        }

        #endregion

        #region Boolean Tests

        [TestCase(2, true, Description = "Two is even")]
        [TestCase(3, false, Description = "Three is odd")]
        [TestCase(0, true, Description = "Zero is even")]
        [TestCase(-2, true, Description = "Negative two is even")]
        [TestCase(-3, false, Description = "Negative three is odd")]
        public void IsEven_ShouldReturnCorrectResult_WithVariousInputs(int number, bool expected)
        {
            // Act
            bool actual = _calculator.IsEven(number);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        #endregion

        #region Max Tests

        [TestCase(5, 3, 5, Description = "First number is larger")]
        [TestCase(3, 5, 5, Description = "Second number is larger")]
        [TestCase(5, 5, 5, Description = "Both numbers are equal")]
        [TestCase(-3, -5, -3, Description = "Both numbers are negative")]
        public void Max_ShouldReturnLargerNumber_WithVariousInputs(int a, int b, int expected)
        {
            // Act
            int actual = _calculator.Max(a, b);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        #endregion

        #region Ignored Tests (Demonstration)

        /// <summary>
        /// Example of using Ignore attribute
        /// This test will be skipped during execution
        /// </summary>
        [Test]
        [Ignore("This test is ignored for demonstration purposes")]
        public void IgnoredTest_ShouldNotRun()
        {
            // This test will not execute due to Ignore attribute
            Assert.That(true, Is.True);
        }

        /// <summary>
        /// Example of conditionally ignored test
        /// </summary>
        [Test]
        [Ignore("Feature not implemented yet")]
        public void FutureFeature_ShouldWork_WhenImplemented()
        {
            // This represents a test for a feature not yet implemented
            // Use Ignore to mark tests that should not run yet
            Assert.Fail("This feature is not implemented yet");
        }

        #endregion

        #region Integration-style Tests

        /// <summary>
        /// Test demonstrating multiple operations together
        /// </summary>
        [Test]
        public void Calculator_ShouldPerformMultipleOperations_Correctly()
        {
            // Arrange & Act & Assert
            int sum = _calculator.Add(5, 3);
            Assert.That(sum, Is.EqualTo(8));

            int difference = _calculator.Subtract(sum, 3);
            Assert.That(difference, Is.EqualTo(5));

            int product = _calculator.Multiply(difference, 2);
            Assert.That(product, Is.EqualTo(10));

            double quotient = _calculator.Divide(product, 2);
            Assert.That(quotient, Is.EqualTo(5.0));
        }

        #endregion
    }
}
