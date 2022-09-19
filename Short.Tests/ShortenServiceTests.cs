using AutoFixture;
using CQRS.Commands;
using FluentAssertions;
using Moq;
using Short.Services;
using StackExchange.Redis;
using Xunit;

namespace Short.Tests
{
    public class ShortenerServiceTests
    {
        readonly Mock<IConnectionMultiplexer> _mockRedisConnector;
        readonly Mock<IDatabase> _mockRedis;
        readonly ShortenerService _service;
        readonly Fixture _fixture;

        public ShortenerServiceTests()
        {
            _fixture = new Fixture();

            _mockRedis = new Mock<IDatabase>( MockBehavior.Loose );
            _mockRedisConnector = new Mock<IConnectionMultiplexer>();
            _mockRedisConnector.Setup( r => r.GetDatabase( It.IsAny<int>(), It.IsAny<object>() ) ).Returns( _mockRedis.Object );
            _mockRedis.Setup( r => r.KeyExists( It.IsAny<RedisKey>(), It.IsAny<CommandFlags>() ) ).Returns( false );

            _service = new ShortenerService( _mockRedisConnector.Object );
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
        public void ShortenUrl_Should_Return_Cached_If_Exists()
        {
            // Setup
            var testCmd = _fixture.Create<Handle<string>>();
            var cachedResult = _fixture.Create<string>();

            _mockRedis
                .Setup( r => r.KeyExists( It.IsAny<RedisKey>(), It.IsAny<CommandFlags>() ) )
                .Returns( true );

            _mockRedis
                .Setup( r => r.StringGet( It.Is<RedisKey>( s => s.ToString() == testCmd.Dto ), It.IsAny<CommandFlags>() ) )
                .Returns( cachedResult );

            // Act
            var result = _service.ShortenUrl( testCmd );

            // Assert
            result.Should().Be( cachedResult );
        }
    }
}