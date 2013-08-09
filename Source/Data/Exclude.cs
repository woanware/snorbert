using NPoco;
using System;

namespace snorbert.Data
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
        [Column("port_src")]
        public UInt16 SourcePort { get; set; }
        [Column("ip_dst")]
        public UInt32 DestinationIp { get; set; }
        [Column("port_dst")]
        public UInt16 DestinationPort { get; set; }
        [Column("ip_proto")]
        public UInt32 IpProto { get; set; }
        [Ignore]
        public string Protocol { get; set; }
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
        [Ignore]
        public string SourcePortText { get; set; }
        [Ignore]
        public string DestinationPortText { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        
        public Exclude()
        {
            SourceIpText = string.Empty;
            DestinationIpText = string.Empty;
            SourcePortText = string.Empty;
            DestinationPortText = string.Empty;
            Protocol = string.Empty;
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
            string ret = string.Empty;
            if (SourcePortText != string.Empty)
            {
                ret = "Source: " + SourceIpText + ":" + SourcePortText + Environment.NewLine;
            }
            else
            {
                ret = "Source: " + SourceIpText + Environment.NewLine;
            }

            if (DestinationPortText != string.Empty)
            {
                ret = "Destination: " + DestinationIpText + ":" + DestinationPortText + Environment.NewLine;
            }
            else
            {
                ret = "Destination: " + DestinationIpText + Environment.NewLine;
            }

            ret += "Protocol: " + Protocol + Environment.NewLine;

            ret += "Rule: " + Rule + Environment.NewLine;
            ret += "Comment: " + Comment;
            return ret;
        }
        #endregion
    }
}
