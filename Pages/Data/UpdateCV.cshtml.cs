using System;
using System.Threading.Tasks;
using CVProject.Models;
using CVProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVProject.Pages.Data
{
    public class UpdateCVModel : PageModel
    {
        
        public string[] skillsList = { "java", "C", "C++", "C#", "python", "javaScript" };
        [BindProperty]
        public UpdateCVCommand Input { get; set; }
        [BindProperty(SupportsGet =true)]
        public int id { get; set; }

        private readonly CVService _service;

        public UpdateCVModel(CVService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet()
        {

            Input = await _service.GetCVForUpdate(id);
            if (Input is null)
            {
                // If id is not for a valid Recipe, generate a 404 error page
                // TODO: Add status code pages middleware to show friendly 404 page
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {/*
            try
            {*/
                if (ModelState.IsValid)
                {
                    await _service.UpdateRecipe(Input);
                    return RedirectToPage("ViewCV", new { id = Input.Id });
                }
            /*}
            catch (Exception)
            {
                // TODO: Log error
                // Add a model-level error by using an empty string key
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured saving the recipe"
                    );
            }*/

            //If we got to here, something went wrong
            return Page();
        }
    }
}
