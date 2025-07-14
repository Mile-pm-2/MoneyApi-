using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoneyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActualProfitLossController : ControllerBase
{
    private readonly MoneyDbContext _context;

    public ActualProfitLossController(MoneyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActualProfitLoss>>> GetActualProfitLosses()
    {
        return await _context.ActualProfitLosses
            .Include(a => a.CostCenter)
            .Include(a => a.ProfitLossItem)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActualProfitLoss>> GetActualProfitLoss(int id)
    {
        var item = await _context.ActualProfitLosses
            .Include(a => a.CostCenter)
            .Include(a => a.ProfitLossItem)
            .FirstOrDefaultAsync(a => a.Id == id);

        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<ActualProfitLoss>> PostActualProfitLoss(ActualProfitLoss item)
    {
        // Проверка существования связанных сущностей
        if (!await _context.CostCenters.AnyAsync(c => c.Id == item.CostCenterId))
            return BadRequest("Invalid CostCenterId");

        if (!await _context.ProfitLossItems.AnyAsync(p => p.Id == item.ProfitLossItemId))
            return BadRequest("Invalid ProfitLossItemId");

        _context.ActualProfitLosses.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetActualProfitLoss), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutActualProfitLoss(int id, ActualProfitLoss item)
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
    public async Task<IActionResult> DeleteActualProfitLoss(int id)
    {
        var item = await _context.ActualProfitLosses.FindAsync(id);
        if (item == null) return NotFound();
        _context.ActualProfitLosses.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}