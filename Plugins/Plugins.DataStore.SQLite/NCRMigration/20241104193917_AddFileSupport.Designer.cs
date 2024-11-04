﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plugins.DataStore.SQLite;

#nullable disable

namespace Plugins.DataStore.SQLite.NCRMigration
{
    [DbContext(typeof(NCRContext))]
    [Migration("20241104193917_AddFileSupport")]
    partial class AddFileSupport
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("EntitiesLayer.Models.FileContent", b =>
                {
                    b.Property<int>("FileContentID")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.HasKey("FileContentID");

                    b.ToTable("FileContent");
                });

            modelBuilder.Entity("EntitiesLayer.Models.NCRLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("NCRLog");
                });

            modelBuilder.Entity("EntitiesLayer.Models.NCRLogHistory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChangedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ChangedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NCRLogID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("NCRLogID");

                    b.ToTable("NCRLogHistory");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SupplierID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProductNumber")
                        .IsUnique();

                    b.HasIndex("SupplierID", "Description")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EntitiesLayer.Models.QualityPortion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DefectDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("DefectPicture")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityDefective")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleRepID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("RoleRepID");

                    b.ToTable("QualityPortions");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Representative", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleInitial")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("FirstName", "MiddleInitial", "LastName")
                        .IsUnique();

                    b.ToTable("Representatives");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EntitiesLayer.Models.RoleRep", b =>
                {
                    b.Property<int>("RoleRepID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RepresentativeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleID")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoleRepID");

                    b.HasIndex("RepresentativeID");

                    b.HasIndex("RoleID");

                    b.ToTable("RoleReps");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("SupplierName")
                        .IsUnique();

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("EntitiesLayer.Models.UploadedFile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("UploadedFile");

                    b.HasDiscriminator().HasValue("UploadedFile");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EntitiesLayer.Models.ProductPicture", b =>
                {
                    b.HasBaseType("EntitiesLayer.Models.UploadedFile");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ProductID");

                    b.HasDiscriminator().HasValue("ProductPicture");
                });

            modelBuilder.Entity("EntitiesLayer.Models.FileContent", b =>
                {
                    b.HasOne("EntitiesLayer.Models.UploadedFile", "UploadedFile")
                        .WithOne("FileContent")
                        .HasForeignKey("EntitiesLayer.Models.FileContent", "FileContentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UploadedFile");
                });

            modelBuilder.Entity("EntitiesLayer.Models.NCRLogHistory", b =>
                {
                    b.HasOne("EntitiesLayer.Models.NCRLog", "NCRLog")
                        .WithMany("History")
                        .HasForeignKey("NCRLogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NCRLog");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Product", b =>
                {
                    b.HasOne("EntitiesLayer.Models.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("EntitiesLayer.Models.QualityPortion", b =>
                {
                    b.HasOne("EntitiesLayer.Models.Product", "Product")
                        .WithMany("QualityPortions")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntitiesLayer.Models.RoleRep", "RoleRep")
                        .WithMany("QualityPortions")
                        .HasForeignKey("RoleRepID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("RoleRep");
                });

            modelBuilder.Entity("EntitiesLayer.Models.RoleRep", b =>
                {
                    b.HasOne("EntitiesLayer.Models.Representative", "Representative")
                        .WithMany("RoleReps")
                        .HasForeignKey("RepresentativeID")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("EntitiesLayer.Models.Role", "Role")
                        .WithMany("RoleReps")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Representative");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EntitiesLayer.Models.ProductPicture", b =>
                {
                    b.HasOne("EntitiesLayer.Models.Product", "product")
                        .WithMany("ProductPictures")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("EntitiesLayer.Models.NCRLog", b =>
                {
                    b.Navigation("History");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Product", b =>
                {
                    b.Navigation("ProductPictures");

                    b.Navigation("QualityPortions");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Representative", b =>
                {
                    b.Navigation("RoleReps");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Role", b =>
                {
                    b.Navigation("RoleReps");
                });

            modelBuilder.Entity("EntitiesLayer.Models.RoleRep", b =>
                {
                    b.Navigation("QualityPortions");
                });

            modelBuilder.Entity("EntitiesLayer.Models.Supplier", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EntitiesLayer.Models.UploadedFile", b =>
                {
                    b.Navigation("FileContent");
                });
#pragma warning restore 612, 618
        }
    }
}