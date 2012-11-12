using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    internal class RuleImporter
    {
        #region Events
        public delegate void CompleteEvent(PersistentDictionary<string, string> rules);
        public event CompleteEvent Complete;
        public delegate void MessageEvent(string message);
        public event MessageEvent Error;
        #endregion

        #region Member Variables
        private bool _isRunning;
        private Thread _thread;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public RuleImporter(){}
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="rules"></param>
        public void Import(string[] files, PersistentDictionary<string, string> rules)
        {
            if (_isRunning == true)
            {
                return;
            }

            _thread = new Thread(() => PerformImport(files, rules));
            _thread.IsBackground = true;
            _thread.Start();
        }
        #endregion

        #region Import Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <param name="rules"></param>
        private void PerformImport(string[] files, PersistentDictionary<string, string> rules)
        {
            try
            {
                _isRunning = true;

                if (PersistentDictionaryFile.Exists(System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Rules")) == true)
                {
                    rules.Dispose();
                    //PersistentDictionaryFile.DeleteFiles("Rules");
                }

                rules = new PersistentDictionary<string, string>("Rules");

                Regex regex = new Regex(@"sid\:(.*?);", RegexOptions.IgnoreCase);
                foreach (string file in files)
                {
                    foreach (var line in File.ReadAllLines(file))
                    {
                        Match match = regex.Match(line);
                        if (match.Success == false)
                        {
                            continue;
                        }

                        var rule = from r in rules where r.Key == match.Groups[1].Value.Trim() select r;
                        if (rule.Any() == true)
                        {
                            // Update the rule e.g. remove and then readd
                            rules.Remove(match.Groups[1].Value.Trim());
                        }

                        rules.Add(match.Groups[1].Value.Trim(), line);
                    }
                }

                OnComplete(rules);
                _isRunning = false;
            }
            catch (Exception ex)
            {
                OnError("An error occurred whilst importing the rules: " + ex.Message);
            }
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// 
        /// </summary>
        private void OnComplete(PersistentDictionary<string, string> rules)
        {
            var handler = Complete;
            if (handler != null)
            {
                handler(rules);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnError(string message)
        {
            var handler = Error;
            if (handler != null)
            {
                handler(message);
            }
        }
        #endregion
    }
}
