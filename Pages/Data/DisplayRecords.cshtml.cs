using System.Collections.Generic;
using System.Threading.Tasks;
using CVProject.Models;
using CVProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVProject.Pages.Data
{
    public class DisplayRecordsModel : PageModel
    {
        public List<CVSummaryViewModel> list;

        private readonly CVService _service;
        public DisplayRecordsModel(CVService service)
        {
            _service = service;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            list = await _service.GetCVs();
            return Page();
        }
    }
}
