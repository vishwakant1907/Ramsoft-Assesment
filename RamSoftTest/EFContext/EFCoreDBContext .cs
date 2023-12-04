using Microsoft.EntityFrameworkCore;
using RamSoftTest.Model;

namespace RamSoftTest.EFContext
{
    public class EFCoreDBContext : DbContext
    {
        public EFCoreDBContext()
        {
        }

        public EFCoreDBContext(DbContextOptions<EFCoreDBContext> options) : base(options)
        {
        }

        public virtual DbSet<TaskManager> TaskManager { get; set; }
    }
}
