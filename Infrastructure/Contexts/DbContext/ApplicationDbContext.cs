using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Extensions;

namespace RepositoryLayer.Contexts;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityTypeConfiguration).Assembly);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeleteQueryFilter();
            }
        }
    }

    public DbSet<ActivityEntity> Activities { get; set; }
    public DbSet<ActivityTypeEntity> ActivityTypes { get; set; }
    public DbSet<AssetAttributeEntity> AssetAttributes { get; set; }
    public DbSet<AssetEntity> Assets { get; set; }
    public DbSet<AssetTypeEntity> AssetTypes { get; set; }
    public DbSet<AttributeGroupEntity> AttributeGroups { get; set; }
    public DbSet<CharacterAssetEntity> CharacterAssets { get; set; }
    public DbSet<CharacterAttributeEntity> CharacterAttributes { get; set; }
    public DbSet<CharacterEntity> Characters { get; set; }
    public DbSet<CharacterTypeEntity> CharacterTypes { get; set; }
    public DbSet<GameEntity> Games { get; set; }
    public DbSet<GameServerEntity> GameServers { get; set; }
    public DbSet<LevelEntity> Levels { get; set; }
    public DbSet<LevelProgressEntity> LevelProgresses { get; set; }
    public DbSet<PaymentEntity> Payments { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<WalletCategoryEntity> WalletCategories { get; set; }
    public DbSet<WalletEntity> Wallets { get; set; }
}
