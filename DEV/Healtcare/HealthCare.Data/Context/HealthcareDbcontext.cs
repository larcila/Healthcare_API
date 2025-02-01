using HealthCare.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Data.Context
{
    public class HealthcareDbcontext: DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        //}

        public HealthcareDbcontext(DbContextOptions<HealthcareDbcontext> options)
            :base(options)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Follow_Up> Follow_Ups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Role_By_User> Roles_By_Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Role_ID = 1, Name = "ADMIN", Description = "the user has access to the entire system" },
                new Role { Role_ID = 2, Name = "USER", Description = "user has limited access" }
            );

            Primery_Keys(modelBuilder);
            Index_On_Table(modelBuilder);
            Dependency_On_Table(modelBuilder);
        }

        /// <summary>
        /// Define primary keys
        /// </summary>
        protected void Primery_Keys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasKey(x => x.Patient_ID);
            modelBuilder.Entity<User>().HasKey(x => x.User_ID);
            modelBuilder.Entity<Follow_Up>().HasKey(x => x.Follow_Up_ID);
            modelBuilder.Entity<Role>().HasKey(x => x.Role_ID);
            modelBuilder.Entity<Role_By_User>().HasKey(x => x.Role_By_User_Id);
        }

        /// <summary>
        /// Index on table
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected void Index_On_Table(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role_By_User>()
                .HasIndex(r => new { r.User_ID, r.Role_ID })
                .IsUnique();
        }

        protected void Dependency_On_Table(ModelBuilder modelBuilder)
        {
            // Relationship between  Role_By_User and User
            modelBuilder.Entity<Role_By_User>()
                .HasOne(r => r.User) // Relationship with User
                .WithMany(u => u.Role_By_Users) // User has meny Role_By_User
                .HasForeignKey(r => r.User_ID) // Foreign key on Role_By_User
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            // Relationship between  Role_By_User and Role
            modelBuilder.Entity<Role_By_User>()
                .HasOne(r => r.Role) // Relationship with Role
                .WithMany(u => u.User_By_Role) // Role has meny Role_By_User
                .HasForeignKey(r => r.Role_ID) // Foreign key on Role_By_User
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            // Relationship between  Follow_Up_By_Patient and Patient
            modelBuilder.Entity<Follow_Up>()
                .HasOne(r => r.Patient) // Relationship with Patient
                .WithMany(u => u.Follow_Up_By_Patient) // Patient has meny Follow_Up
                .HasForeignKey(r => r.Patient_ID) // Foreign key on Follow_Up
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete
        }
    }
}
