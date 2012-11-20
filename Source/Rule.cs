namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    internal class Rule
    {
        #region Member Variables
        public long Id { get; set; }
        public string Text { get; set; }
        public string Sid { get; set; }
        public string Priority { get; set; }
        public long Count { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="sid"></param>
        /// <param name="priority"></param>
        /// <param name="count"></param>
        public Rule(long id, 
                    string text, 
                    string sid, 
                    string priority, 
                    int count)
        {
            Id = id;
            Text = text;
            Sid = sid;
            Priority = priority;
            Count = count;
        }
        #endregion
    }
}
