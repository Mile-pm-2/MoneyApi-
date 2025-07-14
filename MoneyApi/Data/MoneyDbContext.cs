using Microsoft.EntityFrameworkCore;

public class MoneyDbContext : DbContext
{
    public MoneyDbContext(DbContextOptions<MoneyDbContext> options) : base(options) { }
    public DbSet<ActualProfitLoss> ActualProfitLosses { get; set; }
    public DbSet<PlanProfitLoss> PlanProfitLosses { get; set; }
    public DbSet<CashFlowItem> CashFlowItems { get; set; }
    public DbSet<CostCenter> CostCenters { get; set; }
    public DbSet<ProfitLossItem> ProfitLossItems { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ActualCashFlow> ActualCashFlows { get; set; }
    public DbSet<PlanCashFlow> PlanCashFlows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурация связей
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<ActualProfitLoss>()
            .HasOne(a => a.CostCenter)
            .WithMany()
            .HasForeignKey(a => a.CostCenterId);

        modelBuilder.Entity<ActualProfitLoss>()
            .HasOne(a => a.ProfitLossItem)
            .WithMany()
            .HasForeignKey(a => a.ProfitLossItemId);

        modelBuilder.Entity<PlanProfitLoss>()
            .HasOne(p => p.CostCenter)
            .WithMany()
            .HasForeignKey(p => p.CostCenterId);

        modelBuilder.Entity<PlanProfitLoss>()
            .HasOne(p => p.ProfitLossItem)
            .WithMany()
            .HasForeignKey(p => p.ProfitLossItemId);
    }
}