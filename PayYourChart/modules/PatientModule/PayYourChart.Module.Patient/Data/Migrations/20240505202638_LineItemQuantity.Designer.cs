﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayYourChart.Module.Patient;

#nullable disable

namespace PayYourChart.Module.Patient.Data.Migrations
{
    [DbContext(typeof(EfPatientContext))]
    [Migration("20240505202638_LineItemQuantity")]
    partial class LineItemQuantity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("PayYourChart.Module.Patient.Bill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("PayYourChart.Module.Patient.LineItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("BillId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfService")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("ItemCatalogId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<short>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("LineItem");
                });

            modelBuilder.Entity("PayYourChart.Module.Patient.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SSN")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SSN", "DateOfBirth")
                        .IsUnique();

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("PayYourChart.Module.Patient.Bill", b =>
                {
                    b.HasOne("PayYourChart.Module.Patient.Patient", null)
                        .WithMany("Bills")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PayYourChart.Module.Patient.LineItem", b =>
                {
                    b.HasOne("PayYourChart.Module.Patient.Bill", null)
                        .WithMany("LineItems")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PayYourChart.Module.Patient.Bill", b =>
                {
                    b.Navigation("LineItems");
                });

            modelBuilder.Entity("PayYourChart.Module.Patient.Patient", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
