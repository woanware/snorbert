using System;
using System.ComponentModel;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class Global
    {
        #region Event Delegates
        public delegate void MessageEvent(string message);
        public delegate void DefaultEvent();
        #endregion

        #region Constants
        public const string PRIORITIES_FILE = "Priorities.txt";
        public const string RULES_DB = "Rules.db";
        #endregion

        #region Enums
        /// <summary>
        /// 
        /// </summary>
        public enum Protocols : int
        {
            [Description("ICMP")]
            Icmp = 1,
            [Description("TCP")]
            Tcp = 6,
            [Description("UDP")]
            Udp = 17
        }

        [Flags]
        public enum TcpFlags
        {
            [Description("FIN")]
            FIN = 0x01,
            [Description("SYN")]
            SYN = 0x02,
            [Description("RST")]
            RST = 0x04,
            [Description("PSH")]
            PSH = 0x08,
            [Description("ACK")]
            ACK = 0x10,
            [Description("URG")]
            URG = 0x20,
            [Description("ECE")]
            ECE = 0x40,
            [Description("CWR")]
            CWR = 0x80,
            [Description("NS")]
            NS = 0x160
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FieldsEvent : int
        {
            SrcIp = 0,
            SrcPort = 1,
            DstIp = 2,
            DstPort = 3,
            Protocol = 4,
            Host = 5,
            Timestamp = 6,
            TcpFlags = 7,
            Classification = 8,
            Initials = 9,
            Payload = 10
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FieldsEventCopy : int
        {
            Sid,
            SrcIp,
            SrcPort,
            DstIp,
            DstPort,
            SigName,
            Timestamp,
            PayloadAscii,
            HttpHost,
            Cid
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FieldsReferences : int
        {
            Type = 0,
            Data = 1
        }

        /// <summary>
        /// 
        /// </summary>
        public enum FilterType
        {
            Numeric,
            Text,
            Ip,
            Timestamp,
            Severity,
            //SignatureName,
            //SignatureId,
            Classification,
            PayloadAscii,
            PayloadHex,
            Sensor,
            Protocol
        }
        #endregion
    }
}
