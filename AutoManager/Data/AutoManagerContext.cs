using AutoManager.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace AutoManager.Data
{
    public class AutoManagerContext : DbContext
    {
        public AutoManagerContext(DbContextOptions<AutoManagerContext> options) : base(options) { }

        public DbSet<Veiculo> Veiculos { get; set; }
    }
}
