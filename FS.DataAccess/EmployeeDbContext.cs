using FS.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
      public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
      {

      }

    public  DbSet<Employee> Employee { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Employee>(entity =>
      {
        entity.HasKey(x => x.Id);
        entity.ToTable("Employee");
      });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.UserId);
                entity.ToTable("User");
            });
    }

  }
}
