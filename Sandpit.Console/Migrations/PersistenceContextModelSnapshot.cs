﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sandpit.Console.Persistence;

namespace Sandpit.Console.Migrations
{
    [DbContext(typeof(PersistenceContext))]
    partial class PersistenceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23");

            modelBuilder.Entity("Sandpit.Console.Entities.DynamicEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("DynamicEntity");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "DE1"
                        },
                        new
                        {
                            ID = 2,
                            Name = "DE2"
                        },
                        new
                        {
                            ID = 3,
                            Name = "DE3"
                        });
                });

            modelBuilder.Entity("Sandpit.Console.Entities.EncapsulateParent1", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("EncapsulateParent1");
                });

            modelBuilder.Entity("Sandpit.Console.Entities.EncapsulateParent2", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("EncapsulateParent2");
                });

            modelBuilder.Entity("Sandpit.Console.Entities.SemiStaticEntityOwner", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SemiStaticEntity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("SemiStaticEntityOwner");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Owner1",
                            SemiStaticEntity = "DE1"
                        });
                });

            modelBuilder.Entity("Sandpit.Console.Entities.EncapsulateParent1", b =>
                {
                    b.OwnsOne("Sandpit.Console.Entities.Parent", "EncapsulatedParent", b1 =>
                        {
                            b1.Property<int>("EncapsulateParent1ID")
                                .HasColumnType("INTEGER");

                            b1.HasKey("EncapsulateParent1ID");

                            b1.ToTable("Parent1");

                            b1.WithOwner()
                                .HasForeignKey("EncapsulateParent1ID");

                            b1.OwnsMany("Sandpit.Console.Entities.Child", "Children", b2 =>
                                {
                                    b2.Property<int>("ID")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateTime>("Date")
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("ParentEncapsulateParent1ID")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("ID");

                                    b2.HasIndex("ParentEncapsulateParent1ID");

                                    b2.ToTable("Child1");

                                    b2.WithOwner("Parent")
                                        .HasForeignKey("ParentEncapsulateParent1ID");
                                });
                        });
                });

            modelBuilder.Entity("Sandpit.Console.Entities.EncapsulateParent2", b =>
                {
                    b.OwnsOne("Sandpit.Console.Entities.Parent", "EncapsulatedParent", b1 =>
                        {
                            b1.Property<int>("EncapsulateParent2ID")
                                .HasColumnType("INTEGER");

                            b1.HasKey("EncapsulateParent2ID");

                            b1.ToTable("EncapsulateParent2");

                            b1.WithOwner()
                                .HasForeignKey("EncapsulateParent2ID");

                            b1.OwnsMany("Sandpit.Console.Entities.Child", "Children", b2 =>
                                {
                                    b2.Property<int>("ID")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateTime>("Date")
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<int>("ParentEncapsulateParent2ID")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("ID");

                                    b2.HasIndex("ParentEncapsulateParent2ID");

                                    b2.ToTable("Child2");

                                    b2.WithOwner("Parent")
                                        .HasForeignKey("ParentEncapsulateParent2ID");
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
