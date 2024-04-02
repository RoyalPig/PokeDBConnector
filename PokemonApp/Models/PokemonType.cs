using System.ComponentModel.DataAnnotations;

namespace PokemonApp.Models
{
    public class PokemonType
    {
        [Key]
        public int PokemonTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        //VVVV prob delete later, no refrences 
        public List <Pokemon> Pokemons { get; set; } = new List <Pokemon>();
    }
}
