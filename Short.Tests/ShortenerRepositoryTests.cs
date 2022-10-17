using AutoFixture;
using FluentAssertions;
using Moq;
using Short.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Short.Tests
{
    public class ShortenerRepositoryTests
    {
        readonly Mock<IConnectionMultiplexer> _mockRedisConnector;
        readonly Mock<IDatabase> _mockRedis;
        readonly Fixture _fixture;

        readonly ShortenerRepository _subject;

        const string DEFAULT_CACHED_URL = "SOME_URL";

        public ShortenerRepositoryTests()
        {
            _fixture = new Fixture();

            _mockRedis = new Mock<IDatabase>( MockBehavior.Loose );
            _mockRedisConnector = new Mock<IConnectionMultiplexer>();
            _mockRedisConnector.Setup( r => r.GetDatabase( It.IsAny<int>(), It.IsAny<object>() ) ).Returns( _mockRedis.Object );
            
            _subject = new ShortenerRepository( _mockRedisConnector.Object );
        }

        [Fact]
        public void Provides_Output_On_TryRetrieve_When_Found()
        {
            _mockRedis.Setup( r => r.KeyExists( It.IsAny<RedisKey>(), It.IsAny<CommandFlags>() ) ).Returns( true ).Verifiable();
            _mockRedis.Setup( r => r.StringGet( It.IsAny<RedisKey>(), It.IsAny<CommandFlags>() ) ).Returns( DEFAULT_CACHED_URL ).Verifiable();

            var success = _subject.TryRetrieve( _fixture.Create<string>(), out var result );

            success.Should().BeTrue();
            result.Should().Be( DEFAULT_CACHED_URL );

            _mockRedis.Verify();
        }

        [Theory]
        [InlineData( true, true )]
        [InlineData( false, false )]
        public void Save_Returns_Result( bool mockReturn, bool expected )
        {
            var originalUrl = _fixture.Create<string>();
            var shortenedUrl = _fixture.Create<string>();

            _mockRedis
                .Setup( r => r.StringSet( originalUrl, shortenedUrl, It.IsAny<TimeSpan>(), It.IsAny<When>(), It.IsAny<CommandFlags>() ) )
                .Returns( mockReturn )
                .Verifiable();

            var result = _subject.Save( shortenedUrl, originalUrl );

            result.Should().Be( expected );
            _mockRedis.Verify();
        }

        [Fact]
        public void Save_Caches_Provided_Values()
        {
            var originalUrl = _fixture.Create<string>();
            var shortenedUrl = _fixture.Create<string>();

            _mockRedis
                .Setup( r => r.StringSet( originalUrl, shortenedUrl, It.IsAny<TimeSpan>(), It.IsAny<When>(), It.IsAny<CommandFlags>() ) )
                .Returns( true )
                .Verifiable();

            _subject.Save( shortenedUrl, originalUrl );
            _mockRedis.Verify();
        }
    }
}
