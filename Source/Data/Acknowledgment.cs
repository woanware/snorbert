using System;
using NPoco;

namespace snorbert.Data
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("acknowledgment")]
    [PrimaryKey("id")] 
    public class Acknowledgment
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("cid")]
        public long Cid { get; set; }
        [Column("sid")]
        public long Sid { get; set; }
        [Column("initials")]
        public string Initials { get; set; }
        [Column("notes")]
        public string Notes { get; set; }
        [Column("class")]
        public long Class { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
