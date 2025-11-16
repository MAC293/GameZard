using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GameZard.Context;

public partial class GameZardDbContext : DbContext
{
    public GameZardDbContext()
    {
    }

    public GameZardDbContext(DbContextOptions<GameZardDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Emulator> Emulators { get; set; }

    public virtual DbSet<EmulatorSavedatum> EmulatorSavedata { get; set; }

    public virtual DbSet<PcSavedatum> PcSavedata { get; set; }

    public virtual DbSet<Videogame> Videogames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=D:\\Programming\\Business Projects\\GameZard Project\\GameZard\\Database\\GameZard.db");

    //C:\Users\Joseph Pino\Desktop\Backup\GameZard Project\GameZard\Database

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Emulator>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("Emulator");

            entity.Property(e => e.ExecutableLocation).HasColumnName("Executable_Location");
            entity.Property(e => e.IsSelected).HasColumnName("Is_Selected");
        });

        modelBuilder.Entity<EmulatorSavedatum>(entity =>
        {
            entity.ToTable("Emulator_Savedata");

            entity.HasIndex(e => e.Emulator, "IX_Emulator_Savedata_Emulator").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BackupMode).HasColumnName("Backup_Mode");
            entity.Property(e => e.FromPath).HasColumnName("From_Path");
            entity.Property(e => e.LastSave).HasColumnName("Last_Save");
            entity.Property(e => e.ToPath).HasColumnName("To_Path");

            entity.HasOne(d => d.EmulatorNavigation).WithOne(p => p.EmulatorSavedatum)
                .HasForeignKey<EmulatorSavedatum>(d => d.Emulator)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<PcSavedatum>(entity =>
        {
            entity.ToTable("PC_Savedata");

            entity.HasIndex(e => e.Videogame, "IX_PC_Savedata_Videogame").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BackupMode).HasColumnName("Backup_Mode");
            entity.Property(e => e.FromPath).HasColumnName("From_Path");
            entity.Property(e => e.LastSave).HasColumnName("Last_Save");
            entity.Property(e => e.ToPath).HasColumnName("To_Path");

            entity.HasOne(d => d.VideogameNavigation).WithOne(p => p.PcSavedatum)
                .HasForeignKey<PcSavedatum>(d => d.Videogame)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Videogame>(entity =>
        {
            entity.ToTable("Videogame");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ExecutableLocation).HasColumnName("Executable_Location");
            entity.Property(e => e.IsSelected).HasColumnName("Is_Selected");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
