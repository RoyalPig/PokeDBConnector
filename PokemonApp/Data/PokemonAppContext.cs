using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PokemonApp.Models
{
    public class PokemonAppContext : DbContext
    {
        public PokemonAppContext(DbContextOptions<PokemonAppContext> options) 
            : base(options)
        {
        }

        public DbSet<PokemonType> PokemonTypes { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
    }
}
