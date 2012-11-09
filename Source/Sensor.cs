namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class Sensor
    {
        #region Member Variables
        public uint Sid { get; set; }
        public string HostName { get; set; }
        public string Interface { get; set; }
        public string LastEvent { get; set; }
        public long EventCount { get; set; }
        public int EventPercentage { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Sensor()
        {
            HostName = string.Empty;
            Interface = string.Empty;
            LastEvent = string.Empty;
        }
        #endregion
    }
}
