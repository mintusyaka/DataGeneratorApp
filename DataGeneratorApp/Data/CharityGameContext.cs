using System;
using System.Collections.Generic;
using DataGeneratorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DataGeneratorApp.Data;

public partial class CharityGameContext : DbContext
{
    public CharityGameContext()
    {
    }

    public CharityGameContext(DbContextOptions<CharityGameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<CharactersItem> CharactersItems { get; set; }

    public virtual DbSet<CharityFund> CharityFunds { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<LevelsCharacter> LevelsCharacters { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerActivity> PlayerActivities { get; set; }

    public virtual DbSet<PlayersCharacter> PlayersCharacters { get; set; }

    public virtual DbSet<PlayersItem> PlayersItems { get; set; }

    public virtual DbSet<PlayersLevel> PlayersLevels { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q01DVK6;Initial Catalog=charity_game;Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("character");

            entity.HasIndex(e => e.Description, "UQ__characte__489B0D97B6217A20").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__characte__72E12F1BE29CD3F4").IsUnique();

            entity.Property(e => e.CharacterId)
                .ValueGeneratedNever()
                .HasColumnName("character_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Speed).HasColumnName("speed");
            entity.Property(e => e.Strength).HasColumnName("strength");
        });

        modelBuilder.Entity<CharactersItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("characters_items");

            entity.Property(e => e.CharacterId).HasColumnName("character_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");

            entity.HasOne(d => d.Character).WithMany()
                .HasForeignKey(d => d.CharacterId)
                .HasConstraintName("FK__character__chara__22751F6C");

            entity.HasOne(d => d.Item).WithMany()
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__character__item___236943A5");
        });

        modelBuilder.Entity<CharityFund>(entity =>
        {
            entity.ToTable("charity_fund");

            entity.HasIndex(e => e.CharityFundId, "UQ__charity___129FF536C6AEFBCF").IsUnique();

            entity.HasIndex(e => e.CardNumber, "UQ__charity___1E6E0AF4EFFC4900").IsUnique();

            entity.HasIndex(e => e.Description, "UQ__charity___489B0D9770CDF68B").IsUnique();

            entity.HasIndex(e => e.TelephoneNumber, "UQ__charity___CCB1623C748E63FB").IsUnique();

            entity.HasIndex(e => e.Title, "UQ__charity___E52A1BB3371BEFA5").IsUnique();

            entity.Property(e => e.CharityFundId).HasColumnName("charity_fund_id");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("card_number");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("telephone_number");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("item");

            entity.HasIndex(e => e.Description, "UQ__item__489B0D976F523A79").IsUnique();

            entity.HasIndex(e => e.ItemId, "UQ__item__52020FDC3391A36B").IsUnique();

            entity.HasIndex(e => e.Title, "UQ__item__E52A1BB3D1C7FFFF").IsUnique();

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("item_id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ItemType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("item_type");
            entity.Property(e => e.Rarity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rarity");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.ToTable("level");

            entity.HasIndex(e => e.Description, "UQ__level__489B0D970A03322F").IsUnique();

            entity.HasIndex(e => e.Title, "UQ__level__E52A1BB3036A4090").IsUnique();

            entity.Property(e => e.LevelId)
                .ValueGeneratedNever()
                .HasColumnName("level_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("creation_date");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("creation_time");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UnlockScore).HasColumnName("unlock_score");
        });

        modelBuilder.Entity<LevelsCharacter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("levels_characters");

            entity.Property(e => e.CharacterId).HasColumnName("character_id");
            entity.Property(e => e.LevelId).HasColumnName("level_id");

            entity.HasOne(d => d.Character).WithMany()
                .HasForeignKey(d => d.CharacterId)
                .HasConstraintName("FK__levels_ch__chara__339FAB6E");

            entity.HasOne(d => d.Level).WithMany()
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK__levels_ch__level__3493CFA7");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("player_pk");

            entity.ToTable("player");

            entity.HasIndex(e => e.Nickname, "UQ__player__5CF1C59B584E9DEB").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__player__AB6E61643C026E55").IsUnique();

            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.Email)
                .HasMaxLength(319)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nickname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("registration_date");
            entity.Property(e => e.RegistrationTime)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("registration_time");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sex");
        });

        modelBuilder.Entity<PlayerActivity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("player_activity");

            entity.Property(e => e.DeathsCount).HasColumnName("deaths_count");
            entity.Property(e => e.DonatedFundsCount).HasColumnName("donated_funds_count");
            entity.Property(e => e.IsOnline).HasColumnName("is_online");
            entity.Property(e => e.KilledEnemiesCount).HasColumnName("killed_enemies_count");
            entity.Property(e => e.OnlineHoursCount).HasColumnName("online_hours_count");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.WatchedAdsCount).HasColumnName("watched_ads_count");

            entity.HasOne(d => d.Player).WithMany()
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_player_activity_player");
        });

        modelBuilder.Entity<PlayersCharacter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("players_characters");

            entity.Property(e => e.CharacterId).HasColumnName("character_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");

            entity.HasOne(d => d.Character).WithMany()
                .HasForeignKey(d => d.CharacterId)
                .HasConstraintName("FK__players_c__chara__1DB06A4F");

            entity.HasOne(d => d.Player).WithMany()
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__players_c__playe__1CBC4616");
        });

        modelBuilder.Entity<PlayersItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("players_items");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");

            entity.HasOne(d => d.Item).WithMany()
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__players_i__item___208CD6FA");

            entity.HasOne(d => d.Player).WithMany()
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__players_i__playe__1F98B2C1");
        });

        modelBuilder.Entity<PlayersLevel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("players_levels");

            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");

            entity.HasOne(d => d.Level).WithMany()
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK__players_l__level__123EB7A3");

            entity.HasOne(d => d.Player).WithMany()
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__players_l__playe__114A936A");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_transactions");

            entity.ToTable("transaction");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.CharityFundId).HasColumnName("charity_fund_id");
            entity.Property(e => e.MoneyCount).HasColumnName("money_count");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.TransactionDate).HasColumnName("transaction_date");
            entity.Property(e => e.TransactionTime)
                .HasPrecision(0)
                .HasColumnName("transaction_time");

            entity.HasOne(d => d.CharityFund).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CharityFundId)
                .HasConstraintName("FK__transacti__chari__31B762FC");

            entity.HasOne(d => d.Player).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__transacti__playe__30C33EC3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
