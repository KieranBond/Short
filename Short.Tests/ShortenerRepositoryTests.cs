using AutoFixture;
using Moq;
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

        public ShortenerRepositoryTests ()
        {
            _fixture = new Fixture();

            _mockRedis = new Mock<IDatabase>( MockBehavior.Loose );
            _mockRedisConnector = new Mock<IConnectionMultiplexer>();
            _mockRedisConnector.Setup( r => r.GetDatabase( It.IsAny<int>(), It.IsAny<object>() ) ).Returns( _mockRedis.Object );
            _mockRedis.Setup( r => r.KeyExists( It.IsAny<RedisKey>(), It.IsAny<CommandFlags>() ) ).Returns( false );
        }
    }
}
