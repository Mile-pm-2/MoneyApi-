using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoneyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CashFlowItemsController : ControllerBase
{
    private readonly MoneyDbContext _context;

    public CashFlowItemsController(MoneyDbContext context)
    {
        _context = context;
    }

    // GET: api/cashflowitems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CashFlowItem>>> GetCashFlowItems()
    {
        return await _context.CashFlowItems.ToListAsync();
    }

    // GET: api/cashflowitems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CashFlowItem>> GetCashFlowItem(int id)
    {
        var item = await _context.CashFlowItems.FindAsync(id);
        return item == null ? NotFound() : item;
    }

    // POST: api/cashflowitems
    [HttpPost]
    public async Task<ActionResult<CashFlowItem>> PostCashFlowItem(CashFlowItem item)
    {
        _context.CashFlowItems.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCashFlowItem), new { id = item.Id }, item);
    }

    // PUT: api/cashflowitems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCashFlowItem(int id, CashFlowItem item)
    {
        if (id != item.Id) return BadRequest();
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/cashflowitems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCashFlowItem(int id)
    {
        var item = await _context.CashFlowItems.FindAsync(id);
        if (item == null) return NotFound();
        _context.CashFlowItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}