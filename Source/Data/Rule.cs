using NPoco;

namespace snorbert.Data
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("rule")]
    [PrimaryKey("sig_name,sig_rev,sig_sid,sig_gid")]
    public class Rule
    {
        [Column("sig_name")]
        public string Name { get; set; }
        [Column("sig_rev")]
        public long Rev { get; set; }
        [Column("sig_sid")]
        public string Sid { get; set; }
        [Column("sig_gid")]
        public string Gid { get; set; }
        [Column("rule")]
        public string Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Rule()
        {
            Name = string.Empty;
            Data = string.Empty;
        }
    }
}