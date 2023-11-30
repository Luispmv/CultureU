using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CultureU.Models;

namespace CultureU.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Materia> Materias { get; set; }

        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Directiva> Directivas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Materia>().ToTable("Materia");
            // modelBuilder.Entity<Inscripcion>().ToTable("Inscripcion");
            // modelBuilder.Entity<Alumno>().ToTable("Alumno");
            modelBuilder.Entity<Materia>().ToTable(nameof(Materia))
                .HasMany(c => c.Profesores)
                .WithMany(i => i.Materias);
            modelBuilder.Entity<Alumno>().ToTable(nameof(Alumno));
            modelBuilder.Entity<Profesor>().ToTable(nameof(Profesor));
            modelBuilder.Entity<Escuela>()
            .Property(d => d.ConcurrencyToken)
            .IsConcurrencyToken();
        }
        public DbSet<CultureU.Models.Alumno> Alumno { get; set; } = default!;
    }
}
