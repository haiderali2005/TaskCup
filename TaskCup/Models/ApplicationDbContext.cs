using Microsoft.EntityFrameworkCore;
using TaskCup.Controllers;

namespace TaskCup.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions op):base(op)
        {
            
        }
        public DbSet<Userauth> userauths { get; set; }
        public DbSet<Adminauth> adminauths { get; set; }
        public DbSet<Tasks> tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tasks>().
                HasOne(a => a.userauth_).
                WithMany(b => b.Tasks)
                .HasForeignKey(f => f.U_Id)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
