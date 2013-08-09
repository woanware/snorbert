using NPoco;

namespace snorbert.Data
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("Rules")]
    [PrimaryKey("Id")]
    public class Rule
    {
        [Column("Id")]
        public long Id { get; set; }
        [Column("Sid")]
        public string Sid { get; set; }
        [Column("Rule")]
        public string Data { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Rule()
        {
            Sid = string.Empty;
            Data = string.Empty;
        }
    }
}
