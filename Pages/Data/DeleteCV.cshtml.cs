using System.Threading.Tasks;
using CVProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVProject.Pages.Data
{
    public class DeleteCVModel : PageModel
    {
        public CVService _service;
        public DeleteCVModel(CVService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await _service.DeleteCV(id);

            return RedirectToPage("./DisplayRecords");
        }
    }
}
