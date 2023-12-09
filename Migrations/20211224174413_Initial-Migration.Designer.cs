﻿// <auto-generated />
using System;
using BroadBandBillingPaymentSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BroadbandBillingPaymentSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211224174413_Initial-Migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Account", b =>
                {
                    b.Property<string>("account_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tarrif_plan_id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("account_id");

                    b.HasIndex("tarrif_plan_id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Admin", b =>
                {
                    b.Property<string>("admin_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("admin_id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Bill", b =>
                {
                    b.Property<string>("bill_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("account_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("bill_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("customer_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("due_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("payment_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("payment_mode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("bill_id");

                    b.HasIndex("account_id");

                    b.HasIndex("customer_id");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Customer", b =>
                {
                    b.Property<string>("customer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("account_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("f_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("l_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customer_id");

                    b.HasIndex("account_id")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Feedback", b =>
                {
                    b.Property<string>("feedback_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("admin_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("customer_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<string>("reply")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("reply_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("review")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("feedback_id");

                    b.HasIndex("admin_id");

                    b.HasIndex("customer_id");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.TarrifPlan", b =>
                {
                    b.Property<string>("tarrif_plan_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("admin_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tarrif_plan_id");

                    b.HasIndex("admin_id");

                    b.ToTable("TarrifPlan");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Account", b =>
                {
                    b.HasOne("BroadBandBillingPaymentSystem.Data.Models.TarrifPlan", "TarrifPlan")
                        .WithMany("Account")
                        .HasForeignKey("tarrif_plan_id");

                    b.Navigation("TarrifPlan");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Bill", b =>
                {
                    b.HasOne("BroadBandBillingPaymentSystem.Data.Models.Account", "Account")
                        .WithMany("Bill")
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BroadBandBillingPaymentSystem.Data.Models.Customer", "Customer")
                        .WithMany("Bill")
                        .HasForeignKey("customer_id");

                    b.Navigation("Account");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Customer", b =>
                {
                    b.HasOne("BroadBandBillingPaymentSystem.Data.Models.Account", "Account")
                        .WithOne("Customer")
                        .HasForeignKey("BroadBandBillingPaymentSystem.Data.Models.Customer", "account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Feedback", b =>
                {
                    b.HasOne("BroadBandBillingPaymentSystem.Data.Models.Admin", "Admin")
                        .WithMany("Feedback")
                        .HasForeignKey("admin_id");

                    b.HasOne("BroadBandBillingPaymentSystem.Data.Models.Customer", "Customer")
                        .WithMany("Feedback")
                        .HasForeignKey("customer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.TarrifPlan", b =>
                {
                    b.HasOne("BroadBandBillingPaymentSystem.Data.Models.Admin", "Admin")
                        .WithMany("TarrifPlan")
                        .HasForeignKey("admin_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Account", b =>
                {
                    b.Navigation("Bill");

                    b.Navigation("Customer")
                        .IsRequired();
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Admin", b =>
                {
                    b.Navigation("Feedback");

                    b.Navigation("TarrifPlan");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.Customer", b =>
                {
                    b.Navigation("Bill");

                    b.Navigation("Feedback");
                });

            modelBuilder.Entity("BroadBandBillingPaymentSystem.Data.Models.TarrifPlan", b =>
                {
                    b.Navigation("Account");
                });
                
#pragma warning restore 612, 618
        }
    }
}
