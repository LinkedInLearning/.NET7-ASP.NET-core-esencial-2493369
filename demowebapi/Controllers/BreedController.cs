using demowebapi.Dal;
using demowebapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace demowebapi.Controllers;

[ApiController]
[Route("api")]
public class BreedController : ControllerBase
{
    private readonly WpmDbContext dbContext;

    public BreedController(WpmDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("species/{id}/breeds")]
    public async Task<ActionResult<IEnumerable<BreedViewModel>>> GetBreedsBySpecies(int id)
    {
        var allBreeds = await dbContext.Breeds
            .Include(b => b.Species)
            .Where(b => b.SpeciesId == id)
            .Select(b => new BreedViewModel(b.Id, b.Name, b.Species.Name))
            .ToListAsync();
        return allBreeds.Any() ? Ok(allBreeds) : NotFound(id);
    }

    [HttpGet("breeds")]
    public async Task<ActionResult<IEnumerable<BreedViewModel>>> GetAllBreeds()
    {
        var allBreeds = await dbContext.Breeds
            .Include(b => b.Species)
            .Select(b => new BreedViewModel(b.Id, b.Name, b.Species.Name))
            .ToListAsync();
        return Ok(allBreeds);
    }

    [HttpPost("species/{speciesId}/breeds")]
    public async Task<IActionResult> CreateBreed(int speciesId, BreedModel breedModel)
    {
        var newBreed = new Domain.Breed()
        {
            Name = breedModel.Name,
            SpeciesId = speciesId,
            IdealMaxWeight = breedModel.IdealMaxWeight
        };
        dbContext.Breeds.Add(newBreed);
        var result = await dbContext.SaveChangesAsync();
        return result == 1 ? Ok(newBreed.Id) : BadRequest();
    }

    [HttpPut("breeds/{id}")]
    public async Task<IActionResult> UpdateBreed(int id, BreedModel breedModel)
    {
        var breed = dbContext.Breeds.First(b => b.Id == id);
        breed.Name = breedModel.Name;
        breed.IdealMaxWeight = breedModel.IdealMaxWeight;
        var result = await dbContext.SaveChangesAsync();
        return result == 1 ? Ok() : BadRequest();
    }
}
