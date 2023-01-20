using demowebapi.Dal;
using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demowebapi.Controllers;
[ApiController]
[Route("api")]
public class SpeciesController : ControllerBase
{
    private readonly WpmDbContext dbContext;

    public SpeciesController(WpmDbContext dbContext)
	{
        this.dbContext = dbContext;
    }

    [HttpGet("species")]
    public async Task<ActionResult<IEnumerable<SpeciesViewModel>>> GetAllSpecies()
    {
        var allSpecies = await dbContext
                                .Species
                                .Select(s => new SpeciesViewModel(s.Id, s.Name))
                                .ToListAsync();
        return Ok(allSpecies);
    }
}