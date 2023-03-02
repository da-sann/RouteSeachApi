﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RouteSeachApi.Data.Sql.Core;

namespace RouteSeachApi.Migrations
{
    [DbContext(typeof(ShardDbContext))]
    partial class ShardDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RouteSeachApi.Data.Entities.Route", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Destination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DestinationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OriginDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TimeLimit")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });
#pragma warning restore 612, 618
        }
    }
}
