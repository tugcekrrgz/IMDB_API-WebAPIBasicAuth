using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Imdb_API.Models;

public partial class ImdbDataContext : DbContext
{
    public ImdbDataContext()
    {
    }

    public ImdbDataContext(DbContextOptions<ImdbDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cast> Casts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Trailer> Trailers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-I9V1GOS\\SQLEXPRESS;database=ImdbData;uid=sa;pwd=Tugce8417058;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cast>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Casts");

            entity.HasIndex(e => e.MovieId, "IX_Movie_Id");

            entity.Property(e => e.MovieId).HasColumnName("Movie_Id");

            entity.HasOne(d => d.Movie).WithMany(p => p.Casts)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_dbo.Casts_dbo.Movies_Movie_Id");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.Comments)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Movies");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Directors");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Genres");
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

            entity.ToTable("__MigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Movies");

            entity.HasIndex(e => e.DirectorId, "IX_Director_Id");

            entity.Property(e => e.DirectorId).HasColumnName("Director_Id");
            entity.Property(e => e.RevenueMillions).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK_dbo.Movies_dbo.Directors_Director_Id");

            entity.HasMany(d => d.Genres).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_dbo.MovieGenres_dbo.Genres_Genre_Id"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("FK_dbo.MovieGenres_dbo.Movies_Movie_Id"),
                    j =>
                    {
                        j.HasKey("MovieId", "GenreId").HasName("PK_dbo.MovieGenres");
                        j.ToTable("MovieGenres");
                        j.HasIndex(new[] { "GenreId" }, "IX_Genre_Id");
                        j.HasIndex(new[] { "MovieId" }, "IX_Movie_Id");
                        j.IndexerProperty<int>("MovieId").HasColumnName("Movie_Id");
                        j.IndexerProperty<int>("GenreId").HasColumnName("Genre_Id");
                    });
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Picture)
                .HasForeignKey<Picture>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pictures_Movies");
        });

        modelBuilder.Entity<Trailer>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Movie).WithMany(p => p.Trailers)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trailers_Movies");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
