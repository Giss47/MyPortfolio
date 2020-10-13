using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "Mary", Department = Dept.Hr, Email = "mary@alayditech.com" },
                new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "john@alayditech.com" },
                new Employee() { Id = 3, Name = "Sam", Department = Dept.IT, Email = "sam@alayditech.com" }

                );
        }
    }
}