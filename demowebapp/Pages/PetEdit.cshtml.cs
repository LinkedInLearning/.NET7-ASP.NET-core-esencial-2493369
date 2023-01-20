using demowebapp.Dal;
using demowebapp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace demowebapp.Pages
{
    public class PetEditModel : PageModel
    {
        private readonly WpmDbContext dbContext;

        [BindProperty]
        public Pet Pet { get; set; }

        public SelectList Breeds { get; set; }

        public PetEditModel(WpmDbContext dbContext)
        {
            this.dbContext = dbContext;
            var breeds = dbContext
                .Breeds
                .Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();
            Breeds = new SelectList(breeds, "Value", "Text");
        }
        public void OnGet(int id)
        {
            Pet = dbContext.Pets
                        .Where(p => p.Id == id)
                        .First();
        }

        public IActionResult OnPost()
        {
            dbContext.Update(Pet);
            dbContext.SaveChanges();
            return RedirectToPage("Pets");
        }
    }
}
