using Common.Utils;
using Config.Net;
using MongoDB.Driver;

namespace Common
{
    public static class Global
    {
        public static readonly IConfig config = new ConfigurationBuilder<IConfig>().UseJsonFile("config.json").Build();
        public static readonly Logger c = new("Global");

        public static readonly MongoClient MongoClient = new(config.DatabaseUri);
        public static readonly IMongoDatabase db = MongoClient.GetDatabase("PemukulPaku");
        public static long GetUnixInSeconds() => ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
    }

    public interface IConfig
    {
        [Option(DefaultValue = VerboseLevel.Normal)]
        VerboseLevel VerboseLevel { get; }

        [Option(DefaultValue = false)]
        bool UseLocalCache { get; }

        [Option(DefaultValue = true)]
        bool CreateAccountOnLoginAttempt { get; }

        [Option(DefaultValue = "mongodb://localhost:27017/PemukulPaku")]
        string DatabaseUri { get; }

        [Option]
        IGameserver Gameserver { get; }

        [Option]
        IHttp Http { get; }

        public interface IGameserver
        {
            [Option(DefaultValue = "127.0.0.1")]
            public string Host { get; set; }

            [Option(DefaultValue = (uint)(16100))]
            public uint Port { get; }

            [Option(DefaultValue = "overseas01")]
            public string RegionName { get; }
        }

        public interface IHttp
        {

            [Option(DefaultValue = (uint)(80))]
            public uint HttpPort { get; }

            [Option(DefaultValue = (uint)(443))]
            public uint HttpsPort { get; }
        }
    }

    public enum VerboseLevel
    {
        Silent = 0,
        Normal = 1,
        Debug = 2
    }
}