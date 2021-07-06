﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhantomAbyssServer.Database;

namespace PhantomAbyssServer.Migrations
{
    [DbContext(typeof(PAContext))]
    [Migration("20210705204718_MakeCurrentUserOptional")]
    partial class MakeCurrentUserOptional
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.Dungeon", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("CompletedById")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("NumFloors")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RelicId")
                        .HasColumnType("TEXT");

                    b.Property<uint>("RouteId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("RouteStage")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompletedById");

                    b.HasIndex("RouteId");

                    b.ToTable("Dungeons");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.DungeonKeyCurrency", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("NumKeys")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Stage")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("UserCurrencyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserCurrencyId");

                    b.ToTable("DungeonKeyCurrency");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.DungeonLayout", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("DungeonFloorCount")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("DungeonFloorNumber")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("DungeonFloorType")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("DungeonId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("DungeonLayoutType")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("DungeonVersion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LayoutDataHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PermanentSettingsData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RelicId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LayoutDataHash")
                        .IsUnique();

                    b.HasIndex("DungeonId", "DungeonVersion", "DungeonFloorNumber")
                        .IsUnique();

                    b.ToTable("DungeonLayouts");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.Route", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("CompletedById")
                        .HasColumnType("INTEGER");

                    b.Property<uint?>("CurrentUserId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("RouteAttemptCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompletedById");

                    b.HasIndex("CurrentUserId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.SavedRun", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DataHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("DungeonFloorNumber")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("DungeonId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("RouteId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RunSuccessful")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ServerVersion")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DataHash")
                        .IsUnique();

                    b.HasIndex("RouteId");

                    b.HasIndex("UserId");

                    b.HasIndex("DungeonId", "RouteId", "DungeonFloorNumber");

                    b.ToTable("SavedRuns");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.User", b =>
                {
                    b.Property<uint>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("CurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("HealthId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("SharerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SteamId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("HealthId")
                        .IsUnique();

                    b.HasIndex("SharerId")
                        .IsUnique();

                    b.HasIndex("SteamId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.UserCurrency", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Essence")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UserCurrency");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.UserHealth", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("BaseHealth")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("BonusHealth")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("MaxBonusHealth")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UserHealth");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.Dungeon", b =>
                {
                    b.HasOne("PhantomAbyssServer.Database.Models.User", "CompletedBy")
                        .WithMany()
                        .HasForeignKey("CompletedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhantomAbyssServer.Database.Models.Route", "Route")
                        .WithMany("Dungeons")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompletedBy");

                    b.Navigation("Route");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.DungeonKeyCurrency", b =>
                {
                    b.HasOne("PhantomAbyssServer.Database.Models.UserCurrency", "UserCurrency")
                        .WithMany("DungeonKeys")
                        .HasForeignKey("UserCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCurrency");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.Route", b =>
                {
                    b.HasOne("PhantomAbyssServer.Database.Models.User", "CompletedBy")
                        .WithMany()
                        .HasForeignKey("CompletedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhantomAbyssServer.Database.Models.User", "CurrentUser")
                        .WithMany()
                        .HasForeignKey("CurrentUserId");

                    b.Navigation("CompletedBy");

                    b.Navigation("CurrentUser");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.SavedRun", b =>
                {
                    b.HasOne("PhantomAbyssServer.Database.Models.Dungeon", "Dungeon")
                        .WithMany("SavedRuns")
                        .HasForeignKey("DungeonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhantomAbyssServer.Database.Models.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhantomAbyssServer.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dungeon");

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.User", b =>
                {
                    b.HasOne("PhantomAbyssServer.Database.Models.UserCurrency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhantomAbyssServer.Database.Models.UserHealth", "Health")
                        .WithOne("User")
                        .HasForeignKey("PhantomAbyssServer.Database.Models.User", "HealthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Health");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.Dungeon", b =>
                {
                    b.Navigation("SavedRuns");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.Route", b =>
                {
                    b.Navigation("Dungeons");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.UserCurrency", b =>
                {
                    b.Navigation("DungeonKeys");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.UserHealth", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
