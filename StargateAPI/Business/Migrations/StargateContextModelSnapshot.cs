﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StargateAPI.Business.Data;

#nullable disable

namespace StargateAPI.Migrations
{
    [DbContext(typeof(StargateContext))]
    partial class StargateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("StargateAPI.Business.Dtos.AstronautDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CareerEndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CareerStartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CurrentDutyTitle")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentRank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("AstronautDetail");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CareerStartDate = new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentDutyTitle = 3,
                            CurrentRank = 15,
                            PersonId = 2
                        },
                        new
                        {
                            Id = 2,
                            CareerEndDate = new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CareerStartDate = new DateTime(1995, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CurrentDutyTitle = 7,
                            CurrentRank = 4,
                            PersonId = 3
                        });
                });

            modelBuilder.Entity("StargateAPI.Business.Dtos.AstronautDuty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DutyEndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DutyStartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("DutyTitle")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("AstronautDuty");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DutyEndDate = new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyStartDate = new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyTitle = 4,
                            PersonId = 2,
                            Rank = 14
                        },
                        new
                        {
                            Id = 2,
                            DutyEndDate = new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyStartDate = new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyTitle = 3,
                            PersonId = 2,
                            Rank = 14
                        },
                        new
                        {
                            Id = 3,
                            DutyEndDate = new DateTime(1998, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyStartDate = new DateTime(1995, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyTitle = 2,
                            PersonId = 3,
                            Rank = 0
                        },
                        new
                        {
                            Id = 4,
                            DutyEndDate = new DateTime(2002, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyStartDate = new DateTime(1998, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyTitle = 2,
                            PersonId = 3,
                            Rank = 1
                        },
                        new
                        {
                            Id = 5,
                            DutyEndDate = new DateTime(2010, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyStartDate = new DateTime(2002, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyTitle = 5,
                            PersonId = 3,
                            Rank = 2
                        },
                        new
                        {
                            Id = 6,
                            DutyEndDate = new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyStartDate = new DateTime(2010, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyTitle = 5,
                            PersonId = 3,
                            Rank = 3
                        },
                        new
                        {
                            Id = 7,
                            DutyStartDate = new DateTime(2020, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DutyTitle = 7,
                            PersonId = 3,
                            Rank = 4
                        });
                });

            modelBuilder.Entity("StargateAPI.Business.Dtos.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "John Smith"
                        },
                        new
                        {
                            Id = 2,
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Jane Doe"
                        });
                });

            modelBuilder.Entity("StargateAPI.Business.Dtos.RequestLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("TEXT");

                    b.Property<string>("HttpMethod")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ResponseTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusCode")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RequestLog");
                });

            modelBuilder.Entity("StargateAPI.Business.Dtos.AstronautDetail", b =>
                {
                    b.HasOne("StargateAPI.Business.Dtos.Person", "Person")
                        .WithOne("AstronautDetail")
                        .HasForeignKey("StargateAPI.Business.Dtos.AstronautDetail", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("StargateAPI.Business.Dtos.AstronautDuty", b =>
                {
                    b.HasOne("StargateAPI.Business.Dtos.Person", "Person")
                        .WithMany("AstronautDuties")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("StargateAPI.Business.Dtos.Person", b =>
                {
                    b.Navigation("AstronautDetail");

                    b.Navigation("AstronautDuties");
                });
#pragma warning restore 612, 618
        }
    }
}
