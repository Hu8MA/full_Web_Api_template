using jwt.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jwt.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options ):base(options)
        {
            
        }

        public virtual DbSet<employee> emp {  get; set; }  
    }
}
