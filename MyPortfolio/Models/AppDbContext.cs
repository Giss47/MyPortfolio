using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}