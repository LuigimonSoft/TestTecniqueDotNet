using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTestAPI.Repositories.Context
{
    public static class EmployeeDbContextFactory
    {
        public static EmployeeDbContext Create(string dataBaseName)
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(dataBaseName)
                .Options;

            var context = new EmployeeDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
