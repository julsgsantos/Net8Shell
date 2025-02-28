using Microsoft.EntityFrameworkCore;

namespace BIZBOX.PSA.PERSISTENCE.Context
{
    public class BPSADbContext : BPSABaseDbContext
    {
        public BPSADbContext(DbContextOptions<BPSADbContext> options) : base(options)
        {
        }
    }
}
