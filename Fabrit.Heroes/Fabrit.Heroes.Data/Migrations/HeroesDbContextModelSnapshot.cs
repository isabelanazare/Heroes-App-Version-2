﻿// <auto-generated />
using System;
using Fabrit.Heroes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fabrit.Heroes.Data.Migrations
{
    [DbContext(typeof(HeroesDbContext))]
    partial class HeroesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Badge.Badge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Badge.HeroBadge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BadgeId")
                        .HasColumnType("int");

                    b.Property<int>("HeroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BadgeId");

                    b.HasIndex("HeroId");

                    b.ToTable("HeroBadges");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Battle.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InitiatorId")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Battle.HeroBattle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleId")
                        .HasColumnType("int");

                    b.Property<bool>("HasWon")
                        .HasColumnType("bit");

                    b.Property<int>("HeroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BattleId");

                    b.HasIndex("HeroId");

                    b.ToTable("HeroBattles");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Hero.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvatarPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBadGuy")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGod")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastTimeMoved")
                        .HasColumnType("datetime2");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OverallStrength")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Hero.HeroAlly", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AllyFromId")
                        .HasColumnType("int");

                    b.Property<int>("AllyToId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AllyFromId");

                    b.HasIndex("AllyToId");

                    b.ToTable("HeroAllies");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Hero.HeroPower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HeroId")
                        .HasColumnType("int");

                    b.Property<int?>("HeroId1")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastChangeTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastTrainingTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PowerId")
                        .HasColumnType("int");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HeroId");

                    b.HasIndex("HeroId1")
                        .IsUnique()
                        .HasFilter("[HeroId1] IS NOT NULL");

                    b.HasIndex("PowerId");

                    b.ToTable("HeroPowers");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Hero.HeroType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HeroTypes");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Power.Element", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Power.Power", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElementId")
                        .HasColumnType("int");

                    b.Property<string>("MainTrait")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ElementId");

                    b.ToTable("Powers");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("AvatarPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("WasPasswordChanged")
                        .HasColumnType("bit");

                    b.Property<bool>("WasPasswordForgotten")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.User.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Badge.HeroBadge", b =>
                {
                    b.HasOne("Fabrit.Heroes.Data.Entities.Badge.Badge", "Badge")
                        .WithMany("Heroes")
                        .HasForeignKey("BadgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fabrit.Heroes.Data.Entities.Hero.Hero", "Hero")
                        .WithMany("Badges")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Battle.HeroBattle", b =>
                {
                    b.HasOne("Fabrit.Heroes.Data.Entities.Battle.Battle", "Battle")
                        .WithMany("Heroes")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fabrit.Heroes.Data.Entities.Hero.Hero", "Hero")
                        .WithMany("Battles")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Hero.Hero", b =>
                {
                    b.HasOne("Fabrit.Heroes.Data.Entities.Hero.HeroType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fabrit.Heroes.Data.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Hero.HeroAlly", b =>
                {
                    b.HasOne("Fabrit.Heroes.Data.Entities.Hero.Hero", "AllyFrom")
                        .WithMany("Allies")
                        .HasForeignKey("AllyFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fabrit.Heroes.Data.Entities.Hero.Hero", "AllyTo")
                        .WithMany()
                        .HasForeignKey("AllyToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Hero.HeroPower", b =>
                {
                    b.HasOne("Fabrit.Heroes.Data.Entities.Hero.Hero", "Hero")
                        .WithMany("Powers")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fabrit.Heroes.Data.Entities.Hero.Hero", null)
                        .WithOne("MainPower")
                        .HasForeignKey("Fabrit.Heroes.Data.Entities.Hero.HeroPower", "HeroId1");

                    b.HasOne("Fabrit.Heroes.Data.Entities.Power.Power", "Power")
                        .WithMany("Heroes")
                        .HasForeignKey("PowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.Power.Power", b =>
                {
                    b.HasOne("Fabrit.Heroes.Data.Entities.Power.Element", "Element")
                        .WithMany()
                        .HasForeignKey("ElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fabrit.Heroes.Data.Entities.User.User", b =>
                {
                    b.HasOne("Fabrit.Heroes.Data.Entities.User.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
