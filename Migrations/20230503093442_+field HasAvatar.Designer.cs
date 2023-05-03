﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Social_Network_API.Database;

#nullable disable

namespace Social_Network_API.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20230503093442_+field HasAvatar")]
    partial class fieldHasAvatar
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Social_Network_API.Entities.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FollowerId")
                        .HasColumnType("int");

                    b.Property<int>("FollowingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("FollowingId");

                    b.ToTable("subscriptions", (string)null);
                });

            modelBuilder.Entity("Social_Network_API.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte>("Age")
                        .HasColumnType("tinyint");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("BIGINT")
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(320)");

                    b.Property<bool>("HasAvatar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("HasAvatar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("CHAR(64)");

                    b.Property<byte>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint unsigned")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("Role");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("CHAR(24)")
                        .HasColumnName("Salt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Social_Network_API.Entities.Subscription", b =>
                {
                    b.HasOne("Social_Network_API.Entities.User", "Follower")
                        .WithMany("Following")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social_Network_API.Entities.User", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("Social_Network_API.Entities.User", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Following");
                });
#pragma warning restore 612, 618
        }
    }
}
