namespace snorbert
{
    /// <summary>
    /// Encapsulates one connection
    /// </summary>
    public class Connection
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string ConcentratorIp { get; set; }
        public string CollectionName { get; set; }
    }
}
