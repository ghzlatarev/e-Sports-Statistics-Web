﻿// <auto-generated />
using System;
using ESportStatistics.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ESportStatistics.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181027183622_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ESportStatistics.Data.Models.Champion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Armor");

                    b.Property<double?>("ArmorPerLevel");

                    b.Property<double?>("AttackDamage");

                    b.Property<double?>("AttackDamagePerLevel");

                    b.Property<double?>("AttackRange");

                    b.Property<double?>("AttackSpeedOffset");

                    b.Property<double?>("AttackSpeedPerlevel");

                    b.Property<string>("BigImageURL");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<double?>("Crit");

                    b.Property<double?>("CritPerLevel");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<double?>("HP");

                    b.Property<double?>("HPPerLevel");

                    b.Property<double?>("HPRegen");

                    b.Property<double?>("HPRegenPerLevel");

                    b.Property<string>("ImageURL");

                    b.Property<bool>("IsDeleted");

                    b.Property<double?>("MP");

                    b.Property<double?>("MPPerLevel");

                    b.Property<double?>("MPRegen");

                    b.Property<double?>("MPRegenPerLevel");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<double?>("Movespeed");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.Property<double?>("SpellBlock");

                    b.Property<double?>("SpellBlockPerLevel");

                    b.HasKey("Id");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.ToTable("Champions");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BaseGold");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int?>("FlatArmorModifier");

                    b.Property<int?>("FlatCritChanceModifier");

                    b.Property<int?>("FlatHeatlhRegenModifier");

                    b.Property<int?>("FlatMagicDamageModifier");

                    b.Property<int?>("FlatManaPoolModifier");

                    b.Property<int?>("FlatManaRegenModifier");

                    b.Property<int?>("FlatPhysicalDamageModifier");

                    b.Property<int?>("FlatSpellBlockModifier");

                    b.Property<bool?>("GoldPurchaseable");

                    b.Property<string>("ImageURL");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.Property<int?>("PercentAttackSpeedModifier");

                    b.Property<int?>("PercentLifeStealModifier");

                    b.Property<int?>("PercentMovementSpeedModifier");

                    b.Property<int?>("SellGold");

                    b.Property<int?>("TotalGold");

                    b.HasKey("Id");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.League", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("ImageURL");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool?>("LifeSupported");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.Property<string>("Slug");

                    b.Property<string>("URL");

                    b.HasKey("Id");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Mastery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.HasKey("Id");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.ToTable("Masteries");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BeginAt");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool?>("Draw");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("MatchType");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int?>("NumberOfGames");

                    b.Property<int>("PandaScoreId");

                    b.Property<string>("Status");

                    b.Property<int?>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.HasIndex("TournamentId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bio");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<int?>("CurrentTeamId");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("FirstName");

                    b.Property<string>("Hometown");

                    b.Property<string>("ImageURL");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.Property<string>("Role");

                    b.Property<string>("Slug");

                    b.HasKey("Id");

                    b.HasIndex("CurrentTeamId");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Serie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BeginAt");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndAt");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("LeagueId");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.Property<string>("Season");

                    b.Property<string>("Slug");

                    b.Property<int?>("Year");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.ToTable("Series");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Spell", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("ImageURL");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.HasKey("Id");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.ToTable("Spells");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Acronym");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("ImageURL");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.HasKey("Id");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Tournament", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BeginAt");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("EndAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("LeagueId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PandaScoreId");

                    b.Property<int?>("SerieId");

                    b.Property<string>("Slug");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("PandaScoreId")
                        .IsUnique();

                    b.HasIndex("SerieId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Match", b =>
                {
                    b.HasOne("ESportStatistics.Data.Models.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId")
                        .HasPrincipalKey("PandaScoreId");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Player", b =>
                {
                    b.HasOne("ESportStatistics.Data.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("CurrentTeamId")
                        .HasPrincipalKey("PandaScoreId");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Serie", b =>
                {
                    b.HasOne("ESportStatistics.Data.Models.League", "League")
                        .WithMany("Series")
                        .HasForeignKey("LeagueId")
                        .HasPrincipalKey("PandaScoreId");
                });

            modelBuilder.Entity("ESportStatistics.Data.Models.Tournament", b =>
                {
                    b.HasOne("ESportStatistics.Data.Models.League", "League")
                        .WithMany("Tournaments")
                        .HasForeignKey("LeagueId")
                        .HasPrincipalKey("PandaScoreId");

                    b.HasOne("ESportStatistics.Data.Models.Serie", "Serie")
                        .WithMany("Tournaments")
                        .HasForeignKey("SerieId")
                        .HasPrincipalKey("PandaScoreId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ESportStatistics.Data.Models.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ESportStatistics.Data.Models.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ESportStatistics.Data.Models.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ESportStatistics.Data.Models.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
