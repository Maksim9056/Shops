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

                    b.Property<int>("Image_CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name_Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Image_CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("OriginalImageData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.ImageCopy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("CopyImageData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("FileSize")
                        .HasColumnType("integer");

                    b.Property<int>("ImageID")
                        .HasColumnType("integer");

                    b.Property<string>("Resolution")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ImageID");

                    b.ToTable("ImageCopy");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Count_product")
                        .HasColumnType("bigint");

                    b.Property<long>("Idproduct")
                        .HasColumnType("bigint");

                    b.Property<string>("OrdersName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

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

                    b.Property<long>("Category_IdId")
                        .HasColumnType("bigint");

                    b.Property<int>("Id_ProductDataImageId")
                        .HasColumnType("integer");

                    b.Property<string>("Name_Product")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ProductCount")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("ProductsDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Category_IdId");

                    b.HasIndex("Id_ProductDataImageId");

                    b.HasIndex("StatusId");

                    b.ToTable("Products");
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

                    b.Property<long>("ProjectManagerId")
                        .HasColumnType("bigint");

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

                    b.Property<long>("Id_UserId")
                        .HasColumnType("bigint");

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

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "Пользователь только что зарегистрировался",
                            StatusName = "Новый пользователь"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "Пользователь активен в системе",
                            StatusName = "Активный"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Пользователь ожидает подтверждения email",
                            StatusName = "Ожидание подтверждения"
                        },
                        new
                        {
                            Id = 4L,
                            Description = "Пользователь заблокирован из-за нарушения политики",
                            StatusName = "Заблокирован"
                        },
                        new
                        {
                            Id = 5L,
                            Description = "Пользователь удалил свою учетную запись",
                            StatusName = "Удален"
                        },
                        new
                        {
                            Id = 6L,
                            Description = "Товар в наличии",
                            StatusName = "Новый товар"
                        },
                        new
                        {
                            Id = 7L,
                            Description = "Товар не вналичии",
                            StatusName = "Удален товар"
                        },
                        new
                        {
                            Id = 8L,
                            Description = "Надо соберать",
                            StatusName = "Новый заказ"
                        },
                        new
                        {
                            Id = 9L,
                            Description = "Изменение при  сборки проверять",
                            StatusName = "Заказ изменен"
                        },
                        new
                        {
                            Id = 10L,
                            Description = "Заказ удален его делать не надо!",
                            StatusName = "Заказ отменен"
                        },
                        new
                        {
                            Id = 11L,
                            Description = "Заказ заново соберают",
                            StatusName = "Заказ переделывают"
                        },
                        new
                        {
                            Id = 12L,
                            Description = "Заказ готов можете его забрать!",
                            StatusName = "Заказ готов"
                        },
                        new
                        {
                            Id = 13L,
                            Description = "Заказ оплачен!",
                            StatusName = "Заказ оплачен"
                        },
                        new
                        {
                            Id = 14L,
                            Description = "Заказ сделан!",
                            StatusName = "Заказ сделан"
                        });
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Tasks_Shop", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AssignedUserId")
                        .HasColumnType("bigint");

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
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Id_User_ImageId")
                        .HasColumnType("integer");

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

                    b.Property<long>("Year")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id_User_ImageId");

                    b.HasIndex("StatusId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Сarte", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Bank_Carte")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Money_Account")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Сarte");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Category", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.Image", "Image_Category")
                        .WithMany()
                        .HasForeignKey("Image_CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image_Category");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.ImageCopy", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.Image", "Image")
                        .WithMany("ImageCopies")
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
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
                    b.HasOne("ShopClassLibrary.ModelShop.Category", "Category_Id")
                        .WithMany()
                        .HasForeignKey("Category_IdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.Image", "Id_ProductDataImage")
                        .WithMany()
                        .HasForeignKey("Id_ProductDataImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category_Id");

                    b.Navigation("Id_ProductDataImage");

                    b.Navigation("Status");
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
                    b.HasOne("ShopClassLibrary.ModelShop.Image", "Id_User_Image")
                        .WithMany()
                        .HasForeignKey("Id_User_ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopClassLibrary.ModelShop.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Id_User_Image");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Сarte", b =>
                {
                    b.HasOne("ShopClassLibrary.ModelShop.User", null)
                        .WithMany("Сartes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Image", b =>
                {
                    b.Navigation("ImageCopies");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ShopClassLibrary.ModelShop.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Сartes");
                });
#pragma warning restore 612, 618
        }
    }
}
