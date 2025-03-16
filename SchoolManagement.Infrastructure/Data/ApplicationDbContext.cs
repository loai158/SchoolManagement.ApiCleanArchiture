using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities;

namespace SchoolManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Student> Students { get; set; }


        public DbSet<Subject> Subjects { get; set; }

        public DbSet<DepartmetSubject> DepartmetSubjects { get; set; }

        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Ins_Subject> Ins_Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DepartmetSubject>().HasKey(x => new { x.SubID, x.DID });
            modelBuilder.Entity<Ins_Subject>().HasKey(x => new { x.SubId, x.InsId });
            modelBuilder.Entity<StudentSubject>().HasKey(x => new { x.SubID, x.StudID });
            modelBuilder.Entity<Instructor>().HasOne(x => x.Supervisor).WithMany(x => x.Instructors)
                .HasForeignKey(x => x.SupervisorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Department>()
                .HasOne(x => x.Instructor)
                .WithOne(x => x.departmentManager)
                .HasForeignKey<Department>(x => x.InsManger).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
