using System.ComponentModel.DataAnnotations.Schema;

[Table("actualcashflow")]
public class ActualCashFlow
{
    [Column("id")] public int Id { get; set; }
    [Column("Дата операции")] public DateTime OperationDate { get; set; }
    [Column("Сумма")] public decimal Amount { get; set; }
    [Column("ЦФО_id")] public int CostCenterId { get; set; }
    [Column("Статья_cashflow_id")] public int CashFlowItemId { get; set; }
    [Column("Контрагент")] public string? Counterparty { get; set; }

    [ForeignKey("CostCenterId")]
    public CostCenter CostCenter { get; set; }

    [ForeignKey("CashFlowItemId")]
    public CashFlowItem CashFlowItem { get; set; }

}