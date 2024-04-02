using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonApp.Models;

namespace PokemonApp.Pages.PokemonTypes
{
    public class EditModel : PageModel
    {
        private readonly PokemonApp.Models.PokemonAppContext _context;

        public EditModel(PokemonApp.Models.PokemonAppContext context)
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

            var pokemontype =  await _context.PokemonTypes.FirstOrDefaultAsync(m => m.PokemonTypeId == id);
            if (pokemontype == null)
            {
                return NotFound();
            }
            PokemonType = pokemontype;
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

            _context.Attach(PokemonType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PokemonTypeExists(PokemonType.PokemonTypeId))
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

        private bool PokemonTypeExists(int id)
        {
          return (_context.PokemonTypes?.Any(e => e.PokemonTypeId == id)).GetValueOrDefault();
        }
    }
}
