using NPoco;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    [TableName("acknowledgment_class")]
    [PrimaryKey("id")] 
    public class AcknowledgmentClass
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("desc")]
        public string Desc { get; set; }
    }
}
