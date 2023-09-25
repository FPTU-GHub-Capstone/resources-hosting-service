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
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts;
public class EntityTypeConfiguration : 
    IEntityTypeConfiguration<ActivityType>, IEntityTypeConfiguration<ActivityEntity>,
    IEntityTypeConfiguration<AssetAttribute>, IEntityTypeConfiguration<AssetEntity>,
    IEntityTypeConfiguration<AssetType>, IEntityTypeConfiguration<AttributeGroup>,
    IEntityTypeConfiguration<CharacterAsset>, IEntityTypeConfiguration<CharacterAttribute>,
    IEntityTypeConfiguration<CharacterEntity>, IEntityTypeConfiguration<CharacterType>,
    IEntityTypeConfiguration<GameEntity>, IEntityTypeConfiguration<GameServer>,
    IEntityTypeConfiguration<LevelEntity>, IEntityTypeConfiguration<LevelProgress>,
    IEntityTypeConfiguration<PaymentEntity>, IEntityTypeConfiguration<TransactionEntity>,
    IEntityTypeConfiguration<Client>, IEntityTypeConfiguration<UserEntity>,
    IEntityTypeConfiguration<WalletCategory>, IEntityTypeConfiguration<WalletEntity>
{
    //Activity Entity
    public void Configure(EntityTypeBuilder<ActivityEntity> builder)
    {
        // 1 Transaction - 1 or M Activity
        builder
            .HasOne(t => t.Transaction)
            .WithMany(a => a.Activities)
            .HasForeignKey(tId => tId.TransactionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Activity Type
    public void Configure(EntityTypeBuilder<ActivityType> builder)
    {
        // 1 Game - M Activity Type
        builder
            .HasOne(g =>g.Game)
            .WithMany(at => at.ActivityTypes)
            .HasForeignKey(gId => gId.GameId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Asset Attribute
    public void Configure(EntityTypeBuilder<AssetAttribute> builder)
    {
        
    }
    //Asset Entity
    public void Configure(EntityTypeBuilder<AssetEntity> builder)
    {

    }
    //Asset Type
    public void Configure(EntityTypeBuilder<AssetType> builder)
    {

    }
    //Attribute Group
    public void Configure(EntityTypeBuilder<AttributeGroup> builder)
    {

    }
    //Character Asset
    public void Configure(EntityTypeBuilder<CharacterAsset> builder)
    {
        //1 Character - M CharacterAsset
        builder
            .HasOne(c => c.Characters)
            .WithMany(ca => ca.CharacterAssets)
            .HasForeignKey(cId => cId.CharacterId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Character Attribute
    public void Configure(EntityTypeBuilder<CharacterAttribute> builder)
    {

    }
    //Character Entity
    public void Configure(EntityTypeBuilder<CharacterEntity> builder)
    {
        //1 GameServer - M Character
        builder
            .HasOne(gs => gs.GameServer)
            .WithMany(c => c.Characters)
            .HasForeignKey(gsid => gsid.GameServerId)
            .OnDelete(DeleteBehavior.NoAction);

        //1 User - M Character
        builder
            .HasOne(gs => gs.User)
            .WithMany(c => c.Characters)
            .HasForeignKey(gsid => gsid.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Character Type
    public void Configure(EntityTypeBuilder<CharacterType> builder)
    {

    }
    //Game Entity
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {

    }
    //Game Server
    public void Configure(EntityTypeBuilder<GameServer> builder)
    {

    }
    //Level Entity
    public void Configure(EntityTypeBuilder<LevelEntity> builder)
    {
        //1 Level - 1 Level Progress
        builder
            .HasOne(l => l.LevelProgress)
            .WithOne(pId => pId.Level)
            .HasForeignKey<LevelProgress>(id => id.LevelId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Level Progress
    public void Configure(EntityTypeBuilder<LevelProgress> builder)
    {
        //1 Level Progress - 1 Level
        //builder
        //    .HasOne(l => l.Level)
        //    .WithOne(pId => pId.LevelProgress)
        //    .HasForeignKey<LevelEntity>(id => id.LevelProgressId)
        //    .OnDelete(DeleteBehavior.NoAction);
    }
    //Payment Entity
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        //1 Payment - 1 Wallet
        builder
            .HasOne(l => l.Wallet)
            .WithOne(pId => pId.Payment)
            .HasForeignKey<WalletEntity>(id => id.PaymentId)
            .OnDelete(DeleteBehavior.NoAction);
        //1 User - M Payment
        builder
            .HasOne(u => u.User)
            .WithMany(p => p.Payments)
            .HasForeignKey(uId => uId.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Transaction Entity
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {

    }
    //Client
    public void Configure(EntityTypeBuilder<Client> builder)
    {

    }
    //User Entity
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {

    }
    //Wallet Category
    public void Configure(EntityTypeBuilder<WalletCategory> builder)
    {

    }
    //Wallet Entity
    public void Configure(EntityTypeBuilder<WalletEntity> builder)
    {
        // 1 Wallet Category - M Wallet
        builder
            .HasOne(wc => wc.WalletCategory)
            .WithMany(w=>w.WalletEntities)
            .HasForeignKey(wcId => wcId.WalletCategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
