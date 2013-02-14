using System;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class Exclude
    {
        #region Member Variables
        public long Id { get; set; }
        public string SourceIp { get; set; }
        public string DestinationIp { get; set; }
        public string Rule { get; set; }
        public long SigId { get; set; }
        public long SigSid { get; set; }
        public string Comment { get; set; }
        public bool FalsePositive { get; set; }
        public DateTime Timestamp { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Exclude()
        {
            SourceIp = string.Empty;
            DestinationIp = string.Empty;
            Rule = string.Empty;
            Comment = string.Empty;
        }
        #endregion
    }
}
