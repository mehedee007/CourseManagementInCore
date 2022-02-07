using CourseManagementInCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CourseManagementInCore.Models.ViewModels;

namespace CourseManagementInCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TSP> TSPs { get; set; }
        public DbSet<TraineeModuleDescription> TraineeModuleDescriptions { get; set; }
        public DbSet<TrainerExperience> TrainerExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Course>().Property(c => c.FeeWithVat).ValueGeneratedOnAddOrUpdate();
            builder.Entity<Course>().Property(c => c.FeeWithVat).UsePropertyAccessMode(PropertyAccessMode.Property);

        }

        public DbSet<CourseManagementInCore.Models.ViewModels.TrainerViewModelBase> TrainerViewModelBase { get; set; }
    }
}
