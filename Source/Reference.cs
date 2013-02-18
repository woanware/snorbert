using NPoco;
namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    internal class Reference
    {
        #region Member Variables
        [Column("ref_system_name")]
        public string Type { get; set; }
        [Column("ref_tag")]
        public string Data { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Reference()
        {
            Type = string.Empty;
            Data = string.Empty;
        }

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
