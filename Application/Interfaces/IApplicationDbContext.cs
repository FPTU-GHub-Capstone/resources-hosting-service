using Domain.Entities.Activity;
using Domain.Entities.Asset;
using Domain.Entities.Attribute;
using Domain.Entities.Character;
using Domain.Entities.Game;
using Domain.Entities.Level;
using Domain.Entities.Payment;
using Domain.Entities.Transaction;
using Domain.Entities.User;
using Domain.Entities.Wallet;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
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
