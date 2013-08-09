using System;
namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class FalsePositive
    {
        #region Member Variables
        public string Id { get; set; }
        public string Sid { get; set; }
        public FilterDefinition Definition { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }
        public string Display { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FalsePositive()
        {
            Id = Guid.NewGuid().ToString();
            Sid = string.Empty;
            Condition = string.Empty;
            Value = string.Empty;
            Display = string.Empty;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Property only used for ObjectListView aspect name
        /// </summary>
        public string Field
        {
            get
            {
                return Definition.Field;
            }
        }
        #endregion
    }
}
