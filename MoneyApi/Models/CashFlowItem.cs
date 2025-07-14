using System.ComponentModel.DataAnnotations.Schema;

[Table("cashflowitems")]
public class CashFlowItem
{
    [Column("id")] public int Id { get; set; }
    [Column("name")] public string Name { get; set; }
    [Column("type")] public string Type { get; set; }
    [Column("Раздел")] public string Chapter { get; set; }
}
