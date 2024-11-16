﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plugins.DataStore.SQLite;

#nullable disable

namespace Plugins.DataStore.SQLite.ncrcontext
{
    [DbContext(typeof(NCRContext))]
    partial class NCRContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("EntitiesLayer.Models.EngPortion", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Disposition")
                        .HasColumnType("TEXT");

                    b.Property<int>("EngReview")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Notif")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RepID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RevDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RevNumber")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Update")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("EngPortions");
                });

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

                    b.Property<int>("EngPortionID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QualityPortionID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("EngPortionID");

                    b.HasIndex("QualityPortionID");

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

                    b.Property<string>("SapNo")
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

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("DefectDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProcessApplicable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuantityDefective")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RepID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("QualityPortions");
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

            modelBuilder.Entity("EntitiesLayer.Models.QualityPicture", b =>
                {
                    b.HasBaseType("EntitiesLayer.Models.UploadedFile");

                    b.Property<int>("QualityPortionID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("QualityPortionID");

                    b.HasDiscriminator().HasValue("QualityPicture");
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

            modelBuilder.Entity("EntitiesLayer.Models.NCRLog", b =>
                {
                    b.HasOne("EntitiesLayer.Models.EngPortion", "EngPortion")
                        .WithMany()
                        .HasForeignKey("EngPortionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntitiesLayer.Models.QualityPortion", "QualityPortion")
                        .WithMany()
                        .HasForeignKey("QualityPortionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EngPortion");

                    b.Navigation("QualityPortion");
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

                    b.Navigation("Product");
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

            modelBuilder.Entity("EntitiesLayer.Models.QualityPicture", b =>
                {
                    b.HasOne("EntitiesLayer.Models.QualityPortion", "QualityPortion")
                        .WithMany("qualityPictures")
                        .HasForeignKey("QualityPortionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QualityPortion");
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

            modelBuilder.Entity("EntitiesLayer.Models.QualityPortion", b =>
                {
                    b.Navigation("qualityPictures");
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
