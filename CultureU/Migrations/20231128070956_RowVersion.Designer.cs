﻿// <auto-generated />
using System;
using CultureU.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CultureU.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20231128070956_RowVersion")]
    partial class RowVersion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("CultureU.Models.Alumno", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("FirstName");

                    b.Property<DateTime>("InscripcionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Alumno", (string)null);
                });

            modelBuilder.Entity("CultureU.Models.Directiva", b =>
                {
                    b.Property<int>("ProfesorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ProfesorID");

                    b.ToTable("Directivas");
                });

            modelBuilder.Entity("CultureU.Models.Escuela", b =>
                {
                    b.Property<int>("EscuelaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProfesorID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("EscuelaID");

                    b.HasIndex("ProfesorID");

                    b.ToTable("Escuelas");
                });

            modelBuilder.Entity("CultureU.Models.Inscripcion", b =>
                {
                    b.Property<int>("InscripcionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlumnoID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MateriaID")
                        .HasColumnType("INTEGER");

                    b.HasKey("InscripcionID");

                    b.HasIndex("AlumnoID");

                    b.HasIndex("MateriaID");

                    b.ToTable("Inscripciones");
                });

            modelBuilder.Entity("CultureU.Models.Materia", b =>
                {
                    b.Property<int>("MateriaID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Credits")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EscuelaID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("MateriaID");

                    b.HasIndex("EscuelaID");

                    b.ToTable("Materia", (string)null);
                });

            modelBuilder.Entity("CultureU.Models.Profesor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("FirstName");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Profesor", (string)null);
                });

            modelBuilder.Entity("MateriaProfesor", b =>
                {
                    b.Property<int>("MateriasMateriaID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProfesoresID")
                        .HasColumnType("INTEGER");

                    b.HasKey("MateriasMateriaID", "ProfesoresID");

                    b.HasIndex("ProfesoresID");

                    b.ToTable("MateriaProfesor");
                });

            modelBuilder.Entity("CultureU.Models.Directiva", b =>
                {
                    b.HasOne("CultureU.Models.Profesor", "Profesor")
                        .WithOne("Directiva")
                        .HasForeignKey("CultureU.Models.Directiva", "ProfesorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("CultureU.Models.Escuela", b =>
                {
                    b.HasOne("CultureU.Models.Profesor", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorID");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("CultureU.Models.Inscripcion", b =>
                {
                    b.HasOne("CultureU.Models.Alumno", "Alumno")
                        .WithMany("Inscripciones")
                        .HasForeignKey("AlumnoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CultureU.Models.Materia", "Materia")
                        .WithMany("Inscripciones")
                        .HasForeignKey("MateriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("CultureU.Models.Materia", b =>
                {
                    b.HasOne("CultureU.Models.Escuela", "Escuela")
                        .WithMany("Materias")
                        .HasForeignKey("EscuelaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escuela");
                });

            modelBuilder.Entity("MateriaProfesor", b =>
                {
                    b.HasOne("CultureU.Models.Materia", null)
                        .WithMany()
                        .HasForeignKey("MateriasMateriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CultureU.Models.Profesor", null)
                        .WithMany()
                        .HasForeignKey("ProfesoresID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CultureU.Models.Alumno", b =>
                {
                    b.Navigation("Inscripciones");
                });

            modelBuilder.Entity("CultureU.Models.Escuela", b =>
                {
                    b.Navigation("Materias");
                });

            modelBuilder.Entity("CultureU.Models.Materia", b =>
                {
                    b.Navigation("Inscripciones");
                });

            modelBuilder.Entity("CultureU.Models.Profesor", b =>
                {
                    b.Navigation("Directiva");
                });
#pragma warning restore 612, 618
        }
    }
}
