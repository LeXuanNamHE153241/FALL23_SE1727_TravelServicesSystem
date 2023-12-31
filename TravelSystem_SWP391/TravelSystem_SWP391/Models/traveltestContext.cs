﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TravelSystem_SWP391.Models
{
    public partial class traveltestContext : DbContext
    {
        public traveltestContext()
        {
        }

        public traveltestContext(DbContextOptions<traveltestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<Information> Informations { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Tour> Tours { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                var builder = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.HotelId).HasColumnName("hotel_id");

                entity.Property(e => e.Message)
                    .HasMaxLength(200)
                    .HasColumnName("message");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.NumPeople).HasColumnName("num_people");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_User_");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Booking_Hotel");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_Booking_Restaurant");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Booking_Vehicle");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryCode).HasMaxLength(20);

                entity.Property(e => e.CountryName).HasMaxLength(255);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("Feedback");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .HasColumnName("message");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Response)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Subject).HasMaxLength(50);

                entity.HasOne(d => d.EmailNavigation)
                    .WithOne(p => p.Feedback)
                    .HasForeignKey<Feedback>(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_User_");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Amenities)
                    .HasMaxLength(100)
                    .HasColumnName("amenities");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Review)
                    .HasMaxLength(200)
                    .HasColumnName("review");

                entity.Property(e => e.RoomTypes)
                    .HasMaxLength(50)
                    .HasColumnName("room_types");
            });

            modelBuilder.Entity<Information>(entity =>
            {
                entity.Property(e => e.EmailUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InformationDescription).HasMaxLength(255);

                entity.Property(e => e.InformationName).HasMaxLength(20);

                entity.HasOne(d => d.EmailUserNavigation)
                    .WithMany(p => p.Information)
                    .HasForeignKey(d => d.EmailUser)
                    .HasConstraintName("FK__Informati__Email__571DF1D5");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Information)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__Informati__TourI__5812160E");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.EmailUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationCode).HasMaxLength(20);

                entity.Property(e => e.LocationName).HasMaxLength(255);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Locations__Count__59063A47");

                entity.HasOne(d => d.EmailUserNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.EmailUser)
                    .HasConstraintName("FK__Locations__Email__59FA5E80");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .HasColumnName("price");

                entity.Property(e => e.Rate).HasColumnName("rate");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.EmailUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailUserNavigation)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.EmailUser)
                    .HasConstraintName("FK__Schedules__Email__5AEE82B9");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK__Schedules__Hotel__5BE2A6F2");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Schedules__Locat__5CD6CB2B");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__Schedules__Resta__00200768");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK__Schedules__TourI__01142BA1");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK__Schedules__Vehic__02084FDA");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AirPlane)
                    .HasMaxLength(100)
                    .HasColumnName("Air_Plane");

                entity.Property(e => e.CreateDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Duration).HasMaxLength(50);

                entity.Property(e => e.HotelId).HasColumnName("Hotel_id");

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Tour_Hotel");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_Tour_Restaurant");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK_Tour_Staff");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Tour_Vehicle");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK_User");

                entity.ToTable("User_");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.EmailUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Email_User");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.RoleStaff)
                    .HasMaxLength(50)
                    .HasColumnName("roleStaff");

                entity.HasOne(d => d.EmailUserNavigation)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.EmailUser)
                    .HasConstraintName("FK_Email_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
