using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Type2_MBC.Models;

public partial class Type2MbcContext : DbContext
{
    public Type2MbcContext()
    {
    }

    public Type2MbcContext(DbContextOptions<Type2MbcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<MovieDatabase> MovieDatabases { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Theatre> Theatres { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Type2_MBC;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("Booking_Id");
            entity.Property(e => e.BookingDate)
                .HasColumnType("datetime")
                .HasColumnName("Booking_Date");
            entity.Property(e => e.MovieId).HasColumnName("Movie_Id");
            entity.Property(e => e.SeatId).HasColumnName("Seat_Id");
            entity.Property(e => e.TheatreId).HasColumnName("Theatre_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Movie).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Booking_Movie_Database");

            entity.HasOne(d => d.Seat).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("FK_Booking_Seat");

            entity.HasOne(d => d.Theatre).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TheatreId)
                .HasConstraintName("FK_Booking_Theatre");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Booking_User");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.CityId)
                .ValueGeneratedNever()
                .HasColumnName("City_Id");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MovieDatabase>(entity =>
        {
            entity.HasKey(e => e.MovieId);

            entity.ToTable("Movie_Database");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("Movie_Id");
            entity.Property(e => e.CityId).HasColumnName("City_Id");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.City).WithMany(p => p.MovieDatabases)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Movie_Database_City");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.ToTable("Seat");

            entity.Property(e => e.SeatId).HasColumnName("Seat_id");
            entity.Property(e => e.TheatreId).HasColumnName("Theatre_Id");

            entity.HasOne(d => d.Theatre).WithMany(p => p.Seats)
                .HasForeignKey(d => d.TheatreId)
                .HasConstraintName("FK_Seat_Theatre");
        });

        modelBuilder.Entity<Theatre>(entity =>
        {
            entity.ToTable("Theatre");

            entity.Property(e => e.TheatreId).HasColumnName("Theatre_Id");
            entity.Property(e => e.CityId).HasColumnName("City_Id");
            entity.Property(e => e.MovieId).HasColumnName("Movie_Id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.City).WithMany(p => p.Theatres)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Theatre_City");

            entity.HasOne(d => d.Movie).WithMany(p => p.Theatres)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Theatre_Movie_Database");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
