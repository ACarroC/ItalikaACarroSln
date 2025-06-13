using Microsoft.EntityFrameworkCore;
using MusicSchools.Domain.Entities;

namespace MusicSchools.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MusicSchool> MusicSchools => Set<MusicSchool>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();

        public DbSet<StudentTeacher> StudentTeachers => Set<StudentTeacher>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MusicSchool>()
                .HasMany(s => s.Teachers)
                .WithOne(t => t.MusicSchool)
                .HasForeignKey(t => t.MusicSchoolId);  

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.MusicSchool)
                .WithMany(s => s.Teachers)
                .HasForeignKey(t => t.MusicSchoolId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.MusicSchool)
                .WithMany(s => s.Students)
                .HasForeignKey(s => s.MusicSchoolId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Students)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.SetNull); // si se elimina el profesor, los alumnos quedan sin asignar

            modelBuilder.Entity<StudentTeacher>()
        .HasKey(st => new { st.StudentId, st.TeacherId });

            modelBuilder.Entity<StudentTeacher>()
       .ToTable("StudentTeacher");

            modelBuilder.Entity<StudentTeacher>()
                .HasOne(st => st.Student)
                .WithMany(s => s.StudentTeachers)
                .HasForeignKey(st => st.StudentId);

            modelBuilder.Entity<StudentTeacher>()
                .HasOne(st => st.Teacher)
                .WithMany(t => t.StudentTeachers)
                .HasForeignKey(st => st.TeacherId);


        }
    }
}
