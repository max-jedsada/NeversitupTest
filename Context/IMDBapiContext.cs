using DAL.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class IMDBapiContext : DbContext
    {
        public IMDBapiContext(DbContextOptions<IMDBapiContext> option) : base(option)
        {
        }

        public virtual DbSet<Movies> Movies { get; set; }

    }
}
