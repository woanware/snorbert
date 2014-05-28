using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CsvHelper.Configuration;
using NPoco;
using snorbert.Data;

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
        private snorbert.Configs.Sql _sql;
        public bool IsRunning { get; private set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void SetSql(snorbert.Configs.Sql sql)
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

            new Thread(() =>
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
                            csvWriter.WriteField(temp.IpSrcTxt);
                            csvWriter.WriteField(temp.SrcPort);
                            csvWriter.WriteField(temp.IpDstTxt);
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
            }).Start();
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

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_RULES_EVENTS_EXPORT);
                        query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL   
                                                             AND event.timestamp > @0
			                                                 AND signature.sig_id = @1");

                        List<Event> events = db.Fetch<Event>(query, new object[] { dateFrom, sid });
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
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            }).Start();
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

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_RULES_EVENTS_EXPORT);
                        query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL   
                                                             AND event.timestamp > @0 
			                                                 AND event.timestamp < @1
			                                                 AND signature.sig_id = @2");

                        List<Event> events = db.Fetch<Event>(query, new object[] { dateFrom, dateTo, sid });
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
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            }).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">The output file name</param>
        public void ExportExcludes(string filePath)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    List<Exclude> excludes = new List<Exclude>();
                    using (NPoco.Database dbMySql = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        var data = dbMySql.Fetch<Dictionary<string, object>>(_sql.GetQuery(snorbert.Configs.Sql.Query.SQL_EXCLUDES));
                        
                        foreach (Dictionary<string, object> temp in data)
                        {
                            Exclude exclude = new Exclude();
                            exclude.Id = long.Parse(temp["id"].ToString());
                            exclude.SigId = long.Parse(temp["sig_id"].ToString());
                            exclude.SigSid = long.Parse(temp["sig_sid"].ToString());
                            exclude.Rule = temp["sig_name"].ToString();
                            exclude.SourceIpText = temp["ip_src"].ToString();
                            exclude.DestinationIpText = temp["ip_dst"].ToString();
                            if (((byte[])temp["fp"])[0] == 48)
                            {
                                exclude.FalsePositive = false;
                            }
                            else
                            {
                                exclude.FalsePositive = true;
                            }

                            exclude.Timestamp = DateTime.Parse(temp["timestamp"].ToString());
                            excludes.Add(exclude);
                        }
                    }

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
                            csvWriter.WriteField(temp.SigId);
                            csvWriter.WriteField(temp.SourceIpText);
                            csvWriter.WriteField(temp.DestinationIpText);
                            csvWriter.WriteField(temp.FalsePositive);
                            csvWriter.WriteField(temp.Comment);
                            csvWriter.WriteField(temp.Rule);
                            csvWriter.WriteField(temp.Timestamp);
                            csvWriter.WriteField(temp.Rule);
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
            }).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signatures"></param>
        /// <param name="filePath">The output file name</param>
        public void ExportRules(List<Signature> signatures,
                                string filePath)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            new Thread(() =>
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
                        csvWriter.WriteField("SID");
                        csvWriter.WriteField("Name");
                        csvWriter.WriteField("Priority");
                        csvWriter.WriteField("Count");
                        csvWriter.NextRecord();

                        foreach (Signature temp in signatures)
                        {
                            csvWriter.WriteField(temp.Sid);
                            csvWriter.WriteField(temp.Name);
                            csvWriter.WriteField(temp.Priority);
                            csvWriter.WriteField(temp.Count);
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
            }).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateFrom"></param>
        /// <param name="initials"></param>
        /// <param name="text"></param>
        public void ExportAcknowledgmentsFrom(string filePath,
                                              string dateFrom,
                                              string initials,
                                              bool text)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    if (text == true)
                    {
                        using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_ACKNOWLEDGEMENT);
                            query = query.Replace("#WHERE#", @"WHERE acknowledgment.initials = @0
                                                                 AND acknowledgment.timestamp > @1");

                            var data = db.Fetch<Dictionary<string, object>>(query, new object[] { initials, dateFrom });

                            foreach (Dictionary<string, object> temp in data)
                            {
                                streamWriter.WriteLine("Signature: " + temp["sig_name"].ToString());
                                streamWriter.WriteLine("SID/GID: " + temp["sig_sid"].ToString() + "/" + temp["sig_gid"].ToString());
                                streamWriter.WriteLine("Acknowledgement: " + temp["description"].ToString() + " (" + temp["successful"].ToString() + ")");
                                streamWriter.WriteLine("Notes: " + temp["notes"].ToString());
                                streamWriter.WriteLine(string.Empty);
                            }

                            OnComplete();
                        }
                    }
                    else
                    {
                        CsvConfiguration csvConfiguration = new CsvConfiguration();
                        csvConfiguration.Delimiter = '\t';

                        using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                        {
                            string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_ACKNOWLEDGEMENT);
                            query = query.Replace("#WHERE#", @"WHERE acknowledgment.initials = @0
                                                                 AND acknowledgment.timestamp > @1");

                            var data = db.Fetch<Dictionary<string, object>>(query, new object[] { initials, dateFrom });

                            // Write out the file headers
                            csvWriter.WriteField("Sig Name");
                            csvWriter.WriteField("Sig SID");
                            csvWriter.WriteField("Sig GID");
                            csvWriter.WriteField("Acknowledgement");
                            csvWriter.WriteField("Notes");
                            csvWriter.NextRecord();

                            foreach (Dictionary<string, object> temp in data)
                            {
                                csvWriter.WriteField(temp["sig_name"].ToString());
                                csvWriter.WriteField(temp["sig_sid"].ToString());
                                csvWriter.WriteField(temp["sig_gid"].ToString());
                                csvWriter.WriteField(temp["description"].ToString() + " (" + temp["successful"].ToString() + ")");
                                csvWriter.WriteField(temp["notes"].ToString());
                                csvWriter.NextRecord();
                            }

                            OnComplete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            }).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateFrom"></param>
        /// <param name="text"></param>
        public void ExportAcknowledgmentsFromAll(string filePath,
                                                 string dateFrom,
                                                 bool text)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    if (text == true)
                    {
                        using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_ACKNOWLEDGEMENT);
                            query = query.Replace("#WHERE#", @"WHERE acknowledgment.timestamp > @0");
                            var data = db.Fetch<Dictionary<string, object>>(query, new object[] { dateFrom });

                            foreach (Dictionary<string, object> temp in data)
                            {
                                streamWriter.WriteLine("Signature: " + temp["sig_name"].ToString());
                                streamWriter.WriteLine("SID/GID: " + temp["sig_sid"].ToString() + "/" + temp["sig_gid"].ToString());
                                streamWriter.WriteLine("Initials: " + temp["initials"].ToString());
                                streamWriter.WriteLine("Acknowledgement: " + temp["description"].ToString() + " (" + temp["successful"].ToString() + ")");
                                streamWriter.WriteLine("Notes: " + temp["notes"].ToString());
                                streamWriter.WriteLine(string.Empty);
                            }

                            OnComplete();
                        }
                    }
                    else
                    {
                        CsvConfiguration csvConfiguration = new CsvConfiguration();
                        csvConfiguration.Delimiter = '\t';

                        using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                        {
                            string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_ACKNOWLEDGEMENT);
                            query = query.Replace("#WHERE#", @"WHERE acknowledgment.timestamp > @0");
                            var data = db.Fetch<Dictionary<string, object>>(query, new object[] { dateFrom });

                            // Write out the file headers
                            csvWriter.WriteField("Sig Name");
                            csvWriter.WriteField("Sig SID");
                            csvWriter.WriteField("Sig GID");
                            csvWriter.WriteField("Initials");
                            csvWriter.WriteField("Acknowledgement");
                            csvWriter.WriteField("Notes");
                            csvWriter.NextRecord();

                            foreach (Dictionary<string, object> temp in data)
                            {
                                csvWriter.WriteField(temp["sig_name"].ToString());
                                csvWriter.WriteField(temp["sig_sid"].ToString());
                                csvWriter.WriteField(temp["sig_gid"].ToString());
                                csvWriter.WriteField(temp["initials"].ToString());
                                csvWriter.WriteField(temp["description"].ToString() + " (" + temp["successful"].ToString() + ")");
                                csvWriter.WriteField(temp["notes"].ToString());
                                csvWriter.NextRecord();
                            }

                            OnComplete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            }).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="initials"></param>
        /// <param name="text"></param>
        public void ExportAcknowledgmentsFromTo(string filePath,
                                                string dateFrom,
                                                string dateTo,
                                                string initials,
                                                bool text)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    if (text == true)
                    {
                        using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_ACKNOWLEDGEMENT);
                            query = query.Replace("#WHERE#", @"WHERE acknowledgment.initials = @0
                                                                 AND acknowledgment.timestamp > @1
                                                                 AND acknowledgment.timestamp < @2");
                            var data = db.Fetch<Dictionary<string, object>>(query, new object[] { initials, dateFrom, dateTo });

                            foreach (Dictionary<string, object> temp in data)
                            {
                                streamWriter.WriteLine("Signature: " + temp["sig_name"].ToString());
                                streamWriter.WriteLine("SID/GID: " + temp["sig_sid"].ToString() + "/" + temp["sig_gid"].ToString());
                                streamWriter.WriteLine("Acknowledgement: " + temp["description"].ToString() + " (" + temp["successful"].ToString() + ")");
                                streamWriter.WriteLine("Notes: " + temp["notes"].ToString());
                                streamWriter.WriteLine(string.Empty);
                            }

                            OnComplete();
                        }
                    }
                    else
                    {
                        CsvConfiguration csvConfiguration = new CsvConfiguration();
                        csvConfiguration.Delimiter = '\t';

                        using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                        {
                            string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_ACKNOWLEDGEMENT);
                            query = query.Replace("#WHERE#", @"WHERE acknowledgment.initials = @0
                                                                 AND acknowledgment.timestamp > @1
                                                                 AND acknowledgment.timestamp < @2");
                            var data = db.Fetch<Dictionary<string, object>>(query, new object[] { initials, dateFrom, dateTo });

                            // Write out the file headers
                            csvWriter.WriteField("Sig Name");
                            csvWriter.WriteField("Sig SID");
                            csvWriter.WriteField("Sig GID");
                            csvWriter.WriteField("Acknowledgement");
                            csvWriter.WriteField("Notes");
                            csvWriter.NextRecord();

                            foreach (Dictionary<string, object> temp in data)
                            {
                                csvWriter.WriteField(temp["sig_name"].ToString());
                                csvWriter.WriteField(temp["sig_sid"].ToString());
                                csvWriter.WriteField(temp["sig_gid"].ToString());
                                csvWriter.WriteField(temp["description"].ToString() + " (" + temp["successful"].ToString() + ")");
                                csvWriter.WriteField(temp["notes"].ToString());
                                csvWriter.NextRecord();
                            }

                            OnComplete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            }).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="text"></param>
        public void ExportAcknowledgmentsFromToAll(string filePath,
                                                   string dateFrom,
                                                   string dateTo, 
                                                   bool text)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing an export");
                return;
            }

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    if (text == true)
                    {
                        using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_ACKNOWLEDGEMENT);
                            query = query.Replace("#WHERE#", @"WHERE acknowledgment.timestamp > @0
                                                                 AND acknowledgment.timestamp < @1");
                            var data = db.Fetch<Dictionary<string, object>>(query, new object[] { dateFrom, dateTo });

                            foreach (Dictionary<string, object> temp in data)
                            {
                                streamWriter.WriteLine("Signature: " + temp["sig_name"].ToString());
                                streamWriter.WriteLine("SID/GID: " + temp["sig_sid"].ToString() + "/" + temp["sig_gid"].ToString());
                                streamWriter.WriteLine("Initials: " + temp["initials"].ToString());
                                streamWriter.WriteLine("Acknowledgement: " + temp["description"].ToString() + " (" + temp["successful"].ToString() + ")");
                                streamWriter.WriteLine("Notes: " + temp["notes"].ToString());
                                streamWriter.WriteLine(string.Empty);
                            }

                            OnComplete();
                        }
                    }
                    else
                    {
                        CsvConfiguration csvConfiguration = new CsvConfiguration();
                        csvConfiguration.Delimiter = '\t';

                        using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                        using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        using (CsvHelper.CsvWriter csvWriter = new CsvHelper.CsvWriter(streamWriter, csvConfiguration))
                        {
                            string query = _sql.GetQuery(snorbert.Configs.Sql.Query.SQL_ACKNOWLEDGEMENT);
                            query = query.Replace("#WHERE#", @"WHERE acknowledgment.timestamp > @0
                                                                 AND acknowledgment.timestamp < @1");
                            var data = db.Fetch<Dictionary<string, object>>(query, new object[] { dateFrom, dateTo });

                            // Write out the file headers
                            csvWriter.WriteField("Sig Name");
                            csvWriter.WriteField("Sig SID");
                            csvWriter.WriteField("Sig GID");
                            csvWriter.WriteField("Initials");
                            csvWriter.WriteField("Acknowledgement");
                            csvWriter.WriteField("Notes");
                            csvWriter.NextRecord();

                            foreach (Dictionary<string, object> temp in data)
                            {
                                csvWriter.WriteField(temp["sig_name"].ToString());
                                csvWriter.WriteField(temp["sig_sid"].ToString());
                                csvWriter.WriteField(temp["sig_gid"].ToString());
                                csvWriter.WriteField(temp["initials"].ToString());
                                csvWriter.WriteField(temp["description"].ToString() + " (" + temp["successful"].ToString() + ")");
                                csvWriter.WriteField(temp["notes"].ToString());
                                csvWriter.NextRecord();
                            }

                            OnComplete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the export: " + ex.Message);
                }
                finally
                {
                    IsRunning = false;
                }
            }).Start();
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
