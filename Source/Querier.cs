using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using woanware;

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
        public event CompleteEvent<Rule> RuleQueryComplete;
        public event CompleteEvent<Sensor> SensorQueryComplete;
        public event Global.MessageEvent Exclamation;
        public event Global.MessageEvent Error;
        #endregion

        #region Member Variables
        private Sql _sql;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Querier(){}
        #endregion

        #region Member Variables
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QueryEvents(long offset, int pageLimit)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing query");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var dbSignature = new DbSignature();
                    var query = dbSignature.Query(_sql.GetQuery(Sql.Query.SQL_EVENTS), args: new object[] { offset, pageLimit });

                    List<Event> data = Helper.LoadEventDataSet(query);
                    OnComplete(data);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QueryRulesFrom(string dateFrom)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing query");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var dbSignature = new DbSignature();
                    var query = dbSignature.Query(_sql.GetQuery(Sql.Query.SQL_RULES_FROM), args: new object[] { dateFrom });

                    List<Rule> data = new List<Rule>();
                    foreach (var rule in query)
                    {
                        if (rule.sig_priority.ToString().Length > 0)
                        {
                            Rule temp = new Rule(rule.sig_name + " (SID: " + rule.sig_sid.ToString() + "/Priority: " + rule.sig_priority.ToString() + "): " + rule.count.ToString(),
                                                 rule.sig_sid.ToString(),
                                                 rule.sig_priority.ToString(),
                                                 int.Parse(rule.count.ToString()));
                            data.Add(temp);
                        }
                        else
                        {
                            Rule temp = new Rule(rule.sig_name + " (SID: " + rule.sig_sid.ToString() + "): " + rule.count.ToString(),
                                                 rule.sig_sid.ToString(),
                                                 string.Empty,
                                                 int.Parse(rule.count.ToString()));
                            data.Add(temp);
                        }
                    }

                    OnComplete(data);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QueryRulesToFrom(string dateFrom, 
                                     string dateTo)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing query");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var dbSignature = new DbSignature();
                    var query = dbSignature.Query(_sql.GetQuery(Sql.Query.SQL_RULES_FROM_TO), args: new object[] { dateFrom, dateTo});

                    List<Rule> data = new List<Rule>();
                    foreach (var rule in query)
                    {
                        if (rule.sig_priority.ToString().Length > 0)
                        {
                            Rule temp = new Rule(rule.sig_name + " (SID: " + rule.sig_sid.ToString() + "/Priority: " + rule.sig_priority.ToString() + "): " + rule.count.ToString(),
                                                 rule.sig_sid.ToString(),
                                                 rule.sig_priority.ToString(),
                                                 int.Parse(rule.count.ToString()));
                            data.Add(temp);
                        }
                        else
                        {
                            Rule temp = new Rule(rule.sig_name + " (SID: " + rule.sig_sid.ToString() + "): " + rule.count.ToString(),
                                                 rule.sig_sid.ToString(),
                                                 string.Empty,
                                                 int.Parse(rule.count.ToString()));
                            data.Add(temp);
                        }
                    }
                    
                    OnComplete(data);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QueryEventsRulesToFrom(string dateFrom, 
                                           string dateTo, 
                                           string sid, 
                                           long offset, 
                                           int pageLimit)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing query");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var dbSignature = new DbSignature();
                    var query = dbSignature.Query(_sql.GetQuery(Sql.Query.SQL_EVENTS_RULES_FROM_TO), args: new object[] { dateFrom, 
                                                                                                     dateTo,
                                                                                                     sid, 
                                                                                                     offset, 
                                                                                                     pageLimit });
                    List<Event> data = Helper.LoadEventDataSet(query);
                    OnComplete(data);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QueryEventsRulesFrom(string dateFrom, 
                                         string sid, 
                                         long offset, 
                                         int pageLimit)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing query");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var dbSignature = new DbSignature();
                    var query = dbSignature.Query(_sql.GetQuery(Sql.Query.SQL_EVENTS_RULES_FROM), args: new object[] { dateFrom, 
                                                                                                     sid, 
                                                                                                     offset, 
                                                                                                     pageLimit });
                    List<Event> data = Helper.LoadEventDataSet(query);
                    OnComplete(data);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        /// <param name="offset"></param>
        /// <param name="pageLimit"></param>
        public void QuerySearch(string where, 
                                object[] args)
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing query");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var entries = new DbSignature();
                    var query = entries.Query(_sql.GetQuery(Sql.Query.SQL_EVENTS_SEARCH) + where, args: args);

                    List<Event> data = Helper.LoadEventDataSet(query);
                    OnComplete(data);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        public void QuerySensors()
        {
            if (IsRunning == true)
            {
                OnExclamation("Already performing query");
                return;
            }

            IsRunning = true;

            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    var dbSensor = new DbSensor();
                    var query = dbSensor.Query(_sql.GetQuery(Sql.Query.SQL_SENSORS));

                    List<Sensor> data = new List<Sensor>();
                    long count = 0;
                    foreach (var result in query)
                    {
                        Sensor sensor = new Sensor();
                        sensor.Sid = result.sid;
                        sensor.HostName = result.hostname;
                        sensor.Interface = result.inter;

                        if (result.timestamp != null)
                        {
                            sensor.LastEvent = result.timestamp.ToString(); 
                        }

                        sensor.EventCount = result.eventcount;
                        count += sensor.EventCount;

                        data.Add(sensor);
                    }

                    foreach (Sensor sensor in data)
                    {
                        if (sensor.EventCount > 0)
                        {
                            sensor.EventPercentage = (int)(sensor.EventCount / count) * 100;
                        }
                    }

                    OnComplete(data);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the query: " + ex.Message);
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
        private void OnComplete(List<Rule> data)
        {
            var handler = RuleQueryComplete;
            if (handler != null)
            {
                handler(data);
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
