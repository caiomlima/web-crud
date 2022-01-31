﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto_Web_CRUD.Data;

namespace Projeto_Web_CRUD.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20220131184320_crud_4.0_sqlite")]
    partial class crud_40_sqlite
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("Projeto_Web_CRUD.Models.Produto", b =>
                {
                    b.Property<int?>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("VendedorId")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.HasKey("ProdutoId");

                    b.HasIndex("VendedorId");

                    b.ToTable("produto");
                });

            modelBuilder.Entity("Projeto_Web_CRUD.Models.Vendedor", b =>
                {
                    b.Property<int?>("VendedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("VendedorId");

                    b.ToTable("vendedores");
                });

            modelBuilder.Entity("Projeto_Web_CRUD.Models.Produto", b =>
                {
                    b.HasOne("Projeto_Web_CRUD.Models.Vendedor", "Vendedor")
                        .WithMany("Produtos")
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("Projeto_Web_CRUD.Models.Vendedor", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
