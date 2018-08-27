using System;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.EntityConfiguration;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions  options)
        : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.ApplyConfiguration(new CourseConfig());
            builder.ApplyConfiguration(new StudentConfig());
            builder.ApplyConfiguration(new );
            builder.ApplyConfiguration(new CourseConfig());
            builder.ApplyConfiguration(new CourseConfig());

            builder.Entity<StudentCourse>()
                .HasKey(x => new
                {
                    x.CourseId,
                    x.StudentId,
                });
        }
    }
}