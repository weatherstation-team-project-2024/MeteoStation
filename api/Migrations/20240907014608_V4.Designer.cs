﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240907014608_V4")]
    partial class V4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("api.Models.Station", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "station_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StationId"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasAnnotation("Relational:JsonPropertyName", "active");

                    b.Property<string>("City")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "city");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "company_name");

                    b.Property<string>("Country")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "country");

                    b.Property<double>("Elevation")
                        .HasColumnType("double precision")
                        .HasAnnotation("Relational:JsonPropertyName", "elevation");

                    b.Property<string>("FirmwareVersion")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "firmware_version");

                    b.Property<int>("GatewayId")
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "gateway_id");

                    b.Property<string>("GatewayIdHex")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "gateway_id_hex");

                    b.Property<string>("GatewayType")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "gateway_type");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision")
                        .HasAnnotation("Relational:JsonPropertyName", "latitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision")
                        .HasAnnotation("Relational:JsonPropertyName", "longitude");

                    b.Property<bool>("Private")
                        .HasColumnType("boolean")
                        .HasAnnotation("Relational:JsonPropertyName", "private");

                    b.Property<string>("ProductNumber")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "product_number");

                    b.Property<int>("RecordingInterval")
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "recording_interval");

                    b.Property<string>("Region")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "region");

                    b.Property<long>("RegisteredDate")
                        .HasColumnType("bigint")
                        .HasAnnotation("Relational:JsonPropertyName", "registered_date");

                    b.Property<string>("RelationshipType")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "relationship_type");

                    b.Property<Guid>("StationIdUuid")
                        .HasColumnType("uuid");

                    b.Property<string>("StationIdUuidString")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "station_id_uuid");

                    b.Property<string>("StationName")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "station_name");

                    b.Property<string>("SubscriptionType")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "subscription_type");

                    b.Property<string>("TimeZone")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "time_zone");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "user_email");

                    b.Property<string>("Username")
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "username");

                    b.Property<long>("WeatherStationsGeneratedAt")
                        .HasColumnType("bigint");

                    b.Property<int>("WeatherStationsId")
                        .HasColumnType("integer");

                    b.HasKey("StationId");

                    b.HasIndex("StationIdUuid")
                        .IsUnique();

                    b.HasIndex("WeatherStationsGeneratedAt");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("api.Models.WeatherStations", b =>
                {
                    b.Property<long>("GeneratedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Relational:JsonPropertyName", "generated_at");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("GeneratedAt"));

                    b.HasKey("GeneratedAt");

                    b.ToTable("WeatherStations");
                });

            modelBuilder.Entity("api.Models.Station", b =>
                {
                    b.HasOne("api.Models.WeatherStations", "WeatherStations")
                        .WithMany("Stations")
                        .HasForeignKey("WeatherStationsGeneratedAt")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherStations");
                });

            modelBuilder.Entity("api.Models.WeatherStations", b =>
                {
                    b.Navigation("Stations");
                });
#pragma warning restore 612, 618
        }
    }
}
