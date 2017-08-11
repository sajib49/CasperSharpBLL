using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Model
{
    [Table("t_MarketGroup")]
    public class MarketGroup
    {
        [Key]
        public int MarketGroupId { get; set; }
        public int? Pos { get; set; }
        public string MarketGroupCode { get; set; }
        public string MarketGroupDesc { get; set; }
        public int MarketGroupType { get; set; }
        public int? ParentId { get; set; }
        public int? EmployeeId { get; set; }
        public int ChannelId { get; set; }
        public string ShortName { get; set; }
    }
}
