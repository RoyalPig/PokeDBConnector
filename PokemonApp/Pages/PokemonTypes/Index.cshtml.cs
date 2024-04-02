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
    public class IndexModel : PageModel
    {
        private readonly PokemonApp.Models.PokemonAppContext _context;

        public IndexModel(PokemonApp.Models.PokemonAppContext context)
        {
            _context = context;
        }

        public IList<PokemonType> PokemonType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PokemonTypes != null)
            {
                PokemonType = await _context.PokemonTypes.ToListAsync();
            }
        }
    }
}
