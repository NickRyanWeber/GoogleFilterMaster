﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using googlefiltermaster;

namespace sdgreacttemplate.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191007201007_AddedPrimaryKeyForAccountsCacheAndFiltersCache")]
    partial class AddedPrimaryKeyForAccountsCacheAndFiltersCache
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GoogleFilterMaster.Models.AccountsCache", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GoogleAccountId");

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccountsCache");
                });

            modelBuilder.Entity("GoogleFilterMaster.Models.FiltersCache", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountsCacheId");

                    b.Property<string>("FilterValue");

                    b.Property<string>("GoogleFilterId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AccountsCacheId");

                    b.ToTable("FiltersCache");
                });

            modelBuilder.Entity("googlefiltermaster.Models.MasterFilter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FilterValue");

                    b.Property<string>("Name");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MasterFilter");
                });

            modelBuilder.Entity("googlefiltermaster.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FamilyName");

                    b.Property<string>("GivenName");

                    b.Property<string>("GoogleId");

                    b.Property<string>("Locale");

                    b.Property<string>("Name");

                    b.Property<string>("Picture");

                    b.Property<bool>("VerifiedEmail");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GoogleFilterMaster.Models.AccountsCache", b =>
                {
                    b.HasOne("googlefiltermaster.Models.User", "User")
                        .WithMany("AccountsCache")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GoogleFilterMaster.Models.FiltersCache", b =>
                {
                    b.HasOne("GoogleFilterMaster.Models.AccountsCache", "AccountsCache")
                        .WithMany("FiltersCache")
                        .HasForeignKey("AccountsCacheId");
                });

            modelBuilder.Entity("googlefiltermaster.Models.MasterFilter", b =>
                {
                    b.HasOne("googlefiltermaster.Models.User", "User")
                        .WithMany("MasterFilters")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
