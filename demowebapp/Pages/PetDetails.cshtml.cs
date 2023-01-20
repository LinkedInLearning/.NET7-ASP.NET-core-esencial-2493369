using demowebapp.Dal;
using demowebapp.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace demowebapp.Pages
{
    public class PetDetailsModel : PageModel
    {
        private readonly WpmDbContext dbContext;

        public Pet Pet { get; set; }
        public PetDetailsModel(WpmDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(int? id)
        {
            var pet = dbContext.Pets
                .Where(p => p.Id == id)
                .Include(p => p.Owners)
                .Include(p => p.Breed)
                .ThenInclude(b => b.Species)
                .First();

            Pet = pet;
        }
    }
}