using System;
using System.Net;
using NPoco;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class Event
    {
        #region Member Variables/Properties
        [Column("cid")]
        public long Cid { get; set; }
        [Column("sid")]
        public long Sid { get; set; }
        [Column("sig_sid")]
        public long SigSid { get; set; }
        [Column("ip_src")]
        public string IpSrcTxt { get; set; }
        [Column("ip_dst")]
        public string IpDstTxt { get; set; }
        [Ignore]
        public IPAddress IpSrc { get; set; }
        [Ignore]
        public IPAddress IpDst { get; set; }
        [Column("tcp_sport")]
        public int TcpSrcPort { get; set; }
        [Column("tcp_dport")]
        public int TcpDstPort { get; set; }
        [Column("udp_sport")]
        public int UdpSrcPort { get; set; }
        [Column("udp_dport")]
        public int UdpDstPort { get; set; }
        [Ignore]
        public int SrcPort { get; set; }
        [Ignore]
        public int DstPort { get; set; }
        [Column("ip_ver")]
        public int IpVer { get; set; }
        [Column("ip_hlen")]
        public int IpHlen { get; set; }
        [Column("ip_tos")]
        public int IpTos { get; set; }
        [Column("ip_len")]
        public int IpLen { get; set; }
        [Column("ip_id")]
        public int IpId { get; set; }
        [Column("ip_flags")]
        public int IpFlags { get; set; }
        [Column("ip_off")]
        public int IpOff { get; set; }
        [Column("ip_ttl")]
        public int IpTtl { get; set; }
        [Column("ip_proto")]
        public int IpProto{ get; set; }
        [Column("ip_csum")]
        public int IpCsum { get; set; }
        [Ignore]
        public string Protocol { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        [Column("sig_name")]
        public string SigName { get; set; }
        [Column("sig_priority")]
        public long SigPriority { get; set; }
        [Column("sig_rev")]
        public long SigRev { get; set; }
        [Column("sig_gid")]
        public long SigGid{ get; set; }
        [Column("sig_class_name")]
        public string SigClassName { get; set; }
        [Column("tcp_seq")]
        public long TcpSeq { get; set; }
        [Column("tcp_ack")]
        public long TcpAck { get; set; }
        [Column("tcp_off")]
        public int TcpOff { get; set; }
        [Column("tcp_res")]
        public int TcpRes { get; set; }
        [Column("tcp_flags")]
        public int TcpFlags { get; set; }
        [Ignore]
        public string TcpFlagsString { get; set; }
        [Column("tcp_win")]
        public int TcpWin { get; set; }
        [Column("tcp_csum")]
        public int TcpCsum { get; set; }
        [Column("tcp_urp")]
        public int TcpUrp { get; set; }
        [Column("udp_len")]
        public int UdpLen { get; set; }
        [Column("udp_csum")]
        public int UdpCsum { get; set; }
        [Ignore]
        public byte[] PayloadHex { get; set; }
        [Column("data_payload")]
        public string PayloadAscii { get; set; }
        [Ignore]
        public string HttpHost { get; set; }
        [Column("acknowledgment_id")]
        public long AcknowledgmentId { get; set; }
        [Column("initials")]
        public string Initials { get; set; }
        [Column("acknowledgment_class")]
        public string AcknowledgmentClass { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Event()
        {
            SigName = string.Empty;
            SigClassName = string.Empty;
            PayloadAscii = string.Empty;
            TcpFlagsString = string.Empty;
            Initials = string.Empty;
            AcknowledgmentClass = string.Empty;
        }
        #endregion
    }
}
