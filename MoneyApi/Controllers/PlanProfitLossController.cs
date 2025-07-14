using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoneyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanProfitLossController : ControllerBase
{
    private readonly MoneyDbContext _context;

    public PlanProfitLossController(MoneyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanProfitLoss>>> GetPlanProfitLosses()
    {
        return await _context.PlanProfitLosses
            .Include(p => p.CostCenter)
            .Include(p => p.ProfitLossItem)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlanProfitLoss>> GetPlanProfitLoss(int id)
    {
        var item = await _context.PlanProfitLosses
            .Include(p => p.CostCenter)
            .Include(p => p.ProfitLossItem)
            .FirstOrDefaultAsync(p => p.Id == id);

        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<PlanProfitLoss>> PostPlanProfitLoss(PlanProfitLoss item)
    {
        // Проверка существования связанных сущностей
        if (!await _context.CostCenters.AnyAsync(c => c.Id == item.CostCenterId))
            return BadRequest("Invalid CostCenterId");

        if (!await _context.ProfitLossItems.AnyAsync(p => p.Id == item.ProfitLossItemId))
            return BadRequest("Invalid ProfitLossItemId");

        _context.PlanProfitLosses.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlanProfitLoss), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlanProfitLoss(int id, PlanProfitLoss item)
    {
        if (id != item.Id) return BadRequest();

        // Проверка существования связанных сущностей
        if (!await _context.CostCenters.AnyAsync(c => c.Id == item.CostCenterId))
            return BadRequest("Invalid CostCenterId");

        if (!await _context.ProfitLossItems.AnyAsync(p => p.Id == item.ProfitLossItemId))
            return BadRequest("Invalid ProfitLossItemId");

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlanProfitLoss(int id)
    {
        var item = await _context.PlanProfitLosses.FindAsync(id);
        if (item == null) return NotFound();
        _context.PlanProfitLosses.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}