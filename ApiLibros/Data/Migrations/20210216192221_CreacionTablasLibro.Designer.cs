﻿// <auto-generated />
using System;
using ApiLibros.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiLibros.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210216192221_CreacionTablasLibro")]
    partial class CreacionTablasLibro
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiLibros.Models.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CiudadProcedencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Autor");
                });

            modelBuilder.Entity("ApiLibros.Models.Editorial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxLibros")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Editorial");
                });

            modelBuilder.Entity("ApiLibros.Models.Libro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Agno")
                        .HasColumnType("int");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroPaginas")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("autorId")
                        .HasColumnType("int");

                    b.Property<int>("editorialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("autorId");

                    b.HasIndex("editorialId");

                    b.ToTable("Libro");
                });

            modelBuilder.Entity("ApiLibros.Models.Libro", b =>
                {
                    b.HasOne("ApiLibros.Models.Autor", "Autor")
                        .WithMany()
                        .HasForeignKey("autorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiLibros.Models.Editorial", "Editorial")
                        .WithMany()
                        .HasForeignKey("editorialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Editorial");
                });
#pragma warning restore 612, 618
        }
    }
}
