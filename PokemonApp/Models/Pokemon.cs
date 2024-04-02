using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonApp.Models
{
    public class Pokemon
    {
        [Key]
        public int PokemonId { get; set; }

        [BindProperty]
        public int SelectedTypeId { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Height { get; set; }
        
        public int Weight { get; set; }

        public string ImageName { get; set; } = string.Empty;

        public bool IsGood { get; set; }

        // Navigation property
        public PokemonType PokemonType { get; set; } = new();
    }
}
