using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Music_Store.Models;

namespace Music_Store.Data;

public partial class MusicDataContext : IdentityDbContext<ApplicationUser>
{
    public MusicDataContext()
    {
    }

    public MusicDataContext(DbContextOptions<MusicDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlbumTbl> Albums { get; set; }

    public virtual DbSet<GenreTbl> Genres { get; set; }

    public virtual DbSet<LanguageTbl> Languages { get; set; }

    public virtual DbSet<SingerTbl> Singers { get; set; }

    public virtual DbSet<SongTbl> Songs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SAAMOO;Database=MusicData;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlbumTbl>(entity =>
        {
            entity.HasKey(e => e.AlbumId);

            entity.ToTable("AlbumTbl");

            entity.Property(e => e.AlbumId).HasColumnName("albumId");
            entity.Property(e => e.AlbumTitle)
                .HasMaxLength(50)
                .HasColumnName("albumTitle");
        });

        modelBuilder.Entity<GenreTbl>(entity =>
        {
            entity.HasKey(e => e.GenreId);

            entity.ToTable("GenreTbl");

            entity.Property(e => e.GenreId).HasColumnName("genreId");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .HasColumnName("genreName");
        });

        modelBuilder.Entity<LanguageTbl>(entity =>
        {
            entity.HasKey(e => e.LanguageId);

            entity.ToTable("LanguageTbl");

            entity.Property(e => e.LanguageId).HasColumnName("languageId");
            entity.Property(e => e.LanguageName)
                .HasMaxLength(50)
                .HasColumnName("languageName");
        });

        modelBuilder.Entity<SingerTbl>(entity =>
        {
            entity.HasKey(e => e.SingerId);

            entity.ToTable("SingerTbl");

            entity.Property(e => e.SingerId).HasColumnName("singerId");
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("profilePicture");
            entity.Property(e => e.SingerBio)
                .HasMaxLength(150)
                .HasColumnName("singerBio");
            entity.Property(e => e.SingerName)
                .HasMaxLength(100)
                .HasColumnName("singerName");
        });

        modelBuilder.Entity<SongTbl>(entity =>
        {
            entity.HasKey(e => e.SongId);

            entity.ToTable("SongTbl");

            entity.HasIndex(e => e.AlbumId, "IX_SongTbl_albumId");

            entity.HasIndex(e => e.GenreId, "IX_SongTbl_genreId");

            entity.HasIndex(e => e.LanguageId, "IX_SongTbl_languageId");

            entity.HasIndex(e => e.SingerId, "IX_SongTbl_singerId");

            entity.Property(e => e.SongId).HasColumnName("songId");
            entity.Property(e => e.AlbumId).HasColumnName("albumId");
            entity.Property(e => e.GenreId).HasColumnName("genreId");
            entity.Property(e => e.LanguageId).HasColumnName("languageId");
            entity.Property(e => e.SingerId).HasColumnName("singerId");
            entity.Property(e => e.SongDescription)
                .HasMaxLength(150)
                .HasColumnName("songDescription");
            entity.Property(e => e.SongFilepath)
                .HasMaxLength(100)
                .HasColumnName("songFilepath");
            entity.Property(e => e.SongName)
                .HasMaxLength(100)
                .HasColumnName("songName");
            entity.Property(e => e.SongPicture)
                .HasMaxLength(100)
                .HasColumnName("songPicture");
            entity.Property(e => e.SongRelease)
                .HasMaxLength(50)
                .HasColumnName("songRelease");

            entity.HasOne(d => d.Album).WithMany(p => p.SongTbls)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongTbl_AlbumTbl");

            entity.HasOne(d => d.Genre).WithMany(p => p.SongTbls)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongTbl_GenreTbl");

            entity.HasOne(d => d.Language).WithMany(p => p.SongTbls)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongTbl_LanguageTbl");

            entity.HasOne(d => d.Singer).WithMany(p => p.SongTbls)
                .HasForeignKey(d => d.SingerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongTbl_SingerTbl");
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
