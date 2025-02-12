using AgileActorsApp.Services.Mapping;
using NewsAPI.Constants;

namespace AgileActorsUnitTests.ServicesTests.MappingTests
{
    [TestClass]
    public class CategoryMappingTests
    {
        [TestMethod]
        [DataRow("business", Categories.Business)]
        [DataRow("entertainment", Categories.Entertainment)]
        [DataRow("health", Categories.Health)]
        [DataRow("science", Categories.Science)]
        [DataRow("sports", Categories.Sports)]
        [DataRow("technology", Categories.Technology)]
        public void GetCategoryEnum_ValidCategory_ReturnsCorrectEnum(string input, Categories? expected)
        {
            // Act
            var result = CategoryMapping.GetCategoryEnum(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("invalidcategory")]
        public void GetCategoryEnum_InvalidCategory_ReturnsNull(string input)
        {
            // Act
            var result = CategoryMapping.GetCategoryEnum(input);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetCategoryEnum_EmptyString_ReturnsNull()
        {
            // Act
            var result = CategoryMapping.GetCategoryEnum("");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetCategoryEnum_NullInput_ReturnsNull()
        {
            // Act
            var result = CategoryMapping.GetCategoryEnum(null);

            // Assert
            Assert.IsNull(result);
        }
    }
}
