using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityTypeConfiguration).Assembly);
        modelBuilder.AddIsDeletedQueryFilter();
    }

    public DbSet<ActivityEntity> Activities { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<AssetAttribute> AssetAttributes { get; set; }
    public DbSet<AssetEntity> Assets { get; set; }
    public DbSet<AssetType> AssetTypes { get; set; }
    public DbSet<AttributeGroup> AttributeGroups { get; set; }
    public DbSet<CharacterAsset> CharacterAssets { get; set; }
    public DbSet<CharacterAttribute> CharacterAttributes { get; set; }
    public DbSet<CharacterEntity> Characters { get; set; }
    public DbSet<CharacterType> CharacterTypes { get; set; }
    public DbSet<GameEntity> Games { get; set; }
    public DbSet<GameServer> GameServers { get; set; }
    public DbSet<LevelEntity> Levels { get; set; }
    public DbSet<LevelProgress> LevelProgresses { get; set; }
    public DbSet<PaymentEntity> Payments { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<WalletCategory> WalletCategories { get; set; }
    public DbSet<WalletEntity> Wallets { get; set; }
}
