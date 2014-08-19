using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using woanware;
using snorbert.Data;

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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ip"></param>
        ///// <param name="collection"></param>
        ///// <param name="srcIp"></param>
        ///// <param name="dstIp"></param>
        ///// <param name="srcPort"></param>
        ///// <param name="dstPort"></param>
        ///// <param name="protocol"></param>
        ///// <returns></returns>
        //public static string ConstructNetWitnessUrl(string ip, 
        //                                            string collection, 
        //                                            string srcIp, 
        //                                            string srcPort, 
        //                                            string dstIp, 
        //                                            string dstPort,
        //                                            string protocol)
        //{
        //    string nw = string.Format("nw://{0}/?collection={1}&time=Last+24+Hours+of+Collection+Time&where=", ip, collection);
        //    string query = string.Format("%28ip.src={0}&{4}.srcport={1}&ip.dst={2}&{4}.dstport={3}%29", srcIp, srcPort, dstIp, dstPort, protocol.ToLower());
        //    query += "||";
        //    query += string.Format("%28ip.src={0}&{4}.srcport={1}&ip.dst={2}&{4}.dstport={3}%29", dstIp, dstPort, srcIp, srcPort, protocol.ToLower());

        //    return nw + HttpUtility.UrlEncode(query);
        //}

        /// <summary>
        /// Generates the following query:
        /// 
        /// (ip.src={0}||ip.dst={2})&&(ip.src={2}||ip.dst={0})&&({4}.srcport={1}||{4}.dstport={3})&&({4}.srcport={3}||{4}.dstport={1})
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="collection"></param>
        /// <param name="srcIp"></param>
        /// <param name="srcPort"></param>
        /// <param name="dstIp"></param>
        /// <param name="dstPort"></param>
        /// <param name="protocol"></param>
        /// <param name="srcToDst"></param>
        /// <returns></returns>
        public static string ConstructNetWitnessUrl(string ip,
                                                    string collection,
                                                    string srcIp,
                                                    string srcPort,
                                                    string dstIp,
                                                    string dstPort,
                                                    string protocol)
        {
            //const string query = "%28ip.src%3d{0}%26%26{4}.srcport%3d{1}%26%26ip.dst%3d{2}%26%26{4}.dstport%3d{3}%29";
            //const string query = "%28ip.src={0}||ip.dst={2}%29%26%26%28ip.src={2}||ip.dst={0}%29%26%26%28{4}.srcport={1}||{4}.dstport={3}%29%26%26%28{4}.srcport={3}||{4}.dstport={1}%29";
            const string query = "ip.src%3d{0}%2c{2}+%26%26+ip.dst%3d{0}%2c{2}+%26%26+{4}.srcport%3d{1}%2c{3}+%26%26+{4}.dstport%3d{1}%2c{3}";
            string nw = string.Format("nws://{0}/?collection={1}&time=Last+24+Hours+of+Collection+Time&where=", ip, collection);
            string temp = string.Format(query, srcIp, srcPort, dstIp, dstPort, protocol.ToLower());
            string name = "&name=Snorbert:+" + temp;
            return nw + temp + name;
        }

        /// <summary>
        /// Generates the following query:
        /// 
        /// (ip.src={0}||ip.dst={2})&&(ip.src={2}||ip.dst={0})&&({4}.srcport={1}||{4}.dstport={3})&&({4}.srcport={3}||{4}.dstport={1})
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="collection"></param>
        /// <param name="srcIp"></param>
        /// <param name="srcPort"></param>
        /// <param name="dstIp"></param>
        /// <param name="dstPort"></param>
        /// <param name="protocol"></param>
        /// <param name="srcToDst"></param>
        /// <returns></returns>
        public static string ConstructNetWitnessUrl(string nwIp,
                                                    string collection,
                                                    string ip,
                                                    DateTime timestamp)
        {
            DateTime priorTimestamp = timestamp.AddMinutes(-2);
            DateTime postTimestamp = timestamp.AddMinutes(+1);
            string time = string.Format("{0}-{1}-{2}+{3}%3a{4}+{5}++to++",
                                        priorTimestamp.ToString("yyyy"),
                                        priorTimestamp.ToString("MMM"),
                                        priorTimestamp.ToString("dd"),
                                        priorTimestamp.ToString("hh"),
                                        priorTimestamp.ToString("mm"),
                                        priorTimestamp.ToString("tt ").Trim());

            time += string.Format("{0}-{1}-{2}+{3}%3a{4}+{5}",
                                  postTimestamp.ToString("yyyy"),
                                  postTimestamp.ToString("MMM"),
                                  postTimestamp.ToString("dd"),
                                  postTimestamp.ToString("hh"),
                                  postTimestamp.ToString("mm"),
                                  postTimestamp.ToString("tt ").Trim());

            const string query = "%28ip.src%3d{0}+%7c%7c+ip.dst%3d{0}%29";
            string nw = string.Format("nws://{0}/?collection={1}&time={2}&where=", nwIp, collection, time);
            string temp = string.Format(query, ip);
            string name = "&name=Snorbert:+" + temp;
            return nw + temp + name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="srcIp"></param>
        /// <param name="srcPort"></param>
        /// <param name="dstIp"></param>
        /// <param name="dstPort"></param>
        /// <param name="protocol"></param>
        /// <param name="sensorId"></param>
        /// <returns></returns>
        public static string ConstructCommand(string command,
                                              string srcIp,
                                              string srcPort,
                                              string dstIp,
                                              string dstPort,
                                              string protocol,
                                              string sensorId,
                                              string sensorName,
                                              string timestamp)
        {
            string temp = command;

            temp = temp.Replace("#IP_SRC#", srcIp);
            temp = temp.Replace("#IP_DST#", dstIp);
            temp = temp.Replace("#PORT_SRC#", srcPort);
            temp = temp.Replace("#PORT_DST#", dstPort);
            temp = temp.Replace("#PROTO#", protocol);
            temp = temp.Replace("#SENSOR_ID#", sensorId);
            temp = temp.Replace("#SENSOR_NAME#", sensorName);
            temp = temp.Replace("#TIMESTAMP#", timestamp);

            if (command.StartsWith("http://") |
                command.StartsWith("https://"))
            {
                temp = System.Uri.EscapeUriString(temp);
            }

            return temp;
        }

        /// <summary>
        /// Update the event data with some extra details e.g. more better displaying etc. It is quicker to process here rather than later on
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public static List<Event> ProcessEventDataSet(List<Event> events)
        {
            Regex regex = new Regex(@"^Host:\s+(.*)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Event temp in events)
            {
                try
                {
                    temp.IpSrc = IPAddress.Parse(temp.IpSrcTxt);
                    temp.IpDst = IPAddress.Parse(temp.IpDstTxt);

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

                        Match match = regex.Match(temp.PayloadAscii);
                        if (match.Success == true)
                        {
                            temp.HttpHost = match.Groups[1].Value;
                        }
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
                case Global.FieldsEventCopy.SrcIp:
                    value = temp.IpSrcTxt.ToString();
                    break;
                case Global.FieldsEventCopy.SrcPort:
                    value = temp.SrcPort.ToString();
                    break;
                case Global.FieldsEventCopy.DstIp:
                    value = temp.IpDstTxt.ToString();
                    break;
                case Global.FieldsEventCopy.DstPort:
                    value = temp.DstPort.ToString();
                    break;
                case Global.FieldsEventCopy.HttpHost:
                    value = temp.HttpHost.ToString();
                    break;
                case Global.FieldsEventCopy.PayloadAscii:
                    value = temp.PayloadAscii.ToString();
                    break;
                case Global.FieldsEventCopy.Sid:
                    value = temp.SigSid.ToString();
                    break;
                case Global.FieldsEventCopy.SigName:
                    value = temp.SigName.ToString();
                    break;
                case Global.FieldsEventCopy.Cid:
                    value = temp.Cid.ToString();
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
        public static void ResizeEventListColumns(FastObjectListView list, bool hasSignatureColumn)
        {
            if (list.Items.Count == 0)
            {
                list.BeginUpdate();
                foreach (ColumnHeader column in list.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                list.EndUpdate();
            }
            else
            {
                list.BeginUpdate();
                list.Columns[(int)Global.FieldsEvent.SrcIp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.SrcPort].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.DstIp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.DstPort].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.Host].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.Protocol].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.Timestamp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.Classification].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.Initials].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                if (hasSignatureColumn == true)
                {
                    list.Columns[(int)Global.FieldsEvent.Payload].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                list.EndUpdate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ConvertByteArrayToHexString(byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", ""); ;
        }
    }
}
