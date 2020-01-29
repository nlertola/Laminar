using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Laminar.Data
{
    public class LaminarContext : DbContext
    {
        public LaminarContext(DbContextOptions<LaminarContext> options)
            : base(options)
        {
        }

        //public DbSet<Print> Print { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // The line below makes all relations not cascade.
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
