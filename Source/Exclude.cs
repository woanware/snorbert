using NPoco;
using System;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class Exclude
    {
        #region Member Variables
        [Column("id")]
        public long Id { get; set; }
        [Column("ip_src")]
        public UInt32 SourceIp { get; set; }
        [Column("ip_dst")]
        public UInt32 DestinationIp { get; set; }
        [Column("sig_name")]
        public string Rule { get; set; }
        [Column("sig_sid")]
        public long SigId { get; set; }
        [Column("emailAddress")]
        public long SigSid { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("fp")]
        public bool FalsePositive { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        [Ignore]
        public string SourceIpText { get; set; }
        [Ignore]
        public string DestinationIpText { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Exclude()
        {
            SourceIpText = string.Empty;
            DestinationIpText = string.Empty;
            Rule = string.Empty;
            Comment = string.Empty;
        }
        #endregion
    }
}
