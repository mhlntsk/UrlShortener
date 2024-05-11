using NSubstitute;
using Shortener.Business.Models;
using Shortener.Business.Services;
using Shortener.Business.Validation;

namespace Shortener.Business.Tests
{
    public class UrlServiceTests : BaseTest
    {
        UrlService urlService;
        DataInitializer dataInitializer = new DataInitializer();

        [OneTimeSetUp]
        public void RunOnceBeforeAnyTests()
        {            
            urlService = new UrlService(unitOfWork, mapper);
            dataInitializer = new DataInitializer();
        }

        [TestCaseSource(typeof(DataInitializer), nameof(DataInitializer.GetValidUrlShortenerModels))]
        public void Validate_WithValidData_ShouldNotThrowAnException(UrlShortenerModel model)
        {
            // Arrange
            
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                UrlService.Validate(model);
            });
        }

        [TestCaseSource(typeof(DataInitializer), nameof(DataInitializer.GetUrlShortenerModelsWithInvalidFullUrl))]
        public void Validate_WithInvalidFullUrl_ShouldThrowAShortenerException(UrlShortenerModel invalidModel)
        {
            // Arrange

            // Act & Assert
            ShortenerException ex = Assert.Throws<ShortenerException>(() => UrlService.Validate(invalidModel));
            Assert.That(ex.Message, Is.EqualTo("Invalid UrlShortenerModel.FullUrl"));
        }

        [TestCaseSource(typeof(DataInitializer), nameof(DataInitializer.GetUrlShortenerModelsWithInvalidNumberOfAppeals))]
        public void Validate_WithInvalidNumberOfAppeals_ShouldThrowAShortenerException(UrlShortenerModel invalidModel)
        {
            // Arrange

            // Act & Assert
            ShortenerException ex = Assert.Throws<ShortenerException>(() => UrlService.Validate(invalidModel));
            Assert.That(ex.Message, Is.EqualTo("Invalid UrlShortenerModel.NumberOfAppeals"));
        }

        [TestCaseSource(typeof(DataInitializer), nameof(DataInitializer.GetUrlShortenerModelsWithInvalidCreatedDate))]
        public void Validate_WithInvalidCreatedDate_ShouldThrowAShortenerException(UrlShortenerModel invalidModel)
        {
            // Arrange

            // Act & Assert
            ShortenerException ex = Assert.Throws<ShortenerException>(() => UrlService.Validate(invalidModel));
            Assert.That(ex.Message, Is.EqualTo("Invalid UrlShortenerModel.CreatedDate"));
        }

        [TestCaseSource(typeof(DataInitializer), nameof(DataInitializer.GetUrlShortenerModelsWithInvalidLastAppeal))]
        public void Validate_WithInvalidLastAppeal_ShouldThrowAShortenerException(UrlShortenerModel invalidModel)
        {
            // Arrange

            // Act & Assert
            ShortenerException ex = Assert.Throws<ShortenerException>(() => UrlService.Validate(invalidModel));
            Assert.That(ex.Message, Is.EqualTo("Invalid UrlShortenerModel.LastAppeal"));
        }

        [TestCaseSource(typeof(DataInitializer), nameof(DataInitializer.GetUrlShortenerModelsWithInvalidUserId))]
        public void Validate_WithInvalidUserId_ShouldThrowAShortenerException(UrlShortenerModel invalidModel)
        {
            // Arrange

            // Act & Assert
            ShortenerException ex = Assert.Throws<ShortenerException>(() => UrlService.Validate(invalidModel));
            Assert.That(ex.Message, Is.EqualTo("Invalid UrlShortenerModel.UserId"));
        }
    }
}