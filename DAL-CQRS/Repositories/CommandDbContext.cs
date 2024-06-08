using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_CQRS.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL_CQRS.Repositories
{
    public class CommandDbContext : DbContext
    {
        public DbSet<Otel> iK_Otels { get; set; }
        public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
        {

        }

        static List<Otel> otelList = new List<Otel> {
         new() { OtelId = Guid.NewGuid(), Name = "TestOtel1", Price = 100, IsActive=true,OtelAdress="rize",OtelWebSite="www.neredekal.com" }
        };
        public static List<Otel> OtelList => otelList;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
