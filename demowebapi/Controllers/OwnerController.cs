using demowebapi.Dal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demowebapi.Controllers;

[ApiController]
[Route("api")]
public class OwnerController : ControllerBase
{
    private readonly WpmDbContext dbContext;

    public OwnerController(WpmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("pets/{id}/owners")]
    public async Task<IActionResult> GetOwnersByPet(int id)
    {
        var owners = await dbContext.Pets
            .Include(p => p.Owners)
            .Where(p => p.Id == id)
            .SelectMany(p => p.Owners)
            .Select(p => new { p.Id, p.Name })
            .ToListAsync();
        return owners != null ? Ok(owners) : NotFound(id);
    }
}