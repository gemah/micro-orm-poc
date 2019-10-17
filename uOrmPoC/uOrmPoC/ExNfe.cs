using Nzr.Orm.Core.Attributes;

namespace uOrmPoC
{
    [Table("ex_nfe")]
    public class ExNfe
    {
        [Key("id", true)]
        public int Id { get; set; }
        [Column("extra")]
        public string Extra { get; set; }
        [ForeignKey("nfeid", ForeignKeyAttribute.JoinType.Inner)]
        public Nfe Nfe { get; set; }
    }
}
