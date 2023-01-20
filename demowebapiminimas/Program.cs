using demowebapiminimas.Dal;
using demowebapiminimas.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WpmDbContext>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<WpmDbContext>()!;
db.Database.EnsureCreated();
db.Dispose();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var species = app.MapGroup("/api/species");

species.MapGet("/", async (WpmDbContext dbContext) =>
{
    var species = await dbContext.Species.ToListAsync();
    return Results.Ok(species);
}).Produces<IEnumerable<Species>>()
.WithOpenApi(operation => new(operation)
{
    Summary = "Todas las especies",
    Description = "Este endpoint regresa todas las especies de la base de datos"
});


species.MapGet("/{speciesId}/breeds", async (int speciesId, WpmDbContext dbContext) =>
{
    var breeds = await dbContext.Breeds
    .Where(b => b.SpeciesId == speciesId)
    .ToListAsync();
    return breeds.Any() ? Results.Ok(breeds) : Results.NotFound();
}).Produces<IEnumerable<Breed>>(StatusCodes.Status200OK)
  .Produces(StatusCodes.Status404NotFound);

species.MapPost("/", 
    async (SpeciesModel speciesModel, WpmDbContext dbContext) =>
{
    dbContext.Species.Add(new Species() { Name = speciesModel.Name });
    await dbContext.SaveChangesAsync();
}).Produces(StatusCodes.Status200OK);

app.Run();

record SpeciesModel(string Name);