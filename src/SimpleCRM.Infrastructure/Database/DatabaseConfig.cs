namespace SimpleCRM.Infrastructure.Database
{
    public class DatabaseConfig
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string? Default { get; set; }
    }
}
