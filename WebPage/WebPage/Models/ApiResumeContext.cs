using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebPage.Models;

public partial class ApiResumeContext : DbContext
{
    public ApiResumeContext()
    {
    }

    public ApiResumeContext(DbContextOptions<ApiResumeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dato> Datos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=True;Initial Catalog=ApiResume;TrustServerCertificate=True;Data Source=DESKTOP-NN49812");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Dates");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Link)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.Title)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
