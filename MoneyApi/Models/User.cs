using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User
{
    [Column("id")] public int Id { get; set; }
    [Column("Имя")] public string Username { get; set; }
    [Column("role_id")] public int RoleId { get; set; }
    [Column("Email")] public string Email { get; set; }
    [Column("пароль")] public string Password { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; }
}