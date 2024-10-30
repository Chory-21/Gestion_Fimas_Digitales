// Data/Db_context.cs
using Gestion_Fimas_Digitales.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Fimas_Digitales.Data
{
    public class Db_context : DbContext
    {
        public Db_context(DbContextOptions<Db_context> options) : base(options) { }

        public DbSet<FirmaDigital> Firmas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}