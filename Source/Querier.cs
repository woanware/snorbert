using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using woanware;
using snorbert.Configs;
using snorbert.Data;

namespace snorbert
{
    /// <summary>
    /// Object used to perform background querying
    /// </summary>
    internal class Querier
    {
        #region Events
        public delegate void CompleteEvent<T>(List<T> data);
        public event CompleteEvent<Event> EventQueryComplete;
        public event CompleteEvent<Signature> RuleQueryComplete;
        public event CompleteEvent<Signature> RuleCheckQueryComplete;
        public event CompleteEvent<Sensor> SensorQueryComplete;
        public event CompleteEvent<string> RuleIpQueryComplete;
        public event Global.MessageEvent Exclamation;
        public event Global.MessageEvent Error;
        #endregion

        #region Member Variables
        private Sql _sql;
        public bool IsRunning { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Querier(){}
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QueryEvents(long offset, int pageLimit)
        {
            //if (IsRunning == true)
            //{
            //    //OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Event> data = db.Fetch<Event>(_sql.GetQuery(Sql.Query.SQL_EVENTS), new object[] { offset, pageLimit });

                        data = Helper.ProcessEventDataSet(data);
                        OnComplete(data);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="dateFrom"></param>
        /// <param name="hostName"></param>
        /// <param name="check"></param>
        public void QueryRulesFrom(string dateFrom,
                                   string hostName, 
                                   bool includeAcks,
                                   bool check)
        {
            //if (IsRunning == true)
            //{
            //    //OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Signature> temp;
                        string query = _sql.GetQuery(Sql.Query.SQL_RULES);
                        if (includeAcks == true)
                        {
                            if (hostName == string.Empty)
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
		                                                             AND event.timestamp > @0");
                                temp = db.Fetch<Signature>(query, new object[] { dateFrom });
                            }
                            else
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
			                                                         AND event.timestamp > @0
                                                                     AND event.sid IN (SELECT sid FROM sensor WHERE hostname=@1)");
                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, hostName });
                            }
                        }
                        else
                        {
                            if (hostName == string.Empty)
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
                                                                     AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
		                                                             AND event.timestamp > @0");
                                temp = db.Fetch<Signature>(query, new object[] { dateFrom });
                            }
                            else
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
                                                                     AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                         AND event.timestamp > @0
                                                                     AND event.sid IN (SELECT sid FROM sensor WHERE hostname=@1)");
                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, hostName });
                            }
                        }

                        foreach (var rule in temp)
                        {
                            if (rule.Priority.ToString().Length > 0)
                            {
                                rule.Text = rule.Name + " (GID#SID: " + rule.Gid + "#" + rule.Sid.ToString() + "/Priority: " + rule.Priority.ToString() + "): " + rule.Count.ToString();
                            }
                            else
                            {
                                rule.Text = rule.Name + " (GID#SID: " + rule.Gid + "#" + rule.Sid.ToString() + "): " + rule.Count.ToString();
                            }
                        }

                        OnComplete(temp, check);
                    }                    
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="dateFrom"></param>
        /// <param name="priority"></param>
        /// <param name="hostName"></param>
        /// <param name="includeAcks"></param>
        /// <param name="check"></param>
        public void QueryRulesFromPriority(string dateFrom,
                                           string priority,
                                           string hostName,
                                           bool includeAcks,
                                           bool check)
        {
            //if (IsRunning == true)
            //{
            //    //OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Signature> temp;
                        string query = _sql.GetQuery(Sql.Query.SQL_RULES);
                        if (includeAcks == true)
                        {
                            if (hostName == string.Empty)
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
			                                                         AND event.timestamp > @0
                                                                     AND signature.sig_priority = @1");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, priority });
                            }
                            else
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
			                                                         AND event.timestamp > @0
                                                                     AND signature.sig_priority = @1
                                                                     AND event.sid IN (SELECT sid FROM sensor WHERE hostname=@2)");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, priority, hostName });
                            }
                        }
                        else
                        {
                            if (hostName == string.Empty)
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
                                                                     AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                         AND event.timestamp > @0
                                                                     AND signature.sig_priority = @1");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, priority });
                            }
                            else
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
                                                                     AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                         AND event.timestamp > @0
                                                                     AND signature.sig_priority = @1
                                                                     AND event.sid IN (SELECT sid FROM sensor WHERE hostname=@2)");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, priority, hostName });
                            }
                        }

                        foreach (var rule in temp)
                        {
                            rule.Text = rule.Name + " (SID: " + rule.Sid.ToString() + "): " + rule.Count.ToString();
                        }

                        OnComplete(temp, check);
                    }                    
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="hostName"></param>
        /// <param name="check"></param>
        public void QueryRulesFromTo(string dateFrom, 
                                     string dateTo,
                                     string hostName,
                                     bool includeAcks,
                                     bool check)
        {
            //if (IsRunning == true)
            //{
            //    //OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Signature> temp;
                        string query = _sql.GetQuery(Sql.Query.SQL_RULES);

                        if (includeAcks == true)
                        {
                            if (hostName == string.Empty)
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
			                                                         AND event.timestamp > @0 
			                                                         AND event.timestamp < @1");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, dateTo });
                            }
                            else
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
			                                                         AND event.timestamp > @0 
			                                                         AND event.timestamp < @1
                                                                     AND event.sid IN (SELECT sid FROM sensor WHERE hostname=@2)");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, dateTo, hostName });
                            }
                        }
                        else
                        {
                            if (hostName == string.Empty)
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
                                                                     AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                         AND event.timestamp > @0 
			                                                         AND event.timestamp < @1");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, dateTo });
                            }
                            else
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
                                                                     AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                         AND event.timestamp > @0 
			                                                         AND event.timestamp < @1
                                                                     AND event.sid IN (SELECT sid FROM sensor WHERE hostname=@2)");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, dateTo, hostName });
                            }
                        }

                        foreach (var rule in temp)
                        {
                            if (rule.Priority.ToString().Length > 0)
                            {
                                rule.Text = rule.Name + " (SID: " + rule.Sid.ToString() + "/Priority: " + rule.Priority.ToString() + "): " + rule.Count.ToString();
                            }
                            else
                            {
                                rule.Text = rule.Name + " (SID: " + rule.Sid.ToString() + "): " + rule.Count.ToString();
                            }
                        }

                        OnComplete(temp, check);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="priority"></param>
        /// <param name="hostName"></param>
        /// <param name="includeAcks"></param>
        /// <param name="check"></param>
        public void QueryRulesFromToPriority(string dateFrom,
                                             string dateTo,
                                             string priority,
                                             string hostName,
                                             bool includeAcks,
                                             bool check)
        {
            //if (IsRunning == true)
            //{
            //    //OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Signature> temp;
                        string query = _sql.GetQuery(Sql.Query.SQL_RULES);

                        if (includeAcks == true)
                        {
                            if (hostName == string.Empty)
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
			                                                         AND event.timestamp > @0 
			                                                         AND event.timestamp < @1
                                                                     AND signature.sig_priority = @2");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, dateTo, priority });
                            }
                            else
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
			                                                         AND event.timestamp > @0 
			                                                         AND event.timestamp < @1
                                                                     AND signature.sig_priority = @2
                                                                     AND event.sid IN (SELECT sid FROM sensor WHERE hostname=@3)");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, dateTo, priority, hostName });
                            }
                        }
                        else
                        {
                            if (hostName == string.Empty)
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
                                                                     AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                         AND event.timestamp > @0 
			                                                         AND event.timestamp < @1
                                                                     AND signature.sig_priority = @2");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, dateTo, priority });
                            }
                            else
                            {
                                query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL 
                                                                     AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                         AND event.timestamp > @0 
			                                                         AND event.timestamp < @1
                                                                     AND signature.sig_priority = @2
                                                                     AND event.sid IN (SELECT sid FROM sensor WHERE hostname=@3)");

                                temp = db.Fetch<Signature>(query, new object[] { dateFrom, dateTo, priority, hostName });
                            }
                        }

                        foreach (var rule in temp)
                        {
                            rule.Text = rule.Name + " (SID: " + rule.Sid.ToString() + "): " + rule.Count.ToString();
                        }

                        OnComplete(temp, check);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QueryEventsRulesFromTo(string dateFrom, 
                                           string dateTo, 
                                           string sid, 
                                           long offset, 
                                           int pageLimit,
                                           bool includeAcks)
        {
            //if (IsRunning == true)
            //{
            //    //OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Event> temp;
                        string query = _sql.GetQuery(Sql.Query.SQL_RULES_EVENTS);

                        if (includeAcks == true)
                        {
                            query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL   
			                                                     AND event.timestamp > @0 
			                                                     AND event.timestamp < @1
			                                                     AND signature.sig_id = @2 LIMIT @3, @4");
                        }
                        else
                        {
                            query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL   
                                                                 AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                     AND event.timestamp > @0 
			                                                     AND event.timestamp < @1
			                                                     AND signature.sig_id = @2 LIMIT @3, @4");
                        }

                        temp = db.Fetch<Event>(query, new object[] { dateFrom, dateTo, sid, offset, pageLimit });
                        temp = Helper.ProcessEventDataSet(temp);
                        OnComplete(temp);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QueryEventsRulesFrom(string dateFrom, 
                                         string sid, 
                                         long offset, 
                                         int pageLimit,
                                         bool includeAcks)
        {
            //if (IsRunning == true)
            //{
            //    //OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Event> temp;
                        string query = _sql.GetQuery(Sql.Query.SQL_RULES_EVENTS);

                        if (includeAcks == true)
                        {
                            query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL   
			                                                     AND event.timestamp > @0
			                                                     AND signature.sig_id = @1 LIMIT @2, @3");
                        }
                        else
                        {
                            query = query.Replace("#WHERE#", @"WHERE exclude.id IS NULL   
                                                                 AND (acknowledgment.id IS NULL OR acknowledgment.class = 1)
			                                                     AND event.timestamp > @0
			                                                     AND signature.sig_id = @1 LIMIT @2, @3");
                        }

                        temp = db.Fetch<Event>(query, new object[] { dateFrom, sid, offset, pageLimit });
                        temp = Helper.ProcessEventDataSet(temp);
                        OnComplete(temp);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QuerySearch(string where, 
                                object[] args)
        {
            //if (IsRunning == true)
            //{
            //    OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Event> data = db.Fetch<Event>(_sql.GetQuery(Sql.Query.SQL_EVENTS_SEARCH) + where, args);

                        data = Helper.ProcessEventDataSet(data);
                        OnComplete(data);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        public void QuerySensors()
        {
            //if (IsRunning == true)
            //{
            //    OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<Sensor> sensors = db.Fetch<Sensor>(_sql.GetQuery(Sql.Query.SQL_SENSORS));

                        long count = 0;
                        foreach (Sensor sensor in sensors)
                        {
                            count += sensor.EventCount;
                        }

                        foreach (Sensor sensor in sensors)
                        {
                            if (sensor.EventCount > 0)
                            {
                                sensor.EventPercentage = (int)(sensor.EventCount / count) * 100;
                            }
                        }

                        OnComplete(sensors);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="id"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="sourceIps"></param>
        public void QueryRuleIpsFromTo(string id,
                                       string dateFrom,
                                       string dateTo,
                                       bool sourceIps)
        {
            //if (IsRunning == true)
            //{
            //    OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<string> data = new List<string>();
                        if (sourceIps == true)
                        {
                            string query = _sql.GetQuery(Sql.Query.SQL_RULES_SRC_IPS);
                            query = query.Replace("#WHERE#", @"WHERE event.signature=@0 
                                                                 AND event.timestamp > @1
                                                                 AND event.timestamp < @2");

                            List<Event> temp = db.Fetch<Event>(query, new object[] { id, dateFrom, dateTo });
                            foreach (var rule in temp)
                            {
                                data.Add(rule.IpSrcTxt);
                            }
                        }
                        else
                        {
                            string query = _sql.GetQuery(Sql.Query.SQL_RULES_DST_IPS);
                            query = query.Replace("#WHERE#", @"WHERE event.signature=@0 
                                                                 AND event.timestamp > @1
                                                                 AND event.timestamp < @2");

                            List<Event> temp = db.Fetch<Event>(query, new object[] { id, dateFrom, dateTo });
                            foreach (var rule in temp)
                            {
                                data.Add(rule.IpDstTxt);
                            }
                        }

                        OnComplete(data);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="id"></param>
        /// <param name="dateFrom"></param>
        /// <param name="sourceIps"></param>
        public void QueryRuleIpsFrom(string id,
                                     string dateFrom,   
                                     bool sourceIps)
        {
            //if (IsRunning == true)
            //{
            //    OnExclamation("Already performing query");
            //    return;
            //}

            IsRunning = true;

            new Thread(() =>
            {
                try
                {
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        List<string> data = new List<string>();
                        if (sourceIps == true)
                        {
                            string query = _sql.GetQuery(Sql.Query.SQL_RULES_SRC_IPS);
                            query = query.Replace("#WHERE#", @"WHERE event.signature=@0
                                                                 AND event.timestamp > @1");

                            List<Event> temp = db.Fetch<Event>(query, new object[] { id, dateFrom });
                            foreach (var rule in temp)
                            {
                                data.Add(rule.IpSrcTxt);
                            }
                        }
                        else
                        {
                            string query = _sql.GetQuery(Sql.Query.SQL_RULES_DST_IPS);
                            query = query.Replace("#WHERE#", @"WHERE event.signature=@0
                                                                 AND event.timestamp > @1");

                            List<Event> temp = db.Fetch<Event>(query, new object[] { id, dateFrom });
                            foreach (var rule in temp)
                            {
                                data.Add(rule.IpDstTxt);
                            }
                        }

                        OnComplete(data);
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        private void OnComplete(List<Event> data)
        {
            var handler = EventQueryComplete;
            if (handler != null)
            {
                handler(data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="check"></param>
        private void OnComplete(List<Signature> data, bool check)
        {
            if (check == true)
            {
                var handler = RuleCheckQueryComplete;
                if (handler != null)
                {
                    handler(data);
                }
            }
            else
            {
                var handler = RuleQueryComplete;
                if (handler != null)
                {
                    handler(data);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnComplete(List<Sensor> data)
        {
            var handler = SensorQueryComplete;
            if (handler != null)
            {
                handler(data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnComplete(List<string> data)
        {
            var handler = RuleIpQueryComplete;
            if (handler != null)
            {
                handler(data);
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
