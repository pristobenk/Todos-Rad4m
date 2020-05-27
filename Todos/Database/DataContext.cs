using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todos.Models;

namespace Todos.Database
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string ConnectionString = configuration.GetConnectionString("Default");

            optionsBuilder.UseSqlServer(ConnectionString);
           
        }
    }
}
