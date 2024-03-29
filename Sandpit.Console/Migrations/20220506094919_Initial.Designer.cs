﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sandpit.Console.Persistence;

namespace Sandpit.Console.Migrations
{
    [DbContext(typeof(PersistenceContext))]
    [Migration("20220506094919_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23");

            modelBuilder.Entity("Sandpit.Console.Entities.Foo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BarID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Foo");
                });
#pragma warning restore 612, 618
        }
    }
}
