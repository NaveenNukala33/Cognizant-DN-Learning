using System;

namespace CalcLibrary
{
    /// <summary>
    /// Calculator class demonstrating loosely coupled and testable design
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Adds two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Sum of the two numbers</returns>
        public int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// Subtracts second number from first number
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Difference of the two numbers</returns>
        public int Subtract(int a, int b)
        {
            return a - b;
        }

        /// <summary>
        /// Multiplies two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Product of the two numbers</returns>
        public int Multiply(int a, int b)
        {
            return a * b;
        }

        /// <summary>
        /// Divides first number by second number
        /// </summary>
        /// <param name="a">Dividend</param>
        /// <param name="b">Divisor</param>
        /// <returns>Quotient of the division</returns>
        /// <exception cref="DivideByZeroException">Thrown when divisor is zero</exception>
        public double Divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            
            return (double)a / b;
        }

        /// <summary>
        /// Calculates percentage of a number
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="percentage">The percentage</param>
        /// <returns>Percentage of the value</returns>
        public double CalculatePercentage(double value, double percentage)
        {
            return (value * percentage) / 100;
        }

        /// <summary>
        /// Checks if a number is even
        /// </summary>
        /// <param name="number">Number to check</param>
        /// <returns>True if even, false if odd</returns>
        public bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        /// <summary>
        /// Finds the maximum of two numbers
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Maximum of the two numbers</returns>
        public int Max(int a, int b)
        {
            return Math.Max(a, b);
        }
    }
}
