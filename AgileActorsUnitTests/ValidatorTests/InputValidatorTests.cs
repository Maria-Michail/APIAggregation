using AgileActorsApp.Validator;

namespace AgileActorsUnitTests.ValidatorTests
{
    [TestClass]
    public class InputValidatorTests
    {
        [TestMethod]
        public void ValidateCountry_ValidCountry_ReturnsCountryCode()
        {
            // Arrange
            string validCountry = "us";

            // Act
            var result = InputValidator.ValidateCountry(validCountry);

            // Assert
            Assert.AreEqual(validCountry, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateCountry_InvalidCountry_ThrowsValidationException()
        {
            // Arrange
            string invalidCountry = "xyz";

            // Act
            InputValidator.ValidateCountry(invalidCountry);
        }

        [TestMethod]
        public void ValidateCategory_ValidCategory_ReturnsCategory()
        {
            // Arrange
            string validCategory = "business";

            // Act
            var result = InputValidator.ValidateCategory(validCategory);

            // Assert
            Assert.AreEqual(validCategory, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateCategory_InvalidCategory_ThrowsValidationException()
        {
            // Arrange
            string invalidCategory = "invalidCategory";

            // Act
            InputValidator.ValidateCategory(invalidCategory);
        }

        [TestMethod]
        public void ValidateDate_ValidDate_ReturnsDate()
        {
            // Arrange
            DateTime validDate = new DateTime(2020, 1, 1);

            // Act
            var result = InputValidator.ValidateDate(validDate);

            // Assert
            Assert.AreEqual(validDate, result);
        }

        [TestMethod]
        public void ValidateDate_MinDate_ReturnsDate()
        {
            // Arrange
            DateTime validDate = new DateTime(1979, 1, 2);

            // Act
            var result = InputValidator.ValidateDate(validDate);

            // Assert
            Assert.AreEqual(validDate, result);
        }

        [TestMethod]
        public void ValidateDate_MaxDate_ReturnsDate()
        {
            // Arrange
            DateTime validDate = DateTime.UtcNow;

            // Act
            var result = InputValidator.ValidateDate(validDate);

            // Assert
            Assert.AreEqual(validDate.Date, result.Date);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateDate_TooEarlyDate_ThrowsValidationException()
        {
            // Arrange
            DateTime invalidDate = new DateTime(1978, 12, 31);

            // Act
            InputValidator.ValidateDate(invalidDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void ValidateDate_TooLateDate_ThrowsValidationException()
        {
            // Arrange
            DateTime invalidDate = DateTime.UtcNow.AddDays(1);

            // Act
            InputValidator.ValidateDate(invalidDate);
        }

        [TestMethod]
        public void ValidateDate_MinValue_ReturnsToday()
        {
            // Arrange
            DateTime minValueDate = DateTime.MinValue;

            // Act
            var result = InputValidator.ValidateDate(minValueDate);

            // Assert
            Assert.AreEqual(DateTime.Today, result);
        }

        [TestMethod]
        public void ValidateCountry_NullCountry_ReturnsNull()
        {
            // Act
            var result = InputValidator.ValidateCountry(null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ValidateCategory_NullCategory_ReturnsNull()
        {
            // Act
            var result = InputValidator.ValidateCategory(null);

            // Assert
            Assert.IsNull(result);
        }
    }
}
