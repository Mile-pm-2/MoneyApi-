using System.ComponentModel.DataAnnotations.Schema;

[Table("actualprofitioss")]
public class ActualProfitLoss
{
    [Column("id")] public int Id { get; set; }

    [Column("дата операции")]
    public DateTime OperationDate { get; set; }

    [Column("Сумма")]
    public decimal Amount { get; set; }

    [Column("ЦФО_id")]
    public int CostCenterId { get; set; }

    [Column("Статья_PL_id")]
    public int ProfitLossItemId { get; set; }

    [ForeignKey("CostCenterId")]
    public CostCenter CostCenter { get; set; }

    [ForeignKey("ProfitLossItemId")]
    public ProfitLossItem ProfitLossItem { get; set; }
}

