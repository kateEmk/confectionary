namespace LibraryDatabaseCoffe.Models.DB.Context.Connection
{
    public class ConnectionOptions
    {
        public const string NameConnection = "Connection";
        public string ConnectionString { get; set; } = String.Empty;

        public ConnectionOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ConnectionOptions()
        {
        }

        public override string ToString()
        {
            return ConnectionString.ToString();
        }
    }
}