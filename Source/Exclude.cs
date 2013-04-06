using NPoco;
using System;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("exclude")] 
    public class Exclude
    {
        #region Member Variables
        [Column("id")]
        public long Id { get; set; }
        [Column("ip_src")]
        public UInt32 SourceIp { get; set; }
        [Column("ip_dst")]
        public UInt32 DestinationIp { get; set; }
        [Ignore]
        public string Rule { get; set; }
        [Column("sig_id")]
        public long SigId { get; set; }
        [Ignore]
        public long SigSid { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
        [Column("fp")]
        public bool FalsePositive { get; set; }
        [Column("timeadded")]
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

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ret = "Source IP: " + SourceIpText + Environment.NewLine;
            ret += "Destination IP: " + DestinationIpText + Environment.NewLine;
            ret += "Rule: " + Rule + Environment.NewLine;
            ret += "Comment: " + Comment;
            return ret;
        }
        #endregion
    }
}
