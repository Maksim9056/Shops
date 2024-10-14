﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShopClassLibrary;

#nullable disable

namespace ShopClassLibrary.Migrations
{
    [DbContext(typeof(ShopData))]
    partial class ShopDataModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Category_Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Image_Category")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name_Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("OrdersName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name_Product")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductCount")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("ProductDataImage")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<long>("Product_IdId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProductsDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("Product_IdId");

                    b.HasIndex("StatusId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Product_category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Name_Product_CategoryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Name_Product_CategoryId");

                    b.ToTable("Product_category");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProjectDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProjectManagerId")
                        .HasColumnType("integer");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectVersion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("StatusId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Rights", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("RightsName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Rights");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.RightsUsers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("Id_RightsId")
                        .HasColumnType("bigint");

                    b.Property<int>("Id_UserId")
                        .HasColumnType("integer");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id_RightsId");

                    b.HasIndex("Id_UserId");

                    b.HasIndex("StatusId");

                    b.ToTable("RightsUsers");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Status", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Tasks_Shop", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AssignedUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.ToTable("SystemUrls");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("User_Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<long>("Year")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Order", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Product", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.Order", null)
                        .WithMany("Idproduct")
                        .HasForeignKey("OrderId");

                    b.HasOne("ShopClassLibrary.ModelShop.Product_category", "Product_Id")
                        .WithMany()
                        .HasForeignKey("Product_IdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product_Id");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Product_category", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.Category", "Name_Product_Category")
                        .WithMany()
                        .HasForeignKey("Name_Product_CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Name_Product_Category");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Project", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.User", "ProjectManager")
                        .WithMany()
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectManager");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Rights", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.RightsUsers", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.Rights", "Id_Rights")
                        .WithMany()
                        .HasForeignKey("Id_RightsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.User", "Id_User")
                        .WithMany()
                        .HasForeignKey("Id_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Id_Rights");

                    b.Navigation("Id_User");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Tasks_Shop", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.User", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("AssignedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedUser");

                    b.Navigation("Project");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.User", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Order", b =>
                {
                    b.Navigation("Idproduct");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
