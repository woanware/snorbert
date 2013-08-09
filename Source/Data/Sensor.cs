using NPoco;

namespace snorbert.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class Sensor
    {
        #region Member Variables
        public uint Sid { get; set; }
        public string HostName { get; set; }
        [Column("inter")]
        public string Interface { get; set; }
        [Column("timestamp")]
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
