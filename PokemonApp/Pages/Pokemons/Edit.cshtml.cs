using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonApp.Models;

namespace PokemonApp.Pages.Pokemons
{
    public class EditModel : PageModel
    {
        private readonly PokemonApp.Models.PokemonAppContext _context;

        public EditModel(PokemonApp.Models.PokemonAppContext context)
        {
            _context = context;
        }
        public IFormFile UploadedImage { get; set; }


        [BindProperty]
        public Pokemon Pokemon { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pokemons == null)
            {
                return NotFound();
            }

            var pokemon =  await _context.Pokemons.FirstOrDefaultAsync(m => m.PokemonId == id);
            if (pokemon == null)
            {
                return NotFound();
            }
            Pokemon = pokemon;
           ViewData["TypeId"] = new SelectList(_context.PokemonTypes, "PokemonTypeId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pokemon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonExists(Pokemon.PokemonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PokemonExists(int id)
        {
          return (_context.Pokemons?.Any(e => e.PokemonId == id)).GetValueOrDefault();
        }
    }
}
