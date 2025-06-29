using NUnit.Framework;
using Moq;
using CustomerCommLib;

namespace CustomerComm.Tests
{
    [TestFixture]
    public class CustomerCommTests
    {
        private Mock<IMailSender> _mockMailSender;
        private CustomerCommLib.CustomerComm _customerComm;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // This runs once before all tests
        }

        [SetUp]
        public void SetUp()
        {
            // Setup mock object
            _mockMailSender = new Mock<IMailSender>();
            
            // Configure mock to return true for any string parameters
            _mockMailSender.Setup(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(true);
            
            // Inject mock dependency
            _customerComm = new CustomerCommLib.CustomerComm(_mockMailSender.Object);
        }

        [TestCase]
        public void SendMailToCustomer_ShouldReturnTrue_WhenMailSent()
        {
            // Act
            bool result = _customerComm.SendMailToCustomer();

            // Assert
            Assert.That(result, Is.True);
            
            // Verify that SendMail was called exactly once
            _mockMailSender.Verify(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestCase]
        public void SendMailToCustomer_ShouldCallSendMail_WithCorrectParameters()
        {
            // Act
            _customerComm.SendMailToCustomer();

            // Assert - Verify specific parameters
            _mockMailSender.Verify(x => x.SendMail("cust123@abc.com", "Some Message"), Times.Once);
        }

        [TestCase]
        public void SendMailToCustomer_ShouldReturnFalse_WhenMailSendingFails()
        {
            // Arrange - Setup mock to return false
            _mockMailSender.Setup(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(false);
            
            var customerComm = new CustomerCommLib.CustomerComm(_mockMailSender.Object);

            // Act
            bool result = customerComm.SendMailToCustomer();

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
