using WritersPlatform.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace WritersPlatform.DataAccess.Contexts;

public class CompositionDbContext: DbContext
{
    public virtual DbSet<AuthorEntity> Authors { get; set; }
    public virtual DbSet<GenreEntity> Genres { get; set; }
    public virtual DbSet<CompositionEntity> Compositions { get; set; }
    public virtual DbSet<CommentEntity> Comments { get; set; }

    public CompositionDbContext(DbContextOptions<CompositionDbContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authors_table_pkey");

            entity.ToTable("authors_table");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
        });

        modelBuilder.Entity<GenreEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_table_pkey");

            entity.ToTable("genres_table");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CompositionEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("composition_table_pkey");

            entity.ToTable("composition_table");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).ValueGeneratedOnAdd().HasColumnName("author_id");
            entity.Property(e => e.GenreId).ValueGeneratedOnAdd().HasColumnName("genre_id");
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.ImagePath).HasMaxLength(100).HasColumnName("image_path");
            entity.Property(e => e.Path).HasMaxLength(100).HasColumnName("path");
            //entity.Property(e => e.Rating).HasMaxLength(100).HasColumnName("rating");
            entity.Property(e=>e.Description).HasMaxLength(500).HasColumnName("description");
            entity.Property(e => e.CreateDate).HasMaxLength(20).HasColumnName("create_date");
            entity.HasOne(d => d.Author).WithMany(p => p.Compositions).HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("compositions_table_author_id_fkey");
            entity.HasOne(d => d.Genre).WithMany(p => p.Compositions).HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("compositions_table_genre_id_fkey");
        });


        modelBuilder.Entity<CommentEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_table_pkey");

            entity.ToTable("comments_table");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Rating)
                .HasMaxLength(100)
                .HasColumnName("rating");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .HasColumnName("text");
            entity.HasOne(d => d.Author).WithMany(p => p.Comments).HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("compositions_table_author_id_fkey");
            entity.HasOne(d => d.Composition).WithMany(p => p.Comments).HasForeignKey(d => d.CompositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("compositions_table_composition_id_fkey");
        });
    }
}
