using Magic_API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Magic_API.Datos
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext>options) : base(options)
       {

       }
        public DbSet<Magic> Magics { get; set; }


    }
}
