using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Contexts;

public interface IApplicationDbContext
{
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