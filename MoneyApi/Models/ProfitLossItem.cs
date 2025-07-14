using System.ComponentModel.DataAnnotations.Schema;

[Table("profitlossitems")]
public class ProfitLossItem
{
    [Column("id")] public int Id { get; set; }
    [Column("Название статьи")] public string Name { get; set; }
    [Column("Тип")] public string Type { get; set; }
}