using System;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class RuleFile
    {
        #region Member Variables
        public string FileName { get; set; }
        public DateTime ModifiedTimestamp { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public RuleFile()
        {
            FileName = string.Empty;
        }
        #endregion
    }
}
