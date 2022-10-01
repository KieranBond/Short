using AutoFixture;
using CQRS.Commands;
using FluentAssertions;
using Moq;
using Short.Repositories;
using Short.Services;
using Xunit;

namespace Short.Tests
{
    public class ShortenerServiceTests
    {
        readonly ShortenerService _service;
        readonly Mock<IShortenerRepository> _mockRepository;
        readonly Fixture _fixture;

        public ShortenerServiceTests()
        {
            _fixture = new Fixture();

            _mockRepository = new Mock<IShortenerRepository>();

            _service = new ShortenerService( _mockRepository.Object );
        }

        [Fact]
        public void ShortenUrl_Should_Return_Expected()
        {
            // Setup
            var testCmd = _fixture.Create<Handle<string>>();
            
            // Act
            var result = _service.ShortenUrl( testCmd );

            // Assert
            result.Should().NotBe( testCmd.Dto );
            result.Should().HaveLength( 5 );
        }

        [Fact]
        public void ShortenUrl_Should_Query_Repository_For_Existing()
        {
            // Setup
            var testCmd = _fixture.Create<Handle<string>>();
            var cachedResult = _fixture.Create<string>();

            _mockRepository
                .Setup( r => r.TryRetrieve( It.IsAny<string>(), out cachedResult ) )
                .Verifiable();

            // Act
            var result = _service.ShortenUrl( testCmd );

            // Assert
            result.Should().Be( cachedResult );
            _mockRepository.Verify();
        }
    }
}