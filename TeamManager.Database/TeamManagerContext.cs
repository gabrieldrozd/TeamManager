using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using TeamManager.Database.Models;

namespace TeamManager.Database
{
    public class TeamManagerContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeContact> EmployeeContact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TeamManagerDB.sqlite")}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Department>(x => x.Department) 
                .WithOne(y => y.User)
                .HasForeignKey<Department>(y => y.UserId);

            //modelBuilder.Entity<EmployeeContact>()
            //    .HasOne<Employee>(x => x.Employee)
            //    .WithOne(y => y.EmployeeContact)
            //    .HasForeignKey<Employee>(y => y.EmpContactId);

            //modelBuilder.Entity<Employee>()
            //    .HasOne<Department>(x => x.Department)
            //    .WithMany(y => y.Employee)
            //    .HasForeignKey(z => z.DepartmentId);

            //modelBuilder.Entity<>

            base.OnModelCreating(modelBuilder);
        }
    }
}
