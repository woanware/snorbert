namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    internal class Reference
    {
        #region Member Variables
        public string Type { get; set; }
        public string Data { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        public Reference(string type,
                         string data)
        {
            Type = type;
            Data = data;
        }
        #endregion
    }
}
