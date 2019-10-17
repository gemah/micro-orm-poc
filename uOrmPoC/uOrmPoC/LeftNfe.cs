using Nzr.Orm.Core.Attributes;

namespace uOrmPoC
{
    [Table("left_nfe")]
    public class LeftNfe
    {
        [Key("type", true)]
        public string Type { get; set; }

        [ForeignKey("nfeid", ForeignKeyAttribute.JoinType.Left)]
        public Nfe Nfe { get; set; }
    }
}
