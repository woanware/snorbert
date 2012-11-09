using Massive;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class DbSignature : DynamicModel
    {
        public DbSignature() : base("mysql", "signature", "sig_id") { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DbReference : DynamicModel
    {
        public DbReference() : base("mysql", "reference", "ref_id") { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DbClassification : DynamicModel
    {
        public DbClassification() : base("mysql", "sig_class", "sig_class_id") { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DbSensor : DynamicModel
    {
        public DbSensor() : base("mysql", "sensor", "sid") { }
    }
}
