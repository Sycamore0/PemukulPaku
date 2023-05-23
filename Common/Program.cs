using Config.Net;

namespace Common
{
    public static class Global
    {
        public static IConfig config = new ConfigurationBuilder<IConfig>().UseJsonFile("config.json").Build();
    }

    public interface IConfig
    {
        [Option(DefaultValue = VerboseLevel.Normal)]
        VerboseLevel VerboseLevel { get; }

        [Option(DefaultValue = "mongodb://localhost:27017/crepebh")]
        string DatabaseUri { get; }
        
    }

    public enum VerboseLevel
    {
        Normal = 0,
        Debug = 1
    }
}