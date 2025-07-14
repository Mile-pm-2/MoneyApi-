using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoneyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfitLossItemsController : ControllerBase
{
    private readonly MoneyDbContext _context;

    public ProfitLossItemsController(MoneyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfitLossItem>>> GetProfitLossItems()
    {
        return await _context.ProfitLossItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfitLossItem>> GetProfitLossItem(int id)
    {
        var item = await _context.ProfitLossItems.FindAsync(id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<ProfitLossItem>> PostProfitLossItem(ProfitLossItem item)
    {
        _context.ProfitLossItems.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProfitLossItem), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProfitLossItem(int id, ProfitLossItem item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfitLossItem(int id)
    {
        var item = await _context.ProfitLossItems.FindAsync(id);
        if (item == null) return NotFound();
        _context.ProfitLossItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}