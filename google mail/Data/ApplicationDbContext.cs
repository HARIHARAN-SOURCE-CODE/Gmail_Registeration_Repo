
using google_mail.Models;
using Microsoft.EntityFrameworkCore;

namespace google_mail.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        { 
        
        }

        public DbSet<Register> Regtbl { get; set; }
        protected override void OnModelCreating(ModelBuilder Increment)
        {
            Increment.Entity<Register>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);
        
        
        }

    }
}
