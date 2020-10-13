using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blog_Version_2.ModelsDAL
{
    public partial class Blog_ContextDAL : DbContext
    {
        public Blog_ContextDAL()
        {
        }

        public Blog_ContextDAL(DbContextOptions<Blog_ContextDAL> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorys> Categorys { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-GMR70BN0\\SQLEXPRESS;Database=Blog_Version_2;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorys>(entity =>
            {

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<Comments>(entity =>
            {

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.Themes).IsRequired();
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.Property(e => e.Author).IsRequired();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Header).IsRequired();

                entity.Property(e => e.PreviewPhoto).HasColumnName("Preview_Photo");

                entity.Property(e => e.PreviewText)
                    .IsRequired()
                    .HasColumnName("Preview_Text");

                entity.Property(e => e.Tags).IsRequired();  

                entity.Property(e => e.Url).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Blocked);

                entity.Property(e => e.Watching);
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.Property(e => e.Tag).IsRequired();

                entity.Property(e => e.Url).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
