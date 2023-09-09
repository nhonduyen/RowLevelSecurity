﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RowLevelSecurity.API.Data;

#nullable disable

namespace RowLevelSecurity.API.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20230826104947_initMig")]
    partial class initMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RowLevelSecurity.API.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9b2edba5-c748-4380-b8b7-c2ddb7a666d2"),
                            ProductName = "Product 1",
                            TenantId = new Guid("06d2940d-ab0c-42dd-8709-5e645e127fc9")
                        },
                        new
                        {
                            Id = new Guid("27c3bb5c-695e-41b8-bb55-c9cd3ba8450e"),
                            ProductName = "Product 2",
                            TenantId = new Guid("fb0dee6f-86d7-40fc-ac06-5dbdb769551d")
                        },
                        new
                        {
                            Id = new Guid("07cb4964-5a23-4dd0-9d4c-94fa5f597d23"),
                            ProductName = "Product 3",
                            TenantId = new Guid("8aed0ef4-2f76-4def-bf88-06c6eadfbf15")
                        },
                        new
                        {
                            Id = new Guid("40586ea6-f66d-4633-9a80-17af378ce1f8"),
                            ProductName = "Product 4",
                            TenantId = new Guid("06d2940d-ab0c-42dd-8709-5e645e127fc9")
                        },
                        new
                        {
                            Id = new Guid("12906c21-1359-40d9-afbc-bf3db4ebd2a6"),
                            ProductName = "Product 5",
                            TenantId = new Guid("fb0dee6f-86d7-40fc-ac06-5dbdb769551d")
                        },
                        new
                        {
                            Id = new Guid("edef1e05-bb1e-40ad-bc8b-686256c42714"),
                            ProductName = "Product 6",
                            TenantId = new Guid("8aed0ef4-2f76-4def-bf88-06c6eadfbf15")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}