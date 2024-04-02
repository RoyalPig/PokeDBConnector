using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PokemonApp.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace PokemonApp.Pages.Pokemons
{
    public class CreateModel : PageModel
    {
        private readonly PokemonApp.Models.PokemonAppContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(PokemonApp.Models.PokemonAppContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            ViewData["TypeId"] = new SelectList(_context.PokemonTypes, "PokemonTypeId", "Name");
            return Page();
        }

        [BindProperty]
        public Pokemon Pokemon { get; set; } = default!;
        [BindProperty]
        public IFormFile UploadedImage { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine("THIS IS ERROR: " + error.ErrorMessage);
                    }
                }

                ViewData["TypeId"] = new SelectList(_context.PokemonTypes, "PokemonTypeId", "Name");
                return Page();
            }

            // Handle the uploaded image
            if (UploadedImage != null && UploadedImage.Length > 0)
            {
                string fileName = DateTime.Now.ToString("yyy-MM-dd-HH-mm-ss_") + UploadedImage.FileName;
                string filePath = Path.Combine(_env.WebRootPath, "images/pokemons", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(stream);
                }

                Pokemon.ImageName = fileName; // Storing only the filename in the database
            }

            // This part ensures that the PokemonType related to this Pokemon is not added again to the database
            var existingType = _context.PokemonTypes.Find(Pokemon.SelectedTypeId);
            if (existingType != null)
            {
                _context.Attach(existingType).State = EntityState.Unchanged;
                Pokemon.PokemonType = existingType;
            }

            try
            {
                _context.Pokemons.Add(Pokemon);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or print the exception message to see if there's a specific error.
                Console.WriteLine(ex.Message);
            }

            return RedirectToPage("./Index");
        }
    }
}
