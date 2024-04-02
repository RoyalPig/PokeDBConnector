using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PokemonApp.Models;

namespace PokemonApp.Pages.PokemonTypes
{
    public class CreateModel : PageModel
    {
        private readonly PokemonApp.Models.PokemonAppContext _context;

        public CreateModel(PokemonApp.Models.PokemonAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PokemonType PokemonType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.PokemonTypes == null || PokemonType == null)
            {
                return Page();
            }

            _context.PokemonTypes.Add(PokemonType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
