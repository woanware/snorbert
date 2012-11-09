using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using woanware;

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
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Event> LoadEventDataSet(dynamic data)
        {
            List<Event> ret = new List<Event>();
            int count = 0;
            foreach (var item in data)
            {
                try
                {
                    Event temp = new Event();
                    temp.Cid = item.cid;
                    temp.Sid = long.Parse(item.sig_sid.ToString());
                    temp.IpSrc = IPAddress.Parse(item.ip_src.ToString());
                    temp.IpDst = IPAddress.Parse(item.ip_dst.ToString());
                    temp.TcpSrcPort = int.Parse(item.tcp_sport.ToString());
                    temp.TcpDstPort = int.Parse(item.tcp_dport.ToString());
                    temp.UdpSrcPort = int.Parse(item.udp_sport.ToString());
                    temp.UdpDstPort = int.Parse(item.udp_dport.ToString());
                    temp.IpVer = int.Parse(item.ip_ver.ToString());
                    temp.IpHlen = int.Parse(item.ip_hlen.ToString());
                    temp.IpTos = int.Parse(item.ip_tos.ToString());
                    temp.IpLen = int.Parse(item.ip_len.ToString());
                    temp.IpId = int.Parse(item.ip_id.ToString());
                    temp.IpFlags = int.Parse(item.ip_flags.ToString());
                    temp.IpOff = int.Parse(item.ip_off.ToString());
                    temp.IpTtl = int.Parse(item.ip_ttl.ToString());
                    temp.IpProto = int.Parse(item.ip_proto.ToString());
                    temp.IpCsum = int.Parse(item.ip_csum.ToString());
                    temp.Timestamp = item.timestamp;
                    temp.SigName = item.sig_name;
                    temp.SigGid = item.sig_gid;
                    temp.SigPriority = item.sig_priority;
                    temp.SigRev = item.sig_rev;
                    temp.SigClassName = item.sig_class_name;
                    temp.TcpSeq = long.Parse(item.tcp_seq.ToString());
                    temp.TcpAck = long.Parse(item.tcp_ack.ToString());
                    temp.TcpOff = int.Parse(item.tcp_off.ToString());
                    temp.TcpRes = int.Parse(item.tcp_res.ToString());
                    temp.TcpFlags = int.Parse(item.tcp_flags.ToString());

                    List<string> flags = new List<string>();
                    foreach (Global.TcpFlags tcpFlag in Misc.EnumToList<Global.TcpFlags>())
                    {
                        if ((temp.TcpFlags & (int)tcpFlag) == (int)tcpFlag)
                        {
                            flags.Add(tcpFlag.GetEnumDescription());
                        }
                    }

                    temp.TcpFlagsString = string.Join("+", flags.ToArray());

                    temp.TcpWin = int.Parse(item.tcp_win.ToString());
                    temp.TcpCsum = int.Parse(item.tcp_csum.ToString());
                    temp.TcpUrp = int.Parse(item.tcp_urp.ToString());
                    temp.UdpLen = int.Parse(item.udp_len.ToString());
                    temp.UdpCsum = int.Parse(item.udp_csum.ToString());

                    if (temp.IpProto == (int)Global.Protocols.Tcp)
                    {
                        temp.Protocol = Global.Protocols.Tcp.GetEnumDescription();
                        temp.SrcPort = int.Parse(item.tcp_sport.ToString());
                        temp.DstPort = int.Parse(item.tcp_dport.ToString());

                    }
                    else if (temp.IpProto == (int)Global.Protocols.Udp)
                    {
                        temp.Protocol = Global.Protocols.Udp.GetEnumDescription();
                        temp.SrcPort = int.Parse(item.udp_sport.ToString());
                        temp.DstPort = int.Parse(item.udp_dport.ToString());
                    }
                    else
                    {
                        temp.SrcPort = 0;
                        temp.DstPort = 0;
                    }

                    if (item.data_payload != null)
                    {
                        temp.PayloadHex = Helper.StringToByteArray(item.data_payload.ToString());
                        temp.PayloadAscii = woanware.Text.ReplaceNulls(woanware.Text.ByteArrayToString(temp.PayloadHex, woanware.Text.EncodingType.Ascii));
                    }

                    ret.Add(temp);

                    count++;
                }
                catch (Exception ex)
                {
                    Misc.WriteToEventLog(Application.ProductName, "An error occurred whilst parsing the event data: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                }
            }

            return ret;
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
