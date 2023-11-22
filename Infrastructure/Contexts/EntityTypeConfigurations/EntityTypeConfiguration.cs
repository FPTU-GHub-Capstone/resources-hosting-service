using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Contexts;
public class EntityTypeConfiguration : 
    IEntityTypeConfiguration<ActivityTypeEntity>, IEntityTypeConfiguration<ActivityEntity>,
    IEntityTypeConfiguration<AssetAttributeEntity>, IEntityTypeConfiguration<AssetEntity>,
    IEntityTypeConfiguration<AssetTypeEntity>, IEntityTypeConfiguration<AttributeGroupEntity>,
    IEntityTypeConfiguration<CharacterAssetEntity>, IEntityTypeConfiguration<CharacterAttributeEntity>,
    IEntityTypeConfiguration<CharacterEntity>, IEntityTypeConfiguration<CharacterTypeEntity>,
    IEntityTypeConfiguration<GameEntity>, IEntityTypeConfiguration<GameServerEntity>,
    IEntityTypeConfiguration<LevelEntity>, IEntityTypeConfiguration<LevelProgressEntity>,
    IEntityTypeConfiguration<PaymentEntity>, IEntityTypeConfiguration<TransactionEntity>,
    IEntityTypeConfiguration<WalletCategoryEntity>, IEntityTypeConfiguration<WalletEntity>
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
    public void Configure(EntityTypeBuilder<ActivityTypeEntity> builder)
    {
        // 1 Game - M Activity Type
        builder
            .HasOne(g =>g.Game)
            .WithMany(at => at.ActivityTypes)
            .HasForeignKey(gId => gId.GameId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Asset Attribute
    public void Configure(EntityTypeBuilder<AssetAttributeEntity> builder)
    {
        
    }
    //Asset Entity
    public void Configure(EntityTypeBuilder<AssetEntity> builder)
    {

    }
    //Asset Type
    public void Configure(EntityTypeBuilder<AssetTypeEntity> builder)
    {

    }
    //Attribute Group
    public void Configure(EntityTypeBuilder<AttributeGroupEntity> builder)
    {

    }
    //Character Asset
    public void Configure(EntityTypeBuilder<CharacterAssetEntity> builder)
    {
        //1 Character - M CharacterAsset
        builder
            .HasOne(c => c.Characters)
            .WithMany(ca => ca.CharacterAssets)
            .HasForeignKey(cId => cId.CharacterId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Character Attribute
    public void Configure(EntityTypeBuilder<CharacterAttributeEntity> builder)
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
    public void Configure(EntityTypeBuilder<CharacterTypeEntity> builder)
    {

    }
    //Game Entity
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {

    }
    //Game Server
    public void Configure(EntityTypeBuilder<GameServerEntity> builder)
    {

    }
    //Level Entity
    public void Configure(EntityTypeBuilder<LevelEntity> builder)
    {
        //1 Level - 1 Level Progress
        builder
            .HasOne(l => l.LevelProgress)
            .WithOne(pId => pId.Level)
            .HasForeignKey<LevelProgressEntity>(id => id.LevelId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    //Level Progress
    public void Configure(EntityTypeBuilder<LevelProgressEntity> builder)
    {

    }
    //Payment Entity
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
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
    //User Entity
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {

    }
    //Wallet Category
    public void Configure(EntityTypeBuilder<WalletCategoryEntity> builder)
    {

    }
    //Wallet Entity
    public void Configure(EntityTypeBuilder<WalletEntity> builder)
    {
        //1 Payment - 1 Wallet
        builder
            .HasOne(l => l.Payment)
            .WithOne(pId => pId.Wallet)
            .HasForeignKey<PaymentEntity>(id => id.WalletId)
            .OnDelete(DeleteBehavior.NoAction);
        // 1 Wallet Category - M Wallet
        builder
            .HasOne(wc => wc.WalletCategory)
            .WithMany(w=>w.WalletEntities)
            .HasForeignKey(wcId => wcId.WalletCategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
