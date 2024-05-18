﻿// <auto-generated />
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Context.Migrations
{
    [DbContext(typeof(IMDBapiContext))]
    [Migration("20231208073039_addField")]
    partial class addField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.Entities.Movies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstAirDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstAirDate");

                    b.Property<int>("IdOnOpenAPI")
                        .HasColumnType("int")
                        .HasColumnName("IdOnOpenAPI");

                    b.Property<bool>("Isfavorite")
                        .HasColumnType("bit")
                        .HasColumnName("Isfavorite");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("OriginalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OriginalName");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Overview");

                    b.Property<double>("Popularity")
                        .HasColumnType("float")
                        .HasColumnName("Popularity");

                    b.Property<double>("VoteAverage")
                        .HasColumnType("float")
                        .HasColumnName("VoteAverage");

                    b.Property<int>("VoteCount")
                        .HasColumnType("int")
                        .HasColumnName("VoteCount");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}