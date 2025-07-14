using System.ComponentModel.DataAnnotations.Schema;

[Table("costcenters")]
public class CostCenter
{
    [Column("id")] public int Id { get; set; }
    [Column("Название ЦФО")] public string NameCfo { get; set; }
}