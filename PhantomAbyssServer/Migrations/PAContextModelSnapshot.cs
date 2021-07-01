﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhantomAbyssServer.Database;

namespace PhantomAbyssServer.Migrations
{
    [DbContext(typeof(PAContext))]
    partial class PAContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

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

                    b.Property<uint>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DataHash")
                        .IsUnique();

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

                    b.HasIndex("CurrencyId")
                        .IsUnique();

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

                    b.Property<uint>("UserId")
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

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.DungeonKeyCurrency", b =>
                {
                    b.HasOne("PhantomAbyssServer.Database.Models.UserCurrency", "UserCurrency")
                        .WithMany("DungeonKeys")
                        .HasForeignKey("UserCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCurrency");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.SavedRun", b =>
                {
                    b.HasOne("PhantomAbyssServer.Database.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.User", b =>
                {
                    b.HasOne("PhantomAbyssServer.Database.Models.UserCurrency", "Currency")
                        .WithOne("User")
                        .HasForeignKey("PhantomAbyssServer.Database.Models.User", "CurrencyId")
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

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.UserCurrency", b =>
                {
                    b.Navigation("DungeonKeys");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PhantomAbyssServer.Database.Models.UserHealth", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
