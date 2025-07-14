using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoneyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanCashFlowsController : ControllerBase
{
    private readonly MoneyDbContext _context;

    public PlanCashFlowsController(MoneyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanCashFlow>>> GetPlanCashFlows()
    {
        return await _context.PlanCashFlows
            .Include(p => p.CostCenter)
            .Include(p => p.CashFlowItem)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlanCashFlow>> GetPlanCashFlow(int id)
    {
        var cashFlow = await _context.PlanCashFlows
            .Include(p => p.CostCenter)
            .Include(p => p.CashFlowItem)
            .FirstOrDefaultAsync(p => p.Id == id);

        return cashFlow == null ? NotFound() : cashFlow;
    }

    [HttpPost]
    public async Task<ActionResult<PlanCashFlow>> PostPlanCashFlow(PlanCashFlow cashFlow)
    {
        // Проверка существования связанных сущностей
        if (!await _context.CostCenters.AnyAsync(c => c.Id == cashFlow.CostCenterId))
            return BadRequest("Invalid CostCenterId");

        if (!await _context.CashFlowItems.AnyAsync(c => c.Id == cashFlow.CashFlowItemId))
            return BadRequest("Invalid CashFlowItemId");

        _context.PlanCashFlows.Add(cashFlow);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlanCashFlow), new { id = cashFlow.Id }, cashFlow);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlanCashFlow(int id, PlanCashFlow cashFlow)
    {
        if (id != cashFlow.Id) return BadRequest();

        // Проверка существования связанных сущностей
        if (!await _context.CostCenters.AnyAsync(c => c.Id == cashFlow.CostCenterId))
            return BadRequest("Invalid CostCenterId");

        if (!await _context.CashFlowItems.AnyAsync(c => c.Id == cashFlow.CashFlowItemId))
            return BadRequest("Invalid CashFlowItemId");

        _context.Entry(cashFlow).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlanCashFlow(int id)
    {
        var cashFlow = await _context.PlanCashFlows.FindAsync(id);
        if (cashFlow == null) return NotFound();
        _context.PlanCashFlows.Remove(cashFlow);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}