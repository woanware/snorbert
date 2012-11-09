using System;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class Filter
    {
        #region Member Variables
        public string Id { get; private set; }
        public FilterDefinition Definition { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }
        public string Display { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Filter()
        {
            Id = Guid.NewGuid().ToString();
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
