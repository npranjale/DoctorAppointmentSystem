using DoctorAppointmentSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ApplicationUser_ShouldInheritFromIdentityUser()
        {
            // Arrange & Act
            var user = new ApplicationUser();

            // Assert
            Assert.IsInstanceOfType<IdentityUser>(user);
        }

        [TestMethod]
        public void ApplicationUser_ShouldSupportIdentityUserProperties()
        {
            // Arrange
            var user = new ApplicationUser();

            // Act
            user.UserName = "johnsmith";
            user.Email = "john@example.com";
            user.PhoneNumber = "1234567890";

            // Assert
            Assert.AreEqual("johnsmith", user.UserName);
            Assert.AreEqual("john@example.com", user.Email);
            Assert.AreEqual("1234567890", user.PhoneNumber);
        }

        [TestMethod]
        public void FullName_ShouldAcceptAndStoreValidName()
        {
            // Arrange
            var user = new ApplicationUser();
            var expectedName = "John Smith";

            // Act
            user.FullName = expectedName;

            // Assert
            Assert.AreEqual(expectedName, user.FullName);
        }

        [TestMethod]
        public void FullName_ShouldAcceptExactly100Characters()
        {
            // Arrange
            var user = new ApplicationUser();
            user.FullName = new string('A', 100);

            // Act
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            // Assert
            var isValid = Validator.TryValidateObject(
                user, context, results, validateAllProperties: true);

            Assert.IsTrue(isValid);
            Assert.AreEqual(100, user.FullName.Length);
        }

        [TestMethod]
        public void FullName_ShouldFailValidationWhenExceeding100Characters()
        {
            // Arrange
            var user = new ApplicationUser();
            user.FullName = new string('A', 101);

            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(
                user, context, results, validateAllProperties: true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.IsTrue(results.Any(r =>
                r.MemberNames.Contains(nameof(ApplicationUser.FullName))));
        }

        [TestMethod]
        public void FullName_ShouldHaveMaxLengthAttributeOf100()
        {
            // Arrange
            var property = typeof(ApplicationUser)
                .GetProperty(nameof(ApplicationUser.FullName));

            // Act
            var attribute = property?.GetCustomAttributes(
                typeof(MaxLengthAttribute), false)
                .FirstOrDefault() as MaxLengthAttribute;

            // Assert
            Assert.IsNotNull(attribute);
            Assert.AreEqual(100, attribute.Length);
        }

        [TestMethod]
        public void FullName_ShouldInitializeAsEmptyString()
        {
            // Arrange & Act
            var user = new ApplicationUser();

            // Assert
            Assert.AreEqual(string.Empty, user.FullName);
        }
    }
}