using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Enums;
using System.Data;

namespace StargateAPI.Business.Data
{
    public class StargateContext : DbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<Person> People { get; set; }

        public DbSet<AstronautDetail> AstronautDetails { get; set; }

        public DbSet<AstronautDuty> AstronautDuties { get; set; }

        public DbSet<RequestLog> RequestLog { get; set; }

        public StargateContext(DbContextOptions<StargateContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StargateContext).Assembly);

            var isTestEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test";

            if (!isTestEnvironment)
            {
                SeedData(modelBuilder); // Only seed data in non-test environments to not overlap with test seeding
            }

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasData(
                    new Person  //Normal person, no astronaut duties
                    {
                        Id = 1,
                        Name = "John Smith"
                    },
                    new Person
                    {
                        Id = 2,
                        Name = "John Doe"
                    },
                    new Person
                    {
                        Id = 3,
                        Name = "Jane Doe"
                    }
                );

            modelBuilder.Entity<AstronautDetail>()
                .HasData(
                    new AstronautDetail
                    {
                        Id = 1,
                        PersonId = 2,
                        CurrentRank = Rank.LT1,
                        CurrentDutyTitle = DutyTitle.Commander,
                        CareerStartDate = new DateTime(2024, 3, 2)
                    },
                    new AstronautDetail
                    {
                        Id = 2,
                        PersonId = 3,
                        CurrentRank = Rank.SGT,
                        CurrentDutyTitle = DutyTitle.Retired,
                        CareerStartDate = new DateTime(1995, 2, 4),
                        CareerEndDate = new DateTime(2020, 5, 2)
                    }
                );

            modelBuilder.Entity<AstronautDuty>()
                .HasData(
                    new AstronautDuty
                    {
                        Id = 1,
                        PersonId = 2,
                        Rank = Rank.LT2,
                        DutyStartDate = new DateTime(2023, 8, 1),
                        DutyEndDate = new DateTime(2024, 1, 5),
                        DutyTitle = DutyTitle.Pilot
                    },
                    new AstronautDuty
                    {
                        Id = 2,
                        PersonId = 2,
                        Rank = Rank.LT2,
                        DutyStartDate = new DateTime(2024, 1, 6),
                        DutyEndDate = new DateTime(2024, 3, 1),
                        DutyTitle = DutyTitle.Commander
                    },
                    new AstronautDuty
                    {
                        Id = 3,
                        PersonId = 3,
                        Rank = Rank.PVT,
                        DutyStartDate = new DateTime(1995, 2, 4),
                        DutyEndDate = new DateTime(1998, 4, 1),
                        DutyTitle = DutyTitle.FlightEngineer
                    },
                    new AstronautDuty
                    {
                        Id = 4,
                        PersonId = 3,
                        Rank = Rank.PFC,
                        DutyStartDate = new DateTime(1998, 4, 2),
                        DutyEndDate = new DateTime(2002, 10, 1),
                        DutyTitle = DutyTitle.FlightEngineer
                    },
                    new AstronautDuty
                    {
                        Id = 5,
                        PersonId = 3,
                        Rank = Rank.SPC,
                        DutyStartDate = new DateTime(2002, 10, 2),
                        DutyEndDate = new DateTime(2010, 7, 1),
                        DutyTitle = DutyTitle.LunarModulePilot
                    },
                    new AstronautDuty
                    {
                        Id = 6,
                        PersonId = 3,
                        Rank = Rank.CPL,
                        DutyStartDate = new DateTime(2010, 7, 2),
                        DutyEndDate = new DateTime(2020, 5, 2),
                        DutyTitle = DutyTitle.LunarModulePilot
                    },
                    new AstronautDuty
                    {
                        Id = 7,
                        PersonId = 3,
                        Rank = Rank.SGT,
                        DutyStartDate = new DateTime(2020, 5, 3),
                        DutyTitle = DutyTitle.Retired
                    }
                );
        }
    }
}