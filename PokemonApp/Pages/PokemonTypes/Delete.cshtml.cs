using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PokemonApp.Models;

namespace PokemonApp.Pages.PokemonTypes
{
    public class DeleteModel : PageModel
    {
        private readonly PokemonApp.Models.PokemonAppContext _context;

        public DeleteModel(PokemonApp.Models.PokemonAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PokemonType PokemonType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PokemonTypes == null)
            {
                return NotFound();
            }

            var pokemontype = await _context.PokemonTypes.FirstOrDefaultAsync(m => m.PokemonTypeId == id);

            if (pokemontype == null)
            {
                return NotFound();
            }
            else 
            {
                PokemonType = pokemontype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PokemonTypes == null)
            {
                return NotFound();
            }
            var pokemontype = await _context.PokemonTypes.FindAsync(id);

            if (pokemontype != null)
            {
                PokemonType = pokemontype;
                _context.PokemonTypes.Remove(PokemonType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
