using System.ComponentModel.DataAnnotations.Schema;

[Table("roles")]
public class Role
{
    [Column("id")] public int Id { get; set; }
    [Column("Название роли")] public string Name { get; set; }
}