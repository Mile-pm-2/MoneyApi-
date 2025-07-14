using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoneyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActualCashFlowsController : ControllerBase
{
    private readonly MoneyDbContext _context;

    public ActualCashFlowsController(MoneyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActualCashFlow>>> GetActualCashFlows()
    {
        return await _context.ActualCashFlows
            .Include(a => a.CostCenter)
            .Include(a => a.CashFlowItem)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActualCashFlow>> GetActualCashFlow(int id)
    {
        var cashFlow = await _context.ActualCashFlows
            .Include(a => a.CostCenter)
            .Include(a => a.CashFlowItem)
            .FirstOrDefaultAsync(a => a.Id == id);

        return cashFlow == null ? NotFound() : cashFlow;
    }

    [HttpPost]
    public async Task<ActionResult<ActualCashFlow>> PostActualCashFlow(ActualCashFlow cashFlow)
    {
        // Проверка существования связанных сущностей
        if (!await _context.CostCenters.AnyAsync(c => c.Id == cashFlow.CostCenterId))
            return BadRequest("Invalid CostCenterId");

        if (!await _context.CashFlowItems.AnyAsync(c => c.Id == cashFlow.CashFlowItemId))
            return BadRequest("Invalid CashFlowItemId");

        _context.ActualCashFlows.Add(cashFlow);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetActualCashFlow), new { id = cashFlow.Id }, cashFlow);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutActualCashFlow(int id, ActualCashFlow cashFlow)
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
    public async Task<IActionResult> DeleteActualCashFlow(int id)
    {
        var cashFlow = await _context.ActualCashFlows.FindAsync(id);
        if (cashFlow == null) return NotFound();
        _context.ActualCashFlows.Remove(cashFlow);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}