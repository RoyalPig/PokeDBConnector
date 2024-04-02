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
    public class DetailsModel : PageModel
    {
        private readonly PokemonApp.Models.PokemonAppContext _context;

        public DetailsModel(PokemonApp.Models.PokemonAppContext context)
        {
            _context = context;
        }

      public Pokemon Pokemon { get; set; } = default!;
        public IFormFile UploadedImage { get; set; }


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
    }
}
