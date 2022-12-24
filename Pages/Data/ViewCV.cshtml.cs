using System.Threading.Tasks;
using CVProject.Models;
using CVProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVProject.Pages.Data
{
    public class ViewCVModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int id { get; set; }
        [BindProperty(SupportsGet = true)]
        public string handler { get; set; } = null;

        public CVDetailViewModel CV { get; set; }
        private readonly CVService _service;
        public ViewCVModel(CVService service)
        {
            _service = service;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            //if no handler is sent
            //so just show the relative cv
           
            CV = await _service.GetRecipeDetail(id);
            if (CV is null)
            {
               // If id is not for a valid CV, generate a 404 error page
               // TODO: Add status code pages middleware to show friendly 404 page
               return NotFound();
            }
            return Page();
            
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteCV(id);

            return RedirectToPage("/Index");
        }
    }
}
