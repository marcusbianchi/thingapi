﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using thingservice.Data;

namespace thingservice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171107141339_ThingGroupCorrection")]
    partial class ThingGroupCorrection
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("thingservice.Model.Thing", b =>
                {
                    b.Property<int>("thingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<int[]>("childrenThingsIds")
                        .HasColumnName("childrenThingsIds")
                        .HasColumnType("integer[]");

                    b.Property<bool>("enabled");

                    b.Property<int?>("parentThingId");

                    b.Property<string>("physicalConnection")
                        .HasMaxLength(100);

                    b.Property<int>("position")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("thingCode")
                        .HasMaxLength(50);

                    b.Property<string>("thingName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("thingId");

                    b.ToTable("Things");
                });

            modelBuilder.Entity("thingservice.Model.ThingGroup", b =>
                {
                    b.Property<int>("thingGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("enabled");

                    b.Property<string>("groupCode")
                        .HasMaxLength(50);

                    b.Property<string>("groupDescription")
                        .HasMaxLength(100);

                    b.Property<string>("groupName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int[]>("thingsIds")
                        .HasColumnName("thingsIds")
                        .HasColumnType("integer[]");

                    b.HasKey("thingGroupId");

                    b.ToTable("ThingGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
