using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using woanware;

namespace snorbert
{
    /// <summary>
    /// Object used for exporting event data. Works on a background thread to prevent UI slow downs
    /// </summary>
    public class Exporter
    {
        #region Events
        public event Global.DefaultEvent Complete;
        public event Global.MessageEvent Exclamation;
        public event Global.MessageEvent Error;
        #endregion

        #region Member Variables
        private Sql _sql;
        public bool IsRunning { get; private set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void SetSql(Sql sql)
        {
            _sql = sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        /// <param name="filePath">The output file name</param>
        public void ExportCurrent(List<Event> events, 
                                  string filePath)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    CsvConfiguration csvConfiguration = new CsvConfiguration();
                    csvConfiguration.Delimiter = '\t';

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                    {
                        // Write out the file headers
                        csvWriter.WriteField("CID");
                        csvWriter.WriteField("Src IP");
                        csvWriter.WriteField("Src Port");
                        csvWriter.WriteField("Dst IP");
                        csvWriter.WriteField("Dst Port");
                        csvWriter.WriteField("Protocol");
                        csvWriter.WriteField("Timestamp");
                        csvWriter.WriteField("TCP Flags");
                        csvWriter.WriteField("Payload (ASCII)");
                        csvWriter.NextRecord();

                        foreach (Event temp in events)
                        {
                            csvWriter.WriteField(temp.Cid);
                            csvWriter.WriteField(temp.IpSrc);
                            csvWriter.WriteField(temp.SrcPort);
                            csvWriter.WriteField(temp.IpDst);
                            csvWriter.WriteField(temp.DstPort);
                            csvWriter.WriteField(temp.Protocol);
                            csvWriter.WriteField(temp.Timestamp);
                            csvWriter.WriteField(temp.TcpFlagsString);
                            csvWriter.WriteField(temp.PayloadAscii);
                            csvWriter.NextRecord();
                        }
                    }

                    OnComplete();
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">The output file name</param>
        /// <param name="dateFrom"></param>
        /// <param name="sid"></param>
        public void ExportAll(string filePath, 
                              string dateFrom, 
                              string sid)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var dbSignature = new DbSignature();
                    var query = dbSignature.Query(_sql.GetQuery(Sql.Query.SQL_EVENTS_RULES_FROM_EXPORT), args: new object[] { dateFrom, 
                                                                                                                              sid});
                    CsvConfiguration csvConfiguration = new CsvConfiguration();
                    csvConfiguration.Delimiter = '\t';

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                    {
                        // Write out the file headers
                        csvWriter.WriteField("CID");
                        csvWriter.WriteField("Src IP");
                        csvWriter.WriteField("Src Port");
                        csvWriter.WriteField("Dst IP");
                        csvWriter.WriteField("Dst Port");
                        csvWriter.WriteField("Protocol");
                        csvWriter.WriteField("Timestamp");
                        csvWriter.WriteField("TCP Flags");
                        csvWriter.WriteField("Payload (ASCII)");
                        csvWriter.NextRecord();

                        foreach (var item in query)
                        {
                            Event e = new Event();
                            e.Cid = item.cid;
                            e.IpSrc = IPAddress.Parse(item.ip_src.ToString());
                            e.IpDst = IPAddress.Parse(item.ip_dst.ToString());
                            e.IpProto = int.Parse(item.ip_proto.ToString());
                            e.TcpSrcPort = int.Parse(item.tcp_sport.ToString());
                            e.TcpDstPort = int.Parse(item.tcp_dport.ToString());
                            e.UdpSrcPort = int.Parse(item.udp_sport.ToString());
                            e.UdpDstPort = int.Parse(item.udp_dport.ToString());
                            e.Timestamp = item.timestamp;
                            e.TcpFlags = int.Parse(item.tcp_flags.ToString());

                            List<string> flags = new List<string>();
                            foreach (Global.TcpFlags tcpFlag in Misc.EnumToList<Global.TcpFlags>())
                            {
                                if ((e.TcpFlags & (int)tcpFlag) == (int)tcpFlag)
                                {
                                    flags.Add(tcpFlag.GetEnumDescription());
                                }
                            }

                            e.TcpFlagsString = string.Join("+", flags.ToArray());

                            if (e.IpProto == (int)Global.Protocols.Tcp)
                            {
                                e.Protocol = Global.Protocols.Tcp.GetEnumDescription();
                                e.SrcPort = int.Parse(item.tcp_sport.ToString());
                                e.DstPort = int.Parse(item.tcp_dport.ToString());

                            }
                            else if (e.IpProto == (int)Global.Protocols.Udp)
                            {
                                e.Protocol = Global.Protocols.Udp.GetEnumDescription();
                                e.SrcPort = int.Parse(item.udp_sport.ToString());
                                e.DstPort = int.Parse(item.udp_dport.ToString());
                            }
                            else
                            {
                                e.SrcPort = 0;
                                e.DstPort = 0;
                            }

                            if (item.data_payload != null)
                            {
                                e.PayloadHex = Helper.StringToByteArray(item.data_payload.ToString());
                                e.PayloadAscii = woanware.Text.ReplaceNulls(woanware.Text.ByteArrayToString(e.PayloadHex, woanware.Text.EncodingType.Ascii));
                            }

                            csvWriter.WriteField(e.Cid);
                            csvWriter.WriteField(e.IpSrc);
                            csvWriter.WriteField(e.SrcPort);
                            csvWriter.WriteField(e.IpDst);
                            csvWriter.WriteField(e.DstPort);
                            csvWriter.WriteField(e.Protocol);
                            csvWriter.WriteField(e.Timestamp);
                            csvWriter.WriteField(e.TcpFlagsString);
                            csvWriter.WriteField(e.PayloadAscii);
                            csvWriter.NextRecord();
                        }
                    }

                    OnComplete();
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">The output file name</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="sid"></param>
        public void ExportAll(string filePath, 
                              string dateFrom, 
                              string dateTo, 
                              string sid)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var dbSignature = new DbSignature();
                    var query = dbSignature.Query(_sql.GetQuery(Sql.Query.SQL_EVENTS_RULES_FROM_EXPORT), args: new object[] { dateFrom, 
                                                                                                                              dateTo,
                                                                                                                              sid});

                    CsvConfiguration csvConfiguration = new CsvConfiguration();
                    csvConfiguration.Delimiter = '\t';

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                    {
                        // Write out the file headers
                        csvWriter.WriteField("CID");
                        csvWriter.WriteField("Src IP");
                        csvWriter.WriteField("Src Port");
                        csvWriter.WriteField("Dst IP");
                        csvWriter.WriteField("Dst Port");
                        csvWriter.WriteField("Protocol");
                        csvWriter.WriteField("Timestamp");
                        csvWriter.WriteField("TCP Flags");
                        csvWriter.WriteField("Payload (ASCII)");
                        csvWriter.NextRecord();

                        foreach (Event temp in query)
                        {
                            csvWriter.WriteField(temp.Cid);
                            csvWriter.WriteField(temp.IpSrc);
                            csvWriter.WriteField(temp.SrcPort);
                            csvWriter.WriteField(temp.IpDst);
                            csvWriter.WriteField(temp.DstPort);
                            csvWriter.WriteField(temp.Protocol);
                            csvWriter.WriteField(temp.Timestamp);
                            csvWriter.WriteField(temp.TcpFlagsString);
                            csvWriter.WriteField(temp.PayloadAscii);
                            csvWriter.NextRecord();
                        }
                    }

                    OnComplete();
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            });
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// 
        /// </summary>
        private void OnComplete()
        {
            var handler = Complete;
            if (handler != null)
            {
                handler();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnExclamation(string message)
        {
            var handler = Exclamation;
            if (handler != null)
            {
                handler(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        private void OnError(string error)
        {
            var handler = Error;
            if (handler != null)
            {
                handler(error);
            }
        }
        #endregion
    }
}
