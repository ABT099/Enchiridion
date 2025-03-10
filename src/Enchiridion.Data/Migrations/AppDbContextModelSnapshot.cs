﻿// <auto-generated />
using System;
using Enchiridion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Enchiridion.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Enchiridion.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text")
                        .HasColumnName("avatar_url");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_authors");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_authors_user_id");

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.AuthorRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_author_requests");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_author_requests_user_id");

                    b.ToTable("author_requests", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("HabitCategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("habit_category_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_habits");

                    b.HasIndex("HabitCategoryId")
                        .HasDatabaseName("ix_habits_habit_category_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_habits_user_id");

                    b.ToTable("habits", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.HabitCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_habit_categories");

                    b.ToTable("habit_categories", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer")
                        .HasColumnName("author_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("QuoteText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("quote_text");

                    b.HasKey("Id")
                        .HasName("pk_quotes");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_quotes_author_id");

                    b.ToTable("quotes", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.Routine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.PrimitiveCollection<int[]>("Days")
                        .IsRequired()
                        .HasColumnType("integer[]")
                        .HasColumnName("days");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_routines");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_routines_user_id");

                    b.ToTable("routines", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.RoutineStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("RoutineId")
                        .HasColumnType("integer")
                        .HasColumnName("routine_id");

                    b.Property<int>("StepOrder")
                        .HasColumnType("integer")
                        .HasColumnName("step_order");

                    b.HasKey("Id")
                        .HasName("pk_routine_steps");

                    b.HasIndex("RoutineId")
                        .HasDatabaseName("ix_routine_steps_routine_id");

                    b.ToTable("routine_steps", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean")
                        .HasColumnName("is_complete");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("RoutineStepId")
                        .HasColumnType("integer")
                        .HasColumnName("routine_step_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<bool>("isRepeated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_repeated");

                    b.HasKey("Id")
                        .HasName("pk_todos");

                    b.HasIndex("RoutineStepId")
                        .HasDatabaseName("ix_todos_routine_step_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_todos_user_id");

                    b.ToTable("todos", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("auth_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("AuthId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_auth_id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("step_habit", b =>
                {
                    b.Property<int>("habit_id")
                        .HasColumnType("integer")
                        .HasColumnName("habit_id");

                    b.Property<int>("step_id")
                        .HasColumnType("integer")
                        .HasColumnName("step_id");

                    b.HasKey("habit_id", "step_id")
                        .HasName("pk_step_habit");

                    b.HasIndex("habit_id")
                        .HasDatabaseName("ix_step_habit_habit_id");

                    b.HasIndex("step_id")
                        .HasDatabaseName("ix_step_habit_step_id");

                    b.ToTable("step_habit", (string)null);
                });

            modelBuilder.Entity("user_author_subscription", b =>
                {
                    b.Property<int>("user_id")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("author_id")
                        .HasColumnType("integer")
                        .HasColumnName("author_id");

                    b.HasKey("user_id", "author_id")
                        .HasName("pk_user_author_subscription");

                    b.HasIndex("author_id")
                        .HasDatabaseName("ix_user_author_subscription_author_id");

                    b.ToTable("user_author_subscription", (string)null);
                });

            modelBuilder.Entity("Enchiridion.Models.Author", b =>
                {
                    b.HasOne("Enchiridion.Models.User", "User")
                        .WithOne("Author")
                        .HasForeignKey("Enchiridion.Models.Author", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_authors_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Enchiridion.Models.AuthorRequest", b =>
                {
                    b.HasOne("Enchiridion.Models.User", null)
                        .WithMany("AuthorRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_author_requests_users_user_id");
                });

            modelBuilder.Entity("Enchiridion.Models.Habit", b =>
                {
                    b.HasOne("Enchiridion.Models.HabitCategory", "HabitCategory")
                        .WithMany()
                        .HasForeignKey("HabitCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_habits_habit_categories_habit_category_id");

                    b.HasOne("Enchiridion.Models.User", null)
                        .WithMany("Habits")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_habits_users_user_id");

                    b.OwnsOne("Enchiridion.Models.RepeatOptions", "HabitOptions", b1 =>
                        {
                            b1.Property<int>("HabitId")
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            b1.Property<int>("RepeatInterval")
                                .HasColumnType("integer")
                                .HasColumnName("habit_options_repeat_interval");

                            b1.Property<DateTime>("TargetDate")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("habit_options_target_date");

                            b1.HasKey("HabitId");

                            b1.ToTable("habits");

                            b1.WithOwner()
                                .HasForeignKey("HabitId")
                                .HasConstraintName("fk_habits_habits_id");
                        });

                    b.Navigation("HabitCategory");

                    b.Navigation("HabitOptions")
                        .IsRequired();
                });

            modelBuilder.Entity("Enchiridion.Models.Quote", b =>
                {
                    b.HasOne("Enchiridion.Models.Author", "Author")
                        .WithMany("Quotes")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_quotes_authors_author_id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Enchiridion.Models.Routine", b =>
                {
                    b.HasOne("Enchiridion.Models.User", null)
                        .WithMany("Routines")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_routines_users_user_id");
                });

            modelBuilder.Entity("Enchiridion.Models.RoutineStep", b =>
                {
                    b.HasOne("Enchiridion.Models.Routine", null)
                        .WithMany("Steps")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_routine_steps_routines_routine_id");
                });

            modelBuilder.Entity("Enchiridion.Models.Todo", b =>
                {
                    b.HasOne("Enchiridion.Models.RoutineStep", "RoutineStep")
                        .WithMany("Todos")
                        .HasForeignKey("RoutineStepId")
                        .HasConstraintName("fk_todos_routine_steps_routine_step_id");

                    b.HasOne("Enchiridion.Models.User", null)
                        .WithMany("Todos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_todos_users_user_id");

                    b.OwnsOne("Enchiridion.Models.RepeatOptions", "TodoOptions", b1 =>
                        {
                            b1.Property<int>("TodoId")
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            b1.Property<int>("RepeatInterval")
                                .HasColumnType("integer")
                                .HasColumnName("todo_options_repeat_interval");

                            b1.Property<DateTime>("TargetDate")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("todo_options_target_date");

                            b1.HasKey("TodoId");

                            b1.ToTable("todos");

                            b1.WithOwner()
                                .HasForeignKey("TodoId")
                                .HasConstraintName("fk_todos_todos_id");
                        });

                    b.Navigation("RoutineStep");

                    b.Navigation("TodoOptions");
                });

            modelBuilder.Entity("step_habit", b =>
                {
                    b.HasOne("Enchiridion.Models.Habit", null)
                        .WithMany()
                        .HasForeignKey("habit_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_step_habit_to_habit");

                    b.HasOne("Enchiridion.Models.RoutineStep", null)
                        .WithMany()
                        .HasForeignKey("step_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_step_habit_to_step");
                });

            modelBuilder.Entity("user_author_subscription", b =>
                {
                    b.HasOne("Enchiridion.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("author_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_author_subscription_authors_author_id");

                    b.HasOne("Enchiridion.Models.User", null)
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_author_subscription_users_user_id");
                });

            modelBuilder.Entity("Enchiridion.Models.Author", b =>
                {
                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("Enchiridion.Models.Routine", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Enchiridion.Models.RoutineStep", b =>
                {
                    b.Navigation("Todos");
                });

            modelBuilder.Entity("Enchiridion.Models.User", b =>
                {
                    b.Navigation("Author");

                    b.Navigation("AuthorRequests");

                    b.Navigation("Habits");

                    b.Navigation("Routines");

                    b.Navigation("Todos");
                });
#pragma warning restore 612, 618
        }
    }
}
