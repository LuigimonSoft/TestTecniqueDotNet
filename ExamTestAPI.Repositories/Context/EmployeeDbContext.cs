using ExamTestAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExamTestAPI.Repositories.Context
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Salary).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Email).IsRequired();
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Employee>().Property(e => e.Position).IsRequired();
            modelBuilder.Entity<Employee>().HasData();
        }
    }
}
