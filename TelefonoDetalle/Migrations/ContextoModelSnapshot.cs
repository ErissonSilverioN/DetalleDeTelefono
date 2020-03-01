﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TelefonoDetalle.DAL;

namespace TelefonoDetalle.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("TelefonoDetalle.Entidades.Personas", b =>
                {
                    b.Property<int>("PersonaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cedula")
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonaId");

                    b.ToTable("personas");
                });

            modelBuilder.Entity("TelefonoDetalle.Entidades.TelefonosDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonasPersonaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoTelefono")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonasPersonaId");

                    b.ToTable("TelefonosDetalle");
                });

            modelBuilder.Entity("TelefonoDetalle.Entidades.TelefonosDetalle", b =>
                {
                    b.HasOne("TelefonoDetalle.Entidades.Personas", null)
                        .WithMany("Telefonos")
                        .HasForeignKey("PersonasPersonaId");
                });
#pragma warning restore 612, 618
        }
    }
}
