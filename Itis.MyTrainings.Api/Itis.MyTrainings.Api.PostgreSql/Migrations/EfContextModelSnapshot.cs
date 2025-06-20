﻿// <auto-generated />
using System;
using Itis.MyTrainings.Api.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Itis.MyTrainings.Api.PostgreSql.Migrations
{
    [DbContext(typeof(EfContext))]
    partial class EfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ExerciseTraining", b =>
                {
                    b.Property<Guid>("ExercisesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TrainingsId")
                        .HasColumnType("uuid");

                    b.HasKey("ExercisesId", "TrainingsId");

                    b.HasIndex("TrainingsId");

                    b.ToTable("training_exercise", "public", t =>
                        {
                            t.HasComment("Промежуточная таблица связи многие ко многим между тренировками и упражнениями");
                        });
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<int?>("Approaches")
                        .HasColumnType("integer")
                        .HasComment("Кол-во подходов");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasComment("Описание");

                    b.Property<string>("ExplanationVideo")
                        .HasColumnType("text")
                        .HasComment("Ссылка на видео с объяснением");

                    b.Property<string>("ImplementationProgress")
                        .HasColumnType("text")
                        .HasComment("Ход выполнения");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Наименование упражнения");

                    b.Property<int?>("Repetitions")
                        .HasColumnType("integer")
                        .HasComment("Кол-во повторений в подходе");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("exercises", "public", t =>
                        {
                            t.HasComment("Упражнения");
                        });
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Текст сообщения");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("timestamp without time zone")
                        .HasComment("Дата отправки");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid")
                        .HasComment("Id отправителя");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.HasIndex("UserId");

                    b.ToTable("messages", "public", t =>
                        {
                            t.HasComment("Сообщения");
                        });
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<long>("Remains")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ee1f1a13-7f38-46f6-ac3d-4ecf38a4d79a"),
                            Description = "asd",
                            Name = "asd",
                            Price = 10L,
                            Remains = 20L
                        });
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasComment("Наименование роли");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text")
                        .HasComment("Нормализованное имя");

                    b.HasKey("Id");

                    b.ToTable("roles", "public", t =>
                        {
                            t.HasComment("Роли");
                        });
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.SupportChat.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

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

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.SupportChat.UnicastChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FromEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ToEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UnicastChatMessages");
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.Training", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasComment("Наименование тренировки");

                    b.Property<DateTime>("TrainingDate")
                        .HasColumnType("timestamp without time zone")
                        .HasComment("Дата тренировки");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("trainings", "public", t =>
                        {
                            t.HasComment("Тренировки");
                        });
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasComment("Почта");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Имя");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Фамилия");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("RegisteredWithGoogle")
                        .HasColumnType("boolean")
                        .HasComment("Зарегистрирован через Google");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasComment("Никнейм пользователя");

                    b.Property<Guid?>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("users", "public", t =>
                        {
                            t.HasComment("Профили пользователей");
                        });
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasComment("Дата рождения");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer")
                        .HasComment("Гендер");

                    b.Property<int?>("Height")
                        .HasColumnType("integer")
                        .HasComment("Рост");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasComment("Номер телефона");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Weight")
                        .HasColumnType("integer")
                        .HasComment("Вес");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_profiles", "public", t =>
                        {
                            t.HasComment("Профили пользователей");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("ExerciseTraining", b =>
                {
                    b.HasOne("Itis.MyTrainings.Api.Core.Entities.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExercisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Itis.MyTrainings.Api.Core.Entities.Training", null)
                        .WithMany()
                        .HasForeignKey("TrainingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.Exercise", b =>
                {
                    b.HasOne("Itis.MyTrainings.Api.Core.Entities.User", "User")
                        .WithMany("Exercises")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.Message", b =>
                {
                    b.HasOne("Itis.MyTrainings.Api.Core.Entities.User", "Sender")
                        .WithMany("SendedMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Itis.MyTrainings.Api.Core.Entities.User", null)
                        .WithMany("RecievedMessages")
                        .HasForeignKey("UserId");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.Training", b =>
                {
                    b.HasOne("Itis.MyTrainings.Api.Core.Entities.User", "User")
                        .WithMany("Trainings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.UserProfile", b =>
                {
                    b.HasOne("Itis.MyTrainings.Api.Core.Entities.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Itis.MyTrainings.Api.Core.Entities.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Itis.MyTrainings.Api.Core.Entities.User", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("RecievedMessages");

                    b.Navigation("SendedMessages");

                    b.Navigation("Trainings");

                    b.Navigation("UserProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
