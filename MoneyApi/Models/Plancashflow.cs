using System.ComponentModel.DataAnnotations.Schema;

[Table("plancashflow")]
public class PlanCashFlow
{
    [Column("id")] public int Id { get; set; }
    [Column("Период")] public string Period { get; set; }
    [Column("Планируемая дата")] public DateTime PlannedDate { get; set; }
    [Column("Сумма")] public decimal Amount { get; set; }
    [Column("ЦФО_id")] public int CostCenterId { get; set; }
    [Column("Статья_cashflow_id")] public int CashFlowItemId { get; set; }

    [ForeignKey("CostCenterId")]
    public CostCenter CostCenter { get; set; }

    [ForeignKey("CashFlowItemId")]
    public CashFlowItem CashFlowItem { get; set; }
}