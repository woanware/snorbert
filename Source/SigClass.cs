using NPoco;
namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class SigClass
    {
        #region Member Variables
        [Column("sig_class_id")]
        public int Id { get; set; }
        [Column("sig_class_name")]
        public string Name { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public SigClass()
        {
            Name = string.Empty;
        }
        #endregion
    }
}
