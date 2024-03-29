﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperPlay.GameServer.Infrastructure;

#nullable disable

namespace SuperPlay.GameServer.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.25");

            modelBuilder.Entity("SuperPlay.GameServer.Domain.Entities.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("PlayerId");

                    b.Property<int>("Coins")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Coins");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("DeviceId");

                    b.Property<int>("Rolls")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Rolls");

                    b.HasKey("PlayerId");

                    b.ToTable("Player", (string)null);

                    b.HasData(
                        new
                        {
                            PlayerId = 1,
                            Coins = 0,
                            DeviceId = "1",
                            Rolls = 0
                        },
                        new
                        {
                            PlayerId = 2,
                            Coins = 0,
                            DeviceId = "2",
                            Rolls = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
