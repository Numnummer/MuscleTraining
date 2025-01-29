﻿// <auto-generated />
using System;
using Itis.MyTrainings.ChatHistoryService.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Itis.MyTrainings.ChatHistoryService.PostgreSql.Migrations
{
    [DbContext(typeof(ServiceDbContext))]
    [Migration("20241218055928_AddedFilesTable")]
    partial class AddedFilesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("GroupName")
                        .HasColumnType("integer");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities.Files", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities.UnicastChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FromEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ToEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("UnicastChatMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
