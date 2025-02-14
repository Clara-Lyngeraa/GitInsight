﻿// <auto-generated />
using System;
using GitInsight.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GitInsight.Entities.Migrations
{
    [DbContext(typeof(GitInsightContext))]
    [Migration("20221117110411_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GitInsight.Entities.AnalyzedRepo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("character varying(48)");

                    b.HasKey("Id");

                    b.ToTable("AnalyzedRepos");
                });

            modelBuilder.Entity("GitInsight.Entities.DataCommit", b =>
                {
                    b.Property<string>("StringId")
                        .HasColumnType("text");

                    b.Property<int?>("AnalyzedRepoId")
                        .HasColumnType("integer");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("character varying(48)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("StringId");

                    b.HasIndex("AnalyzedRepoId");

                    b.ToTable("DataCommits");
                });

            modelBuilder.Entity("GitInsight.Entities.DataCommit", b =>
                {
                    b.HasOne("GitInsight.Entities.AnalyzedRepo", null)
                        .WithMany("CommitsInRepo")
                        .HasForeignKey("AnalyzedRepoId");
                });

            modelBuilder.Entity("GitInsight.Entities.AnalyzedRepo", b =>
                {
                    b.Navigation("CommitsInRepo");
                });
#pragma warning restore 612, 618
        }
    }
}
