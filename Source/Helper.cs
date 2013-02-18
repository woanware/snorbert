using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using woanware;
using System.Text.RegularExpressions;
using System.Web;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(String hex)
        {
            int numChars = hex.Length;
            byte[] bytes = new byte[numChars / 2];
            for (int i = 0; i < numChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        /// <summary>
        /// Helper method for GenerateListColumns()
        /// </summary>
        /// <param name="objectListView"></param>
        /// <param name="data"></param>
        public static void AddListColumn(ObjectListView objectListView, string name, string aspect)
        {
            OLVColumn columnHeader = new OLVColumn();
            columnHeader.Text = name;
            columnHeader.AspectName = aspect;
            objectListView.Columns.Add(columnHeader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="collection"></param>
        /// <param name="srcIp"></param>
        /// <param name="dstIp"></param>
        /// <param name="srcPort"></param>
        /// <param name="dstPort"></param>
        /// <param name="protocol"></param>
        /// <returns></returns>
        public static string ConstructNetWitnessUrl(string ip, 
                                                    string collection, 
                                                    string srcIp, 
                                                    string srcPort, 
                                                    string dstIp, 
                                                    string dstPort,
                                                    string protocol)
        {
            string nw = string.Format("nw://{0}/?collection={1}&time=Last+24+Hours+of+Collection+Time&where=", ip, collection);
            string query = string.Format("(ip.src={0}&&{4}.srcport={1}&&ip.dst={2}&&{4}.dstport={3})", srcIp, srcPort, dstIp, dstPort, protocol.ToLower());
            query += "||";
            query += string.Format("(ip.src={0}&&{4}.srcport={1}&&ip.dst={2}&&{4}.dstport={3})", dstIp, dstPort, srcIp, srcPort, protocol.ToLower());
            return nw + HttpUtility.UrlEncode(query);
        }

        /// <summary>
        /// Update the event data with some extra details e.g. more better displaying etc. It is quicker to process here rather than later on
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public static List<Event> ProcessEventDataSet(List<Event> events)
        {
            foreach (Event temp in events)
            {
                try
                {
                    List<string> flags = new List<string>();
                    foreach (Global.TcpFlags tcpFlag in Misc.EnumToList<Global.TcpFlags>())
                    {
                        if ((temp.TcpFlags & (int)tcpFlag) == (int)tcpFlag)
                        {
                            flags.Add(tcpFlag.GetEnumDescription());
                        }
                    }

                    temp.TcpFlagsString = string.Join("+", flags.ToArray());

                    if (temp.IpProto == (int)Global.Protocols.Tcp)
                    {
                        temp.Protocol = Global.Protocols.Tcp.GetEnumDescription();
                        temp.SrcPort = int.Parse(temp.TcpSrcPort.ToString());
                        temp.DstPort = int.Parse(temp.TcpDstPort.ToString());

                    }
                    else if (temp.IpProto == (int)Global.Protocols.Udp)
                    {
                        temp.Protocol = Global.Protocols.Udp.GetEnumDescription();
                        temp.SrcPort = int.Parse(temp.UdpSrcPort.ToString());
                        temp.DstPort = int.Parse(temp.UdpDstPort.ToString());
                    }
                    else
                    {
                        temp.SrcPort = 0;
                        temp.DstPort = 0;
                    }

                    if (temp.PayloadAscii != null)
                    {
                        temp.PayloadHex = Helper.StringToByteArray(temp.PayloadAscii);
                        temp.PayloadAscii = woanware.Text.ReplaceNulls(woanware.Text.ByteArrayToString(temp.PayloadHex, woanware.Text.EncodingType.Ascii));
                        temp.HttpHost = ParseHost(temp.PayloadAscii);
                    }
                }
                catch (Exception ex)
                {
                    Misc.WriteToEventLog(Application.ProductName, "An error occurred whilst processing the event data: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                }
            }

            return events;
        }
        
        /// <summary>
        /// Parses out the HTTP Host header
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string ParseHost(string data)
        {
            Regex regex = new Regex(@"^Host:\s+(.*)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match match = regex.Match(data);
            if (match.Success == true)
            {
                return match.Groups[1].Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        public static string CopyDataToClipboard(UserControl uc, FastObjectListView list, Global.FieldsEventCopy field)
        {
            if (list.SelectedObjects.Count != 1)
            {
                return string.Empty; 
            }

            Event temp = (Event)list.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(uc, "Unable to locate event");
                return string.Empty; 
            }

            string value = string.Empty;
            switch (field)
            {
                case Global.FieldsEventCopy.Cid:
                    value = temp.Cid.ToString();
                    break;
                case Global.FieldsEventCopy.DstIp:
                    value = temp.IpDst.ToString();
                    break;
                case Global.FieldsEventCopy.DstPort:
                    value = temp.DstPort.ToString();
                    break;
                case Global.FieldsEventCopy.PayloadAscii:
                    value = temp.PayloadAscii.ToString();
                    break;
                case Global.FieldsEventCopy.Sid:
                    value = temp.Sid.ToString();
                    break;
                case Global.FieldsEventCopy.SigName:
                    value = temp.SigName.ToString();
                    break;
                case Global.FieldsEventCopy.SrcIp:
                    value = temp.IpSrc.ToString();
                    break;
                case Global.FieldsEventCopy.SrcPort:
                    value = temp.SrcPort.ToString();
                    break;
                case Global.FieldsEventCopy.Timestamp:
                    value = temp.Timestamp.ToString();
                    break;
            }

            Clipboard.SetText(value);

            if (field != Global.FieldsEventCopy.PayloadAscii)
            {
                //UpdateStatusBar("\"" + value + "\" copied to the clipboard");
            }
            else
            {
                //UpdateStatusBar("Payload (ASCII) copied to the clipboard");
            }

            return string.Empty; 
        }

        /// <summary>
        /// Resizes the event list's columns
        /// </summary>
        public static void ResizeEventListColumns(FastObjectListView list)
        {
            if (list.Items.Count == 0)
            {
                foreach (ColumnHeader column in list.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            else
            {
                list.Columns[(int)Global.FieldsEvent.Cid].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.SrcIp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.DstIp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.Timestamp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }
    }
}
