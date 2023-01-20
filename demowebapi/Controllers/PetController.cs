using demowebapi.Dal;
using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demowebapi.Controllers;

[ApiController]
[Route("api")]
public class PetController : ControllerBase
{
    private readonly WpmDbContext dbContext;

    public PetController(WpmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("pets")]
    public async Task<ActionResult<IEnumerable<PetViewModel>>> GetAllPets()
    {
        var allPets = await dbContext.Pets
            .Include(p => p.Breed)
            .Select(p => new PetViewModel(p.Id, p.Name, p.Age, p.Weight, p.Breed.Name))
            .ToListAsync();
        return Ok(allPets);
    }

    [HttpGet("breeds/{id}/pets")]
    public async Task<ActionResult<IEnumerable<PetViewModel>>> GetPetsByBreed(int id)
    {
        var allPets = await dbContext.Pets
            .Include(p => p.Breed)
            .Where(p => p.BreedId == id)
            .Select(p => new PetViewModel(p.Id, p.Name, p.Age, p.Weight, p.Breed.Name))
            .ToListAsync();
        return allPets.Any() ? Ok(allPets) : NotFound(id);
    }
}
