namespace Short.Config
{
    public record RedisConfig
    {
        public readonly string Url;

        public RedisConfig ( string url )
        {
            Url = url;
        }

     }
}
