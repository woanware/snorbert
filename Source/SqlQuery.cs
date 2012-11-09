namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlQuery
    {
        #region Member Variables
        public Sql.Query Query { get; set; }
        public string Data { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public SqlQuery()
        {
            Data = string.Empty;
        }
        #endregion
    }
}
