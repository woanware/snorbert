using NPoco;
namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    internal class Rule
    {
        #region Member Variables
        [Column("sig_id")]
        public long Id { get; set; }
        [Column("sig_sid")]
        public string Sid { get; set; }
        [Column("sig_name")]
        public string Name { get; set; }
        [Column("sig_priority")]
        public string Priority { get; set; }
        [Ignore]
        public string Text { get; set; }
        [Ignore]
        public long Count { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Rule()
        {
            Sid = string.Empty;
            Name = string.Empty;
            Priority = string.Empty;
            Text = string.Empty;
        }

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
