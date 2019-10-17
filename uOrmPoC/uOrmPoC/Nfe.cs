
using Nzr.Orm.Core.Attributes;

namespace uOrmPoC
{
    [Table("nfe")]
    public partial class Nfe
    {
        [Key("id", true)]
        public int Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("xml", typeName: "Xml")]
        public dynamic Xml { get; set; }
    }
}