using System;
using System.Net;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class Event
    {
        #region Member Variables/Properties
        public long Cid { get; set; }
        public long Sid { get; set; }
        public IPAddress IpSrc { get; set; }
        public IPAddress IpDst { get; set; }
        public int TcpSrcPort { get; set; }
        public int TcpDstPort { get; set; }
        public int UdpSrcPort { get; set; }
        public int UdpDstPort { get; set; }
        public int SrcPort { get; set; }
        public int DstPort { get; set; }
        public int IpVer { get; set; }
        public int IpHlen { get; set; }
        public int IpTos { get; set; }
        public int IpLen { get; set; }
        public int IpId { get; set; }
        public int IpFlags { get; set; }
        public int IpOff { get; set; }
        public int IpTtl { get; set; }
        public int IpProto{ get; set; }
        public int IpCsum { get; set; }
        public string Protocol { get; set; }
        public DateTime Timestamp { get; set; }
        public string SigName { get; set; }
        public long SigPriority { get; set; }
        public long SigRev { get; set; }
        public long SigGid{ get; set; }
        public string SigClassName { get; set; }
        public long TcpSeq { get; set; }
        public long TcpAck { get; set; }
        public int TcpOff { get; set; }
        public int TcpRes { get; set; }
        public int TcpFlags { get; set; }
        public string TcpFlagsString { get; set; }
        public int TcpWin { get; set; }
        public int TcpCsum { get; set; }
        public int TcpUrp { get; set; }
        public int UdpLen { get; set; }
        public int UdpCsum { get; set; }
        public byte[] PayloadHex { get; set; }
        public string PayloadAscii { get; set; }
        public string HttpHost { get; set; }
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
        }
        #endregion
    }
}
