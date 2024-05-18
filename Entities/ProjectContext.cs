using DAL.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> option) : base(option)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }

    }
}
