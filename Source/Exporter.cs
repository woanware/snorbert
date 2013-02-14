using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Isam.Esent.Collections.Generic;
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
        public void ExportEventCurrent(List<Event> events, 
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
        public void ExportEventsAll(string filePath, 
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
                    NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
                    List<Event> events = db.Fetch<Event>(_sql.GetQuery(Sql.Query.SQL_EVENTS_RULES_FROM_EXPORT), new object[] { dateFrom, sid });
                    events = Helper.ProcessEventDataSet(events);

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

                        foreach (var item in events)
                        {
                            csvWriter.WriteField(item.Cid);
                            csvWriter.WriteField(item.IpSrcTxt);
                            csvWriter.WriteField(item.SrcPort);
                            csvWriter.WriteField(item.IpDst);
                            csvWriter.WriteField(item.DstPort);
                            csvWriter.WriteField(item.Protocol);
                            csvWriter.WriteField(item.Timestamp);
                            csvWriter.WriteField(item.TcpFlagsString);
                            csvWriter.WriteField(item.PayloadAscii);
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
        public void ExportEventsAll(string filePath, 
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
                    NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
                    List<Event> events = db.Fetch<Event>(_sql.GetQuery(Sql.Query.SQL_EVENTS_RULES_FROM_TO_EXPORT), new object[] { dateFrom, dateTo, sid });
                    events = Helper.ProcessEventDataSet(events);

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

                        foreach (var item in events)
                        {
                            csvWriter.WriteField(item.Cid);
                            csvWriter.WriteField(item.IpSrcTxt);
                            csvWriter.WriteField(item.SrcPort);
                            csvWriter.WriteField(item.IpDst);
                            csvWriter.WriteField(item.DstPort);
                            csvWriter.WriteField(item.Protocol);
                            csvWriter.WriteField(item.Timestamp);
                            csvWriter.WriteField(item.TcpFlagsString);
                            csvWriter.WriteField(item.PayloadAscii);
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
        public void ExportExcludes(PersistentDictionary<string, string> rules, string filePath)
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
                    NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
                    List<Exclude> excludes = db.Fetch<Exclude>(_sql.GetQuery(Sql.Query.SQL_EXCLUDES));

                    CsvConfiguration csvConfiguration = new CsvConfiguration();
                    csvConfiguration.Delimiter = '\t';

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                    {
                        // Write out the file headers
                        csvWriter.WriteField("Sig. ID");
                        csvWriter.WriteField("Source IP");
                        csvWriter.WriteField("Destination IP");
                        csvWriter.WriteField("FP");
                        csvWriter.WriteField("Comment");
                        csvWriter.WriteField("Sig. Name");
                        csvWriter.WriteField("Timestamp");
                        csvWriter.WriteField("Sig.");
                        csvWriter.NextRecord();

                        foreach (var temp in excludes)
                        {
                            //Exclude exclude = new Exclude();
                            //exclude.Id = temp.id;
                            //exclude.SigId = temp.sig_id;
                            //exclude.SigSid = temp.sig_sid;
                            //exclude.SourceIp = temp.ip_src;
                            //exclude.DestinationIp = temp.ip_dst;

                            //if (temp.fp[0] == 48)
                            //{
                            //    exclude.FalsePositive = false;
                            //}
                            //else
                            //{
                            //    exclude.FalsePositive = true;
                            //}

                            //exclude.Comment = temp.comment;
                            //exclude.Rule = temp.sig_name;
                            //exclude.Timestamp = temp.timestamp;

                            csvWriter.WriteField(temp.SigId);
                            csvWriter.WriteField(temp.SourceIp);
                            csvWriter.WriteField(temp.DestinationIp);
                            csvWriter.WriteField(temp.FalsePositive);
                            csvWriter.WriteField(temp.Comment);
                            csvWriter.WriteField(temp.Rule);
                            csvWriter.WriteField(temp.Timestamp);

                            var rule = from r in rules where r.Key == temp.SigSid.ToString() select r;
                            if (rule.Any() == true)
                            {
                                csvWriter.WriteField(rule.First().Value);
                            }
                            else
                            {
                                csvWriter.WriteField(string.Empty);
                            }

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
