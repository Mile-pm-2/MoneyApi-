using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoneyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CostCentersController : ControllerBase
{
    private readonly MoneyDbContext _context;

    public CostCentersController(MoneyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CostCenter>>> GetCostCenters()
    {
        return await _context.CostCenters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CostCenter>> GetCostCenter(int id)
    {
        var costCenter = await _context.CostCenters.FindAsync(id);
        return costCenter == null ? NotFound() : costCenter;
    }

    [HttpPost]
    public async Task<ActionResult<CostCenter>> PostCostCenter(CostCenter costCenter)
    {
        _context.CostCenters.Add(costCenter);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCostCenter), new { id = costCenter.Id }, costCenter);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCostCenter(int id, CostCenter costCenter)
    {
        if (id != costCenter.Id) return BadRequest();
        _context.Entry(costCenter).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCostCenter(int id)
    {
        var costCenter = await _context.CostCenters.FindAsync(id);
        if (costCenter == null) return NotFound();
        _context.CostCenters.Remove(costCenter);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}