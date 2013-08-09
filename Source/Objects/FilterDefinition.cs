namespace snorbert.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterDefinition
    {
        #region Member Variables
        public string ColumnName { get; set; }
        public string Field { get; set; }
        public snorbert.Global.FilterType Type { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FilterDefinition()
        {
            ColumnName = string.Empty;
            Field = string.Empty;
        }
        #endregion
    }
}
