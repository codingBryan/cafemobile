﻿// <auto-generated />
using System;
using CafeMobile_api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CafeMobile_api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231114035052_mig1")]
    partial class mig1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("image")
                        .HasColumnType("longblob");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CategoryId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Coupon", b =>
                {
                    b.Property<int>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("image")
                        .HasColumnType("longtext");

                    b.Property<bool>("is_active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.HasKey("CouponId");

                    b.ToTable("coupons");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Coupon_meal", b =>
                {
                    b.Property<int>("CouponId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.HasKey("CouponId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("coupon_meals");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.ItemSales", b =>
                {
                    b.Property<string>("ItemName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("sold_on")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<double>("totalSales")
                        .HasColumnType("double");

                    b.Property<int>("unitsSold")
                        .HasColumnType("int");

                    b.HasKey("ItemName", "sold_on");

                    b.ToTable("item_sales");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("image")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.Property<double?>("price_CP")
                        .HasColumnType("double");

                    b.HasKey("MealId");

                    b.ToTable("meals");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("audience")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("notifications");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Parent", b =>
                {
                    b.Property<int>("ParentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password_hash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("profile_pic")
                        .HasColumnType("longtext");

                    b.HasKey("ParentId");

                    b.ToTable("parents");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Parents_Students", b =>
                {
                    b.Property<int>("parentId")
                        .HasColumnType("int");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.HasKey("parentId", "studentId");

                    b.HasIndex("studentId");

                    b.ToTable("parents_students");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Redemption", b =>
                {
                    b.Property<Guid>("RedemptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("scanned")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("scanned_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("RedemptionId");

                    b.HasIndex("StudentId");

                    b.ToTable("redemptions");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Redemption_meal", b =>
                {
                    b.Property<Guid>("RedemptionId")
                        .HasColumnType("char(36)");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.HasKey("RedemptionId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("redemption_meals");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("RedemptionId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("cooking")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("created_at")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("mealId")
                        .HasColumnType("int");

                    b.Property<bool>("pending")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("prepared")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.Property<int>("units")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RedemptionId");

                    b.HasIndex("studentId");

                    b.ToTable("sales");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Staff", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password_hash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("profile_pic")
                        .HasColumnType("longtext");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("staff");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("adm_no")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("cp_balance")
                        .HasColumnType("double");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password_hash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("profile_pic")
                        .HasColumnType("longtext");

                    b.HasKey("StudentId");

                    b.ToTable("students");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.StudentCoupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CouponId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<double>("balance")
                        .HasColumnType("double");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("image")
                        .HasColumnType("longtext");

                    b.Property<bool>("is_active")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CouponId");

                    b.HasIndex("StudentId");

                    b.ToTable("studentCoupons");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<double>("amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("purpose")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TransactionId");

                    b.HasIndex("ParentId");

                    b.HasIndex("StudentId");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Coupon_meal", b =>
                {
                    b.HasOne("CafeMobile_api.Models.Entities.Coupon", "Coupon")
                        .WithMany("couponMeals")
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeMobile_api.Models.Entities.Meal", "Meal")
                        .WithMany("coupons")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coupon");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Parents_Students", b =>
                {
                    b.HasOne("CafeMobile_api.Models.Entities.Parent", "Parent")
                        .WithMany("students")
                        .HasForeignKey("parentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeMobile_api.Models.Entities.Student", "Student")
                        .WithMany("parents")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Redemption", b =>
                {
                    b.HasOne("CafeMobile_api.Models.Entities.Student", "Student")
                        .WithMany("redemptions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Redemption_meal", b =>
                {
                    b.HasOne("CafeMobile_api.Models.Entities.Meal", "Meal")
                        .WithMany("redemptions")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeMobile_api.Models.Entities.Redemption", "Redemption")
                        .WithMany("meals")
                        .HasForeignKey("RedemptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("Redemption");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Sale", b =>
                {
                    b.HasOne("CafeMobile_api.Models.Entities.Redemption", "Redemption")
                        .WithMany("sales")
                        .HasForeignKey("RedemptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeMobile_api.Models.Entities.Student", "Student")
                        .WithMany("sales")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Redemption");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.StudentCoupon", b =>
                {
                    b.HasOne("CafeMobile_api.Models.Entities.Coupon", "Coupon")
                        .WithMany("purchasedCoupons")
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeMobile_api.Models.Entities.Student", "Student")
                        .WithMany("coupons")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coupon");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Transaction", b =>
                {
                    b.HasOne("CafeMobile_api.Models.Entities.Parent", "Parent")
                        .WithMany("transactions")
                        .HasForeignKey("ParentId");

                    b.HasOne("CafeMobile_api.Models.Entities.Student", "Student")
                        .WithMany("transactions")
                        .HasForeignKey("StudentId");

                    b.Navigation("Parent");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Coupon", b =>
                {
                    b.Navigation("couponMeals");

                    b.Navigation("purchasedCoupons");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Meal", b =>
                {
                    b.Navigation("coupons");

                    b.Navigation("redemptions");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Parent", b =>
                {
                    b.Navigation("students");

                    b.Navigation("transactions");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Redemption", b =>
                {
                    b.Navigation("meals");

                    b.Navigation("sales");
                });

            modelBuilder.Entity("CafeMobile_api.Models.Entities.Student", b =>
                {
                    b.Navigation("coupons");

                    b.Navigation("parents");

                    b.Navigation("redemptions");

                    b.Navigation("sales");

                    b.Navigation("transactions");
                });
#pragma warning restore 612, 618
        }
    }
}