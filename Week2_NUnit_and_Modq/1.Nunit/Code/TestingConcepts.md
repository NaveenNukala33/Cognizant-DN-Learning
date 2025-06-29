# Unit Testing Concepts and Best Practices

## Unit Testing vs Functional Testing

### Unit Testing
- **Definition**: Testing the smallest testable parts of an application (individual methods/functions)
- **Scope**: Tests isolated units of code
- **Dependencies**: Mocked or stubbed
- **Speed**: Very fast execution
- **Purpose**: Verify individual components work correctly in isolation

### Functional Testing
- **Definition**: Testing complete functionality from end-to-end
- **Scope**: Tests entire features or user workflows
- **Dependencies**: Real systems and integrations
- **Speed**: Slower execution
- **Purpose**: Verify system meets business requirements

## Types of Testing

1. **Unit Testing**: Testing individual components in isolation
2. **Functional Testing**: Testing complete features and workflows
3. **Automated Testing**: Tests that run without manual intervention
4. **Performance Testing**: Testing system performance under load
5. **Integration Testing**: Testing component interactions
6. **Acceptance Testing**: Testing against business requirements

## Benefits of Automated Testing

- **Faster Feedback**: Quick detection of bugs and regressions
- **Consistent Execution**: Tests run the same way every time
- **Regression Prevention**: Catches bugs when code changes
- **Documentation**: Tests serve as living documentation
- **Confidence**: Enables safe refactoring and feature additions
- **Cost Effective**: Reduces manual testing effort over time

## Loosely Coupled & Testable Design

### Characteristics of Testable Code:
- **Dependency Injection**: Dependencies passed in rather than created internally
- **Interface-based**: Code depends on abstractions, not implementations
- **Single Responsibility**: Each class/method has one reason to change
- **No Static Dependencies**: Avoids hard-to-mock static calls
- **Pure Functions**: Functions with no side effects when possible

### Example of Tightly Coupled Code (BAD):
`csharp
public class OrderService
{
    public void ProcessOrder(Order order)
    {
        // Directly creating dependencies - hard to test
        var emailService = new EmailService();
        var paymentService = new PaymentService();
        
        // Hard to test without actually sending emails and charging cards
        paymentService.ProcessPayment(order.Amount);
        emailService.SendConfirmation(order.Email);
    }
}
`

### Example of Loosely Coupled Code (GOOD):
`csharp
public class OrderService
{
    private readonly IEmailService _emailService;
    private readonly IPaymentService _paymentService;
    
    // Dependencies injected - easy to mock in tests
    public OrderService(IEmailService emailService, IPaymentService paymentService)
    {
        _emailService = emailService;
        _paymentService = paymentService;
    }
    
    public void ProcessOrder(Order order)
    {
        // Easy to test with mocked dependencies
        _paymentService.ProcessPayment(order.Amount);
        _emailService.SendConfirmation(order.Email);
    }
}
`

## NUnit Attributes Explained

### [TestFixture]
- Marks a class as containing tests
- Required for NUnit to recognize test classes

### [Test]
- Marks a method as a test method
- Method will be executed by test runner

### [TestCase]
- Parameterized test attribute
- Allows multiple test scenarios with different inputs
- Reduces code duplication

### [SetUp]
- Method runs before each test
- Used for initialization (creating objects, setting up data)
- Ensures each test starts with clean state

### [TearDown]
- Method runs after each test
- Used for cleanup (disposing objects, clearing data)
- Ensures no test affects another

### [Ignore]
- Skips test execution
- Useful for temporarily disabling tests
- Should include reason in attribute parameter

## Benefits of Parameterized Tests

### Without TestCase (Repetitive):
`csharp
[Test]
public void Add_Test1() { Assert.That(calculator.Add(1,2), Is.EqualTo(3)); }

[Test]
public void Add_Test2() { Assert.That(calculator.Add(5,3), Is.EqualTo(8)); }

[Test]
public void Add_Test3() { Assert.That(calculator.Add(-1,1), Is.EqualTo(0)); }
`

### With TestCase (Concise):
`csharp
[TestCase(1, 2, 3)]
[TestCase(5, 3, 8)]
[TestCase(-1, 1, 0)]
public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
{
    Assert.That(calculator.Add(a, b), Is.EqualTo(expected));
}
`

### Benefits:
- **Less Code**: One test method handles multiple scenarios
- **Better Coverage**: Easy to add more test cases
- **Maintainability**: Changes to test logic only needed in one place
- **Readability**: Clear relationship between inputs and expected outputs
