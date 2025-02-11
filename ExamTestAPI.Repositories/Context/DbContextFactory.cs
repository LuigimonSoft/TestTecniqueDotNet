using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTestAPI.Repositories.Context
{
    public static class DbContextFactory
    {
        public static T Create<T>(string dataBaseName) where T : DbContext
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(dataBaseName)
                .Options;

            var context = (T)Activator.CreateInstance(typeof(T), options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
