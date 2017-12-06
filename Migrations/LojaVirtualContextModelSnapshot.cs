﻿// <auto-generated />
using loja_virtual.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace lojavirtual.Migrations
{
    [DbContext(typeof(LojaVirtualContext))]
    partial class LojaVirtualContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("loja_virtual.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("loja_virtual.Models.Preco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int?>("ProdutoId");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Preco");
                });

            modelBuilder.Entity("loja_virtual.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoriaId");

                    b.Property<DateTime>("DataValidade");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("loja_virtual.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Nome");

                    b.Property<string>("Password");

                    b.Property<string>("Tipo");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("loja_virtual.Models.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<DateTime>("DataAceite");

                    b.Property<DateTime>("DataExpedicao");

                    b.Property<int>("IdCliente");

                    b.Property<string>("Situacao");

                    b.HasKey("Id");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("loja_virtual.Models.VendaProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PrecoId");

                    b.Property<int?>("ProdutoId");

                    b.Property<int?>("VendaId");

                    b.HasKey("Id");

                    b.HasIndex("PrecoId");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("VendaId");

                    b.ToTable("VendaProduto");
                });

            modelBuilder.Entity("loja_virtual.Models.Preco", b =>
                {
                    b.HasOne("loja_virtual.Models.Produto")
                        .WithMany("HistoricoPreco")
                        .HasForeignKey("ProdutoId");
                });

            modelBuilder.Entity("loja_virtual.Models.Produto", b =>
                {
                    b.HasOne("loja_virtual.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");
                });

            modelBuilder.Entity("loja_virtual.Models.VendaProduto", b =>
                {
                    b.HasOne("loja_virtual.Models.Preco", "Preco")
                        .WithMany()
                        .HasForeignKey("PrecoId");

                    b.HasOne("loja_virtual.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");

                    b.HasOne("loja_virtual.Models.Venda", "Venda")
                        .WithMany()
                        .HasForeignKey("VendaId");
                });
#pragma warning restore 612, 618
        }
    }
}
