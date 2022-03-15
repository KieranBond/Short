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

        public ShortenerServiceTests()
        {
            _mockRedis = new Mock<IDatabase>( MockBehavior.Loose );
            _mockRedisConnector = new Mock<IConnectionMultiplexer>();
            _mockRedisConnector.Setup( r => r.GetDatabase( It.IsAny<int>(), It.IsAny<object>() ) ).Returns( _mockRedis.Object );

            _service = new ShortenerService( _mockRedisConnector.Object );
        }

        [Fact]
        public void ShortenUrl_Should_Return_Expected()
        {
            var cmd = new Handle<string>( "" );
            var result = _service.ShortenUrl( cmd );

            result.Should().Be( "" );
        }
    }
}