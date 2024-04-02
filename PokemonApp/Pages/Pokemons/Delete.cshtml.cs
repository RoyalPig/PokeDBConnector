using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PokemonApp.Models;

namespace PokemonApp.Pages.Pokemons
{
    public class DeleteModel : PageModel
    {
        private readonly PokemonApp.Models.PokemonAppContext _context;

        public DeleteModel(PokemonApp.Models.PokemonAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Pokemon Pokemon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pokemons == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(m => m.PokemonId == id);

            if (pokemon == null)
            {
                return NotFound();
            }
            else 
            {
                Pokemon = pokemon;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pokemons == null)
            {
                return NotFound();
            }
            var pokemon = await _context.Pokemons.FindAsync(id);

            if (pokemon != null)
            {
                Pokemon = pokemon;
                _context.Pokemons.Remove(Pokemon);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
